using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace DllLectionConsole
{
    public class LibraryImport
    {
        [DllImport("DllLection", CallingConvention=CallingConvention.Cdecl)]
        public static extern int add(int a, int b);

        [DllImport("DllLection", CallingConvention = CallingConvention.Cdecl)]
        public static extern int average(int[] arr, int length);

        [DllImport("DllLection", CallingConvention = CallingConvention.Cdecl)]
        public static extern int struct_sum(Number a, Number b);

    }

    public struct Number
    {
        public int num;
    }
}
