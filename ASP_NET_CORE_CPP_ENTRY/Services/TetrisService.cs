namespace ASP_NET_CORE_CPP_ENTRY.Services
{
    public class TetrisService : IDisposable
    {
        private IntPtr _gameInstance;
        private bool _disposed = false;

        public TetrisService()
        {
            _gameInstance = Pruebas.Cliente.Interop.TetrisNative.Tetris_CreateGame();

            // Auto-load model if exists
            string modelPath = Path.Combine(Directory.GetCurrentDirectory(), "tetris_weights.txt");
            if (File.Exists(modelPath))
            {
                if (!Pruebas.Cliente.Interop.TetrisNative.Tetris_LoadModel(_gameInstance, modelPath))
                {
                    Console.WriteLine("Warning: Failed to load model. Using random weights.");
                }
            }
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                Pruebas.Cliente.Interop.TetrisNative.Tetris_DestroyGame(_gameInstance);
                _disposed = true;
            }
        }

        public TetrisState GetState()
        {
            var board = new int[200];
            Pruebas.Cliente.Interop.TetrisNative.Tetris_GetBoardState(
                _gameInstance,
                board,
                out int score,
                out int lines,
                out int level,
                out int nextPiece,
                out bool gameOver
            );

            return new TetrisState
            {
                Board = board,
                Score = score,
                Lines = lines,
                Level = level,
                NextPiece = nextPiece,
                GameOver = gameOver
            };
        }

        public void Step()                 => Pruebas.Cliente.Interop.TetrisNative.Tetris_StepAI(_gameInstance);
        public void Reset()                => Pruebas.Cliente.Interop.TetrisNative.Tetris_ResetGame(_gameInstance);
        public bool LoadModel(string path) => Pruebas.Cliente.Interop.TetrisNative.Tetris_LoadModel(_gameInstance, path);

        public class TetrisState
        {
            public int[] Board { get; set; }
            public int Score { get; set; }
            public int Lines { get; set; }
            public int Level { get; set; }
            public int NextPiece { get; set; }
            public bool GameOver { get; set; }
        }
    }
}
