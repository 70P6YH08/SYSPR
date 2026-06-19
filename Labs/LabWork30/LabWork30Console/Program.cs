using LabWork30Console;

Console.WriteLine(LibraryImport.is_simple(5));

Console.WriteLine();

Console.WriteLine(LibraryImport.count_simple_nums([14, 2, 23, 4, 2, 3, 1, 0, 4], 9));

var f = new Point { x = 2, y = -3 };
var s = new Point { x = 9, y = -8 };

Console.WriteLine();

Console.WriteLine(LibraryImport.hypotenuse(f, s));