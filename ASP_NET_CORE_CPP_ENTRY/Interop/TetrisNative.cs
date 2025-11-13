using ASP_NET_CORE_CPP_ENTRY.Services;
using System;
using System.Runtime.InteropServices;

namespace Pruebas.Cliente.Interop
{ 
    public static class TetrisNative
    {

        private const string DLL_NAME = @"TensorFlowAppCPP.dll"; // Or libtictactoe.so
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr Tetris_CreateGame();

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern void Tetris_DestroyGame(IntPtr game);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern void Tetris_GetBoardState(
            IntPtr game,
            [Out] int[] board,      // 200 elements
            out int score,
            out int lines,
            out int level,
            out int nextPiece,
            out bool gameOver
        );

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern void Tetris_StepAI(IntPtr game);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern void Tetris_ResetGame(IntPtr game);

        [DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool Tetris_LoadModel(IntPtr game, string filename);


    }
}