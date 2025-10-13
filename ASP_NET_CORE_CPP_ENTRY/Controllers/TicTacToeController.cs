// Controllers/TicTacToeController.cs
using Microsoft.AspNetCore.Mvc;
using Pruebas.Cliente.Interop;
using System.Text.Json;

namespace Pruebas.Cliente.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public unsafe class TicTacToeController : ControllerBase
    {
        [HttpGet("play")]
        public IActionResult Play(
            [FromQuery] int aiMode = 0,
            [FromQuery] double? temperature = null)
        {
            double temp = temperature ?? ((aiMode == 1) ? 1.5 : 0.1);

            if (!TicTacToeNative.TryPlayGame(aiMode, temp, out var nativeResult))
            {
                return StatusCode(500, new { error = "Failed to run AI game." });
            }

            // ✅ Use fixed blocks to safely extract arrays
            var finalBoard = CopyArray(nativeResult.finalBoard);
            var moves = CopyMoves(nativeResult.moves, nativeResult.moveCount);
            var history = ExtractHistory(nativeResult.history, nativeResult.historyCount);

            return Ok(new
            {
                finalBoard,
                moves,
                winner = nativeResult.winner switch
                {
                    1 => "X",
                    -1 => "O",
                    _ => "Draw"
                },
                moveCount = nativeResult.moveCount,
                historyCount = nativeResult.historyCount,
                history
            });
        }

        // === Helper Functions Using 'fixed' ===
        private static int[] CopyArray(int* source) // receives decayed fixed buffer
        {
            var arr = new int[9];
            for (int i = 0; i < 9; ++i)
                arr[i] = source[i];
            return arr;
        }

        private static int[] CopyMoves(int* source, int count)
        {
            var arr = new int[count];
            for (int i = 0; i < count; ++i)
                arr[i] = source[i];
            return arr;
        }

        private static List<int[]> ExtractHistory(int* historyPtr, int count)
        {
            var history = new List<int[]>();
            int statesToExtract = Math.Min(count, 10);

            for (int s = 0; s < statesToExtract; ++s)
            {
                var state = new int[9];
                int offset = s * 9;
                for (int i = 0; i < 9; ++i)
                {
                    state[i] = historyPtr[offset + i];
                }
                history.Add(state);
            }
            return history;
        }
    }
}