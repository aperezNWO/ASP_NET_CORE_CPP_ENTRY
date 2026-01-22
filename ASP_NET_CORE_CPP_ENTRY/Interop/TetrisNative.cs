using ASP_NET_CORE_CPP_ENTRY;
using ASP_NET_CORE_CPP_ENTRY.Interop;
using System;
using System.Runtime.InteropServices;

namespace Pruebas.Cliente.Interop
{ 
    public class TetrisNative : TensorFlowNative
    {
        // ==================== Constants ====================
        [DllImport(tensorFlowDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int TETRIS_GetBoardWidth();

        [DllImport(tensorFlowDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int TETRIS_GetBoardHeight();

        // ==================== Game Session ====================
        public delegate IntPtr TETRIS_CreateGameDelegate();
        [DllImport(tensorFlowDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr TETRIS_CreateGame();

        [DllImport(tensorFlowDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void TETRIS_DestroyGame(IntPtr game);

        // ==================== Game Control ====================
        [DllImport(tensorFlowDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void TETRIS_Reset(IntPtr game);

        [DllImport(tensorFlowDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void TETRIS_Step(IntPtr game);

        [DllImport(tensorFlowDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void TETRIS_ToggleAutoPlay(IntPtr game);

        // ==================== State Query ====================
        [DllImport(tensorFlowDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int TETRIS_GetScore(IntPtr game);

        [DllImport(tensorFlowDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int TETRIS_GetLines(IntPtr game);

        [DllImport(tensorFlowDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int TETRIS_GetLevel(IntPtr game);

        [DllImport(tensorFlowDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int TETRIS_GetNextPiece(IntPtr game);

        [DllImport(tensorFlowDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int TETRIS_IsGameOver(IntPtr game);

        // Returns flat pointer to 2D array
        [DllImport(tensorFlowDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr TETRIS_GetBoardMatrix(IntPtr game);

        [DllImport(tensorFlowDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr TETRIS_GetBoardMatrixWithPreview(IntPtr game);

        // ==================== AI Functions ====================
        [DllImport(tensorFlowDllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern void TETRIS_TrainAI([MarshalAs(UnmanagedType.LPStr)] string weightsFile, int generations);

        [DllImport(tensorFlowDllName, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern void TETRIS_LoadAI(IntPtr game, [MarshalAs(UnmanagedType.LPStr)] string weightsFile);

        // Helper to get AI weights
        [DllImport(tensorFlowDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void TETRIS_GetAIWeights(IntPtr game, double[] weightsOut);

        [DllImport(tensorFlowDllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void TETRIS_SetAIWeights(IntPtr game, double[] weightsIn);
    }
}