using ASP_NET_CORE_CPP_ENTRY.Models;
using Microsoft.AspNetCore.Mvc;
using Pruebas.Cliente.Interop;
using System.Runtime.InteropServices;

namespace ASP_NET_CORE_CPP_ENTRY.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TetrisController : ControllerBase
    {
        private static IntPtr _gameInstance = IntPtr.Zero;
        private static readonly object _lock = new object();
        private static int _boardWidth = 0;
        private static int _boardHeight = 0;

        static TetrisController()
        {
            // Get board dimensions once
            _boardWidth = TetrisNative.TETRIS_GetBoardWidth();
            _boardHeight = TetrisNative.TETRIS_GetBoardHeight();
        }

        [HttpPost("create")]
        public IActionResult CreateGame()
        {
            lock (_lock)
            {
                if (_gameInstance != IntPtr.Zero)
                {
                    TetrisNative.TETRIS_DestroyGame(_gameInstance);
                }
                _gameInstance = TetrisNative.TETRIS_CreateGame();
            }
            return Ok(new { message = "Game created" });
        }

        [HttpPost("destroy")]
        public IActionResult DestroyGame()
        {
            lock (_lock)
            {
                if (_gameInstance != IntPtr.Zero)
                {
                    TetrisNative.TETRIS_DestroyGame(_gameInstance);
                    _gameInstance = IntPtr.Zero;
                }
            }
            return NoContent();
        }

        [HttpPost("reset")]
        public IActionResult Reset()
        {
            lock (_lock)
            {
                TetrisNative.TETRIS_Reset(_gameInstance);
            }
            return Ok();
        }

        [HttpPost("step")]
        public IActionResult Step()
        {
            lock (_lock)
            {
                TetrisNative.TETRIS_Step(_gameInstance);
            }
            return Ok();
        }

        [HttpGet("state")]
        public IActionResult GetState()
        {
            lock (_lock)
            {
                if (_gameInstance == IntPtr.Zero)
                    return BadRequest(new { error = "Game not created." });

                try
                {
                    // Create DTO with dimensions
                    var state = new TetrisStateDto(_boardWidth, _boardHeight)
                    {
                        Score = TetrisNative.TETRIS_GetScore(_gameInstance),
                        Lines = TetrisNative.TETRIS_GetLines(_gameInstance),
                        Level = TetrisNative.TETRIS_GetLevel(_gameInstance),
                        NextPiece = TetrisNative.TETRIS_GetNextPiece(_gameInstance),
                        GameOver = TetrisNative.TETRIS_IsGameOver(_gameInstance) != 0
                    };

                    // Copy directly to flat array
                    IntPtr matrixPtr = TetrisNative.TETRIS_GetBoardMatrix(_gameInstance);
                    if (matrixPtr != IntPtr.Zero)
                    {
                        Marshal.Copy(matrixPtr, state.BoardMatrix, 0, state.BoardMatrix.Length);
                    }

                    return Ok(state);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new { error = ex.Message });
                }
            }
        }

        [HttpPost("train")]
        public IActionResult TrainAI([FromBody] TrainRequest request)
        {
            if (string.IsNullOrEmpty(request.WeightsFile))
            {
                request.WeightsFile = "tetris_weights.txt";
            }

            // Run training (this is blocking - consider async for production)
            TetrisNative.TETRIS_TrainAI(request.WeightsFile, request.Generations);
            return Ok(new { message = "Training completed", file = request.WeightsFile });
        }

        [HttpPost("load-ai")]
        public IActionResult LoadAI([FromBody] LoadAIRequest request)
        {
            lock (_lock)
            {
                TetrisNative.TETRIS_LoadAI(_gameInstance, request.WeightsFile);
            }
            return Ok();
        }

        [HttpGet("ai-weights")]
        public IActionResult GetAIWeights()
        {
            lock (_lock)
            {
                double[] weights = new double[4];
                TetrisNative.TETRIS_GetAIWeights(_gameInstance, weights);
                return Ok(new AiWeightsDto
                {
                    LinesWeight = weights[0],
                    HeightWeight = weights[1],
                    HolesWeight = weights[2],
                    BumpinessWeight = weights[3]
                });
            }
        }

        [HttpPost("ai-weights")]
        public IActionResult SetAIWeights([FromBody] AiWeightsDto weights)
        {
            lock (_lock)
            {
                double[] weightArray = { weights.LinesWeight, weights.HeightWeight, weights.HolesWeight, weights.BumpinessWeight };
                TetrisNative.TETRIS_SetAIWeights(_gameInstance, weightArray);
            }
            return Ok();
        }

        [HttpPost("toggle-auto-play")]
        public IActionResult ToggleAutoPlay()
        {
            lock (_lock)
            {
                TetrisNative.TETRIS_ToggleAutoPlay(_gameInstance);
            }
            return Ok();
        }
    }

    public class TrainRequest
    {
        public string WeightsFile { get; set; } = "tetris_weights.txt";
        public int Generations { get; set; } = 20;
    }

    public class LoadAIRequest
    {
        public string WeightsFile { get; set; }
    }
}

