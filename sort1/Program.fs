[<EntryPoint>]
let main args = 
    Utils.SortFile.run args.[0] // FIX: if args.Length != 1 print usage
    0