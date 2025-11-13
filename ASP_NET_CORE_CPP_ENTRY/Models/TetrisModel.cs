namespace ASP_NET_CORE_CPP_ENTRY.Models
{
    public class TetrisStateDto
    {
        public int Score { get; set; }
        public int Lines { get; set; }
        public int Level { get; set; }
        public int NextPiece { get; set; }
        public bool GameOver { get; set; }

        // Jagged array - array of arrays for Angular
        public int[][] BoardMatrix { get; set; }

        public TetrisStateDto()
        {
            // Initialize with 20 rows x 10 columns
            BoardMatrix = new int[20][];
            for (int i = 0; i < 20; i++)
            {
                BoardMatrix[i] = new int[10];
            }
        }
    }

    public class AIWeightsDto
    {
        public double LinesWeight { get; set; }
        public double HeightWeight { get; set; }
        public double HolesWeight { get; set; }
        public double BumpinessWeight { get; set; }
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
