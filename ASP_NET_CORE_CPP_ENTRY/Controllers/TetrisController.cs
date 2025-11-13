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
        private static int _boardWidth = 10;
        private static int _boardHeight = 20;
        private static bool _dllLoaded = false;

        static TetrisController()
        {
            try
            {
                _boardWidth = TetrisNative.TETRIS_GetBoardWidth();
                _boardHeight = TetrisNative.TETRIS_GetBoardHeight();
                _dllLoaded = true;
                Console.WriteLine("✅ DLL loaded successfully");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"❌ DLL load failed: {ex.Message}");
                _dllLoaded = false;
            }
        }

        [HttpGet("health")]
        public IActionResult HealthCheck()
        {
            return Ok(new { dllLoaded = _dllLoaded, gameCreated = _gameInstance != IntPtr.Zero });
        }

        [HttpPost("create")]
        public IActionResult CreateGame()
        {
            if (!_dllLoaded) return StatusCode(500, new { error = "DLL not loaded" });

            lock (_lock)
            {
                try
                {
                    if (_gameInstance != IntPtr.Zero)
                        TetrisNative.TETRIS_DestroyGame(_gameInstance);

                    _gameInstance = TetrisNative.TETRIS_CreateGame();
                    return _gameInstance != IntPtr.Zero
                        ? Ok(new { message = "Game created" })
                        : StatusCode(500, new { error = "Failed to create game" });
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new { error = ex.Message });
                }
            }
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
                if (_gameInstance == IntPtr.Zero)
                    return BadRequest(new { error = "Game not created" });

                TetrisNative.TETRIS_Reset(_gameInstance);
                return Ok();
            }
        }

        [HttpPost("step")]
        public IActionResult Step()
        {
            lock (_lock)
            {
                if (_gameInstance == IntPtr.Zero) return BadRequest(new { error = "Game not created" });
                TetrisNative.TETRIS_Step(_gameInstance);
                return Ok();
            }
        }

        [HttpGet("state")]
        public IActionResult GetState()
        {
            lock (_lock)
            {
                if (_gameInstance == IntPtr.Zero)
                    return BadRequest(new { error = "Game not created" });

                try
                {
                    var state = new TetrisStateDto
                    {
                        Score = TetrisNative.TETRIS_GetScore(_gameInstance),
                        Lines = TetrisNative.TETRIS_GetLines(_gameInstance),
                        Level = TetrisNative.TETRIS_GetLevel(_gameInstance),
                        NextPiece = TetrisNative.TETRIS_GetNextPiece(_gameInstance),
                        GameOver = TetrisNative.TETRIS_IsGameOver(_gameInstance) != 0
                    };

                    IntPtr matrixPtr = TetrisNative.TETRIS_GetBoardMatrix(_gameInstance);
                    if (matrixPtr == IntPtr.Zero)
                        return StatusCode(500, new { error = "Board matrix is null" });

                    int totalCells = _boardWidth * _boardHeight;
                    int[] flatMatrix = new int[totalCells];
                    Marshal.Copy(matrixPtr, flatMatrix, 0, totalCells);

                    // Convert to jagged array
                    state.BoardMatrix = new int[_boardHeight][];
                    for (int y = 0; y < _boardHeight; y++)
                    {
                        state.BoardMatrix[y] = new int[_boardWidth];
                        for (int x = 0; x < _boardWidth; x++)
                        {
                            state.BoardMatrix[y][x] = flatMatrix[y * _boardWidth + x];
                        }
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
            if (!_dllLoaded) return StatusCode(500, new { error = "DLL not loaded" });
            try
            {
                TetrisNative.TETRIS_TrainAI(request.WeightsFile, request.Generations);
                return Ok(new { message = "Training completed", file = request.WeightsFile });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpPost("load-ai")]
        public IActionResult LoadAI([FromBody] LoadAIRequest request)
        {
            lock (_lock)
            {
                if (_gameInstance == IntPtr.Zero) return BadRequest(new { error = "Game not created" });
                TetrisNative.TETRIS_LoadAI(_gameInstance, request.WeightsFile);
                return Ok();
            }
        }

        [HttpGet("ai-weights")]
        public IActionResult GetAIWeights()
        {
            lock (_lock)
            {
                if (_gameInstance == IntPtr.Zero) return BadRequest(new { error = "Game not created" });

                double[] weights = new double[4];
                TetrisNative.TETRIS_GetAIWeights(_gameInstance, weights);
                return Ok(new AIWeightsDto
                {
                    LinesWeight = weights[0],
                    HeightWeight = weights[1],
                    HolesWeight = weights[2],
                    BumpinessWeight = weights[3]
                });
            }
        }

        [HttpPost("ai-weights")]
        public IActionResult SetAIWeights([FromBody] AIWeightsDto weights)
        {
            lock (_lock)
            {
                if (_gameInstance == IntPtr.Zero) return BadRequest(new { error = "Game not created" });

                double[] weightArray = { weights.LinesWeight, weights.HeightWeight, weights.HolesWeight, weights.BumpinessWeight };
                TetrisNative.TETRIS_SetAIWeights(_gameInstance, weightArray);
                return Ok();
            }
        }

        [HttpPost("toggle-auto-play")]
        public IActionResult ToggleAutoPlay()
        {
            lock (_lock)
            {
                if (_gameInstance == IntPtr.Zero) return BadRequest(new { error = "Game not created" });
                TetrisNative.TETRIS_ToggleAutoPlay(_gameInstance);
                return Ok();
            }
        }
    }
}

