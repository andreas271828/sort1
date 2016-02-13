module Sort1.SortFileTests2

open NUnit.Framework

[<TestFixture>]
type SortFileTests() = 
    
    static member ParseLineData = 
        [| ("HUEMER, ANDREAS, 42", ("HUEMER", "ANDREAS", 42))
           ("HUEMER,    ANDREAS,42", ("HUEMER", "ANDREAS", 42)) |]
    
    [<TestCaseSource("ParseLineData")>]
    static member parseLine parseLineData = 
        let input, expected = parseLineData
        Assert.AreEqual(expected, SortFile.parseLine input)
    
    static member CompareLinesData = 
        [| (("HUEMER", "ANDREAS", 42), ("HUEMER", "ANDREAS", 42), 0)
           (("HUEMER", "ANDREAS", 52), ("HUEMER", "ANDREAS", 42), -1) |]
    
    [<TestCaseSource("CompareLinesData")>]
    static member parseLine compareLinesData = 
        let line1, line2, expected = compareLinesData
        Assert.AreEqual(expected, SortFile.compareLines line1 line2)
