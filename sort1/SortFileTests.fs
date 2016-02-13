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
