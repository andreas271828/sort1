module Sort1.SortFileTests

open NUnit.Framework
open Sort1.SortFile

[<Test>]
let outputPath() = 
    Assert.AreEqual("Test-sorted.txt", SortFile.outputPath "Test.txt")
    Assert.AreEqual("test-sorted.txt", SortFile.outputPath "test")

[<Test>]
let parseLine() = 
    Assert.AreEqual(("HUEMER", "ANDREAS", 42), SortFile.parseLine "HUEMER, ANDREAS, 42")
    Assert.AreEqual(("HUEMER", "ANDREAS", 42), SortFile.parseLine "HUEMER,    ANDREAS,42")

[<Test>]
let compareLines() = 
    Assert.AreEqual(0, SortFile.compareLines ("HUEMER", "ANDREAS", 42) ("HUEMER", "ANDREAS", 42))
    Assert.AreEqual(-1, SortFile.compareLines ("HUEMER", "ANDREAS", 52) ("HUEMER", "ANDREAS", 42))

[<Test>]
let processLines() = 
    let lines1 = [| "HUEMER, ANDREAS, 42"; "HUEMER, ANDREAS, 42" |]
    let lines1Sorted = [| "HUEMER, ANDREAS, 42"; "HUEMER, ANDREAS, 42" |]
    let lines2 = [| "HUEMER, ANDREAS, 42"; "HUEMER, ANDREAS, 45" |]
    let lines2Sorted = [| "HUEMER, ANDREAS, 45"; "HUEMER, ANDREAS, 42" |]
    Assert.AreEqual(lines1Sorted, SortFile.processLines lines1)
