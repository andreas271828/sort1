(********************************************************************
Copyright (C) 2016 Andreas Huemer

This file is part of sort1.

sort1 is free software: you can redistribute it and/or modify it
under the terms of the GNU General Public License as published by the
Free Software Foundation, either version 3 of the License, or (at
your option) any later version.

sort1 is distributed in the hope that it will be useful, but
WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program. If not, see <http://www.gnu.org/licenses/>.
********************************************************************)
module Sort1.SortFile

open System

type Line = 
    | Empty
    | Invalid
    | Data of string * string * int

let outputPath inputPath = 
    let ext = IO.Path.GetExtension inputPath
    let n = inputPath.Length - ext.Length
    let p = inputPath.Substring(0, n)
    p + "-sorted.txt"

let parseLine (line : string) = 
    let splitLine = line.Split [| ',' |]
    
    let parse l f s = 
        try 
            Line.Data(l, f, Int32.Parse s)
        with :? FormatException -> Line.Invalid
    match splitLine.Length with
    | 1 when String.IsNullOrWhiteSpace splitLine.[0] -> Line.Empty
    | 3 -> parse (splitLine.[0].Trim()) (splitLine.[1].Trim()) (splitLine.[2].Trim())
    | _ -> Line.Invalid

let compareLines line1 line2 = 
    match (line1, line2) with
    | ((_, _, s1), (_, _, s2)) when s1 > s2 -> -1
    | ((_, _, s1), (_, _, s2)) when s1 < s2 -> 1
    | ((l1, f1, _), (l2, f2, _)) -> 
        let l = String.Compare(l1, l2)
        match l with
        | 0 -> String.Compare(f1, f2)
        | _ -> l

let processLines lines = 
    let isData = 
        function 
        | Line.Data(_, _, _) -> true
        | _ -> false
    
    let getData = 
        function 
        | Line.Data(l, f, s) -> (l, f, s)
        | _ -> failwith "No data to extract."
    
    let printLine (l, f, s) = sprintf "%s, %s, %i" l f s
    lines
    |> Seq.map parseLine
    |> Seq.filter isData
    |> Seq.map getData
    |> Seq.toArray
    |> Array.sortWith compareLines
    |> Array.map printLine

let run inputPath = IO.File.WriteAllLines(outputPath inputPath, processLines (IO.File.ReadLines inputPath))
