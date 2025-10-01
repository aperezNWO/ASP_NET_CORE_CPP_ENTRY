// Controllers/TicTacToeController.cs
using Microsoft.AspNetCore.Mvc;
using Pruebas.Cliente.Interop; // Reference the unsafe interop
using System.Text.Json;

namespace Pruebas.Cliente.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public unsafe class TicTacToeController : ControllerBase
    {
        [HttpGet("play")]
        public IActionResult Play()
        {
            if (!TicTacToeNative.TryPlayGame(out var nativeResult))
            {
                return StatusCode(500, new { error = "Failed to run AI game." });
            }

            // Safely extract data

            var finalBoard = CopyFixedArray(nativeResult.FinalBoard, 9);
            var moves = CopyFixedArray(nativeResult.Moves, nativeResult.MoveCount);
            var history = ExtractHistory(nativeResult.History, nativeResult.HistoryCount);


            return Ok(new
            {
                finalBoard,
                moves,
                winner = nativeResult.Winner,
                moveCount = nativeResult.MoveCount,
                history
            });
        }

        // === Helper Functions (must be unsafe) ===
        private static int[] CopyFixedArray(int* ptr, int length)
        {
            var arr = new int[length];
            for (int i = 0; i < length; ++i)
                arr[i] = ptr[i];
            return arr;
        }

        private static List<int[]> ExtractHistory(int* historyPtr, int count)
        {
            var history = new List<int[]>();
            for (int s = 0; s < count && s < 10; ++s)
            {
                var state = new int[9];
                int offset = s * 9;
                for (int i = 0; i < 9; ++i)
                    state[i] = historyPtr[offset + i];
                history.Add(state);
            }
            return history;
        }
    }
}