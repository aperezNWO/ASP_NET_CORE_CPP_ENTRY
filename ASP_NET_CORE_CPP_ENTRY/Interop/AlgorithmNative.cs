using System.Runtime.InteropServices;

namespace ASP_NET_CORE_CPP_ENTRY.Interop
{
    public class AlgorithmNative
    {

        const string dll_Algorithm = @"Algorithm.dll";

        // DIJKSTRA
        [DllImport(dll_Algorithm, EntryPoint = @"Dijkstra_GenerateRandomVertex_CPP", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr _Dijkstra_GenerateRandomVertex_CPP(int p_vertexSize, int sourcePoint);

        // SORTBENCHMARK - HTML
        [DllImport(dll_Algorithm, EntryPoint = @"SortBenchMark_GetSort_CPP", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr SortBenchMark_GetSort_CPP(ushort p_sortAlgoritm, string p_unsortedList);

        // SORTBENCHMARK - JSON
        [DllImport(dll_Algorithm, EntryPoint = @"SortBenchMark_GetSort_CPP_JSON", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr SortBenchMark_GetSort_CPP_JSON(ushort p_sortAlgoritm, string p_unsortedList);

        // REGEX
        [DllImport(dll_Algorithm, EntryPoint = @"RegExManager_RegExEval", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr RegExManager_RegExEval(string p_tagSearch, string p_textSearch);

        // SUDOKU
        [DllImport(dll_Algorithm, EntryPoint = @"Sudoku_Generate_CPP", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr _Sudoku_Generate_CPP();

        // SUDOKU
        [DllImport(dll_Algorithm, EntryPoint = @"Sudoku_Solve_CPP", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr _Sudoku_Solve_CPP(string p_matrix);

        // GET DLL VERSION
        [DllImport(dll_Algorithm, EntryPoint = @"GetDLLVersion", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr _GetDLLVersion();

        // GET C++ STD VERSIO
        [DllImport(dll_Algorithm, EntryPoint = @"GetCPPSTDVersion", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr _GetCPPSTDVersion();

    }
}
