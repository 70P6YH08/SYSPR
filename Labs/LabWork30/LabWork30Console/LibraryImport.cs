using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace LabWork30Console
{
    public class LibraryImport
    {
        [DllImport("LabWork30Library", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool is_simple(int num);

        [DllImport("LabWork30Library", CallingConvention = CallingConvention.Cdecl)]
        public static extern int count_simple_nums(int[] arr, int length);

        [DllImport("LabWork30Library", CallingConvention = CallingConvention.Cdecl)]
        public static extern double hypotenuse(Point first, Point second);

    }

    public struct Point
    {
        public double x;
        public double y;
    }
}
