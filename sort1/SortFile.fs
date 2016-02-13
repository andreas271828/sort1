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

// FIX: More unit tests (incl. edge cases)
// FIX: Error handling (create unit tests with error cases)
// FIX: Test duplicate, incomplete and empty lines.
// FIX: Print information in console.
open System

let outputPath inputPath = 
    let p = IO.Path.GetFileNameWithoutExtension inputPath
    p + "-sorted.txt"

let parseLine (line : string) = 
    let splitLine = line.Split [| ',' |]
    (splitLine.[0].Trim(), splitLine.[1].Trim(), Int32.Parse splitLine.[2])

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
    let printLine (l, f, s) = sprintf "%s, %s, %i" l f s
    lines
    |> Seq.map parseLine
    |> Seq.toArray
    |> Array.sortWith compareLines
    |> Array.map printLine

let run inputPath = IO.File.WriteAllLines(outputPath inputPath, processLines (IO.File.ReadLines inputPath))
