[<EntryPoint>]
let main args = 
    Sort1.SortFile.run args.[0] // FIX: if args.Length != 1 print usage
    0