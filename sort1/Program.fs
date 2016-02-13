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
[<EntryPoint>]
let main args = 
    let runWithMessages file =
        printfn "Sorting \"%s\"." file
        Sort1.SortFile.run file
        printfn "Finished sorting."
        printfn "Created \"%s\"." (Sort1.SortFile.outputPath file)
    match args.Length with
    | 1 -> runWithMessages args.[0]
    | _ -> printfn "Usage: sort1.exe <input file>"
    0
