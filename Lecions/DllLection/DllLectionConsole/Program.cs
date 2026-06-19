using DllLectionConsole;

Console.WriteLine(LibraryImport.add(5,2));
Console.WriteLine(LibraryImport.average([14,2,23,4,2], 5));

var s1 = new Number { num = 1 };
var s2 = new Number { num = 2 };

Console.WriteLine(LibraryImport.struct_sum(s1, s2));