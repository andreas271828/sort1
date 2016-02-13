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

[<TestCase("Test.txt", "Test-sorted.txt")>]
[<TestCase("test", "test-sorted.txt")>]
let outputPath input expected = Assert.AreEqual(expected, SortFile.outputPath input)

[<TestCase([| "HUEMER, ANDREAS, 42"; "HUEMER, ANDREAS, 42" |], [| "HUEMER, ANDREAS, 42"; "HUEMER, ANDREAS, 42" |])>]
[<TestCase([| "HUEMER, ANDREAS, 42"; "HUEMER, ANDREAS, 45" |], [| "HUEMER, ANDREAS, 45"; "HUEMER, ANDREAS, 42" |])>]
let processLines input expected = Assert.AreEqual(expected, SortFile.processLines input)
