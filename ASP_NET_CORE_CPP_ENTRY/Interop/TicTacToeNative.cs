// Interop/TicTacToeNative.cs
using System.Runtime.InteropServices;

namespace Pruebas.Cliente.Interop
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct TicTacToeResult // ← This struct uses fixed buffers → needs unsafe
    {
        public fixed int FinalBoard[9];
        public fixed int Moves[9];
        public int Winner;
        public int MoveCount;
        public fixed int History[90]; // 10 states × 9 cells
        public int HistoryCount;
    }

    public static class TicTacToeNative
    {
        private const string DllName = @"TensorFlowAppCPP.dll";

        [DllImport(DllName, EntryPoint = "PlayTicTacToeGameWithHistory", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool PlayGame(ref TicTacToeResult result);

        public static bool TryPlayGame(out TicTacToeResult result)
        {
            result = new TicTacToeResult();
            try
            {
                return PlayGame(ref result);
            }
            catch
            {
                return false;
            }
        }
    }
}