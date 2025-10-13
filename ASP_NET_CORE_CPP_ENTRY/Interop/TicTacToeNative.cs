// Interop/TicTacToeNative.cs
using System;
using System.Runtime.InteropServices;

namespace Pruebas.Cliente.Interop
{
    public static class TicTacToeNative
    {
        private const string DllName = @"TensorFlowAppCPP.dll"; // Or libtictactoe.so

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct TicTacToeResultOnline
        {
            public fixed int finalBoard[9];     // Not int*, but fixed array
            public fixed int moves[9];          // Same here
            public int winner;
            public int moveCount;
            public fixed int history[90];       // 10 x 9 board states
            public int historyCount;
        }

        [DllImport(DllName, EntryPoint = "PlayTicTacToeGameWithHistory", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool PlayGameInternal(
            ref TicTacToeResultOnline result,
            int aiMode,
            double temperature);

        public static bool TryPlayGame(int aiMode, double temperature, out TicTacToeResultOnline result)
        {
            result = new TicTacToeResultOnline();
            try
            {
                return PlayGameInternal(ref result, aiMode, temperature);
            }
            catch (DllNotFoundException)
            {
                Console.WriteLine($"❌ DLL '{DllName}' not found.");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error: {ex.Message}");
                return false;
            }
        }
    }
}