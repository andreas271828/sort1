namespace Utils

module SortFile = 
    open System
    
    let run inputPath = 
        let outputPath = inputPath + ".new" // FIX: File name according to specifications
        IO.File.WriteAllLines(outputPath, IO.File.ReadLines inputPath) // FIX: Sort input
