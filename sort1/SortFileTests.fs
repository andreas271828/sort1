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
module Sort1.SortFileTests

open NUnit.Framework

[<TestFixture>]
type SortFileTests() = 
    
    [<TestCase("Test.txt", "Test-sorted.txt")>]
    [<TestCase("test", "test-sorted.txt")>]
    [<TestCase("test.other", "test-sorted.txt")>]
    [<TestCase("/tmp/test.txt", "/tmp/test-sorted.txt")>]
    [<TestCase("../../test.txt", "../../test-sorted.txt")>]
    [<TestCase("C:/tmp/test.txt", "C:/tmp/test-sorted.txt")>]
    [<TestCase("C:\tmp\test.txt", "C:\tmp\test-sorted.txt")>]
    static member outputPath input expected = Assert.AreEqual(expected, SortFile.outputPath input)
    
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
    
    static member ProcessLinesData = 
        let input1 = [| "HUEMER, ANDREAS, 42"; "HUEMER, ANDREAS, 42" |]
        let expected1 = [| "HUEMER, ANDREAS, 42"; "HUEMER, ANDREAS, 42" |]
        let input2 = [| "HUEMER, ANDREAS, 42"; "HUEMER, ANDREAS, 45" |]
        let expected2 = [| "HUEMER, ANDREAS, 45"; "HUEMER, ANDREAS, 42" |]
        [| (input1, expected1)
           (input2, expected2) |]
    
    [<TestCaseSource("ProcessLinesData")>]
    static member processLines (processLinesData : string [] * string []) = 
        let input, expected = processLinesData
        Assert.AreEqual(expected, SortFile.processLines input)
