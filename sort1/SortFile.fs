namespace Utils

// FIX: Error handling (create unit tests with error cases)
// FIX: Print information in console.
module SortFile = 
    open System

    let run inputPath = 
        let outputPath =
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
            lines
            |> Seq.map parseLine
            |> Seq.sortWith compareLines
        
        let processFile = 
            let printLine (l, f, s) = sprintf "%s, %s, %i" l f s
            IO.File.ReadLines inputPath
            |> processLines
            |> Seq.map printLine
        
        IO.File.WriteAllLines(outputPath, processFile)
