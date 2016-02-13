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
        [| ("HUEMER, ANDREAS, 42", SortFile.Line.Data("HUEMER", "ANDREAS", 42))
           ("HUEMER,    ANDREAS,42", SortFile.Line.Data("HUEMER", "ANDREAS", 42)) |]
    
    [<TestCaseSource("ParseLineData")>]
    static member parseLine parseLineData = 
        let input, expected = parseLineData
        Assert.AreEqual(expected, SortFile.parseLine input)
    
    static member CompareLinesData = 
        [| (("xxx", "yyy", 42), ("xxx", "yyy", 42), 0)
           (("xxx", "yyy", 42), ("xxx", "yy", 42), 1)
           (("xxx", "yyy", 42), ("xxx", "zzz", 42), -1)
           (("xxx", "zzz", 42), ("xxx", "yyy", 42), 1)
           (("xxx", "yyy", 42), ("x", "yyy", 42), 1)
           (("xxx", "yyy", 42), ("y", "yyy", 42), -1)
           (("xxx", "yyy", 42), ("y", "x", 42), -1)
           (("y", "x", 42), ("xxx", "yyy", 42), 1)
           (("xxx", "yyy", 42), ("xxx", "yyy", 52), 1)
           (("xxx", "yyy", 42), ("aaa", "aaa", 52), 1)
           (("aaa", "aaa", 52), ("xxx", "yyy", 42), -1) |]
    
    [<TestCaseSource("CompareLinesData")>]
    static member parseLine compareLinesData = 
        let line1, line2, expected = compareLinesData
        Assert.AreEqual(expected, SortFile.compareLines line1 line2)
    
    static member ProcessLinesData = 
        [| ([||], [||])
           ([| "x, y, 42"; "x, y, 42" |], [| "x, y, 42"; "x, y, 42" |])
           ([| "x, y, 42"; "x, y, 45" |], [| "x, y, 45"; "x, y, 42" |])
           ([| "x, y, 45"; "x, y, 42" |], [| "x, y, 45"; "x, y, 42" |])
           ([| "x, y, 45"; "x, y, 42"; "x, y, 45" |], [| "x, y, 45"; "x, y, 45"; "x, y, 42" |])
           ([| "x, y, 42"; ""; "x, y, 45" |], [| "x, y, 45"; "x, y, 42" |])
           ([| "x, y"; "hello"; "x, y, 45"; "1, 2, 3, 4, 5" |], [| "x, y, 45" |])
           
           ([| "BUNDY, TERESSA, 88"; "SMITH, ALLAN, 70"; "KING, MADISON, 88"; "SMITH, FRANCIS, 85"; "SMITH, ERIN, 85" |], 
            [| "BUNDY, TERESSA, 88"; "KING, MADISON, 88"; "SMITH, ERIN, 85"; "SMITH, FRANCIS, 85"; "SMITH, ALLAN, 70" |]) |]
    
    [<TestCaseSource("ProcessLinesData")>]
    static member processLines (processLinesData : string [] * string []) = 
        let input, expected = processLinesData
        Assert.AreEqual(expected, SortFile.processLines input)
