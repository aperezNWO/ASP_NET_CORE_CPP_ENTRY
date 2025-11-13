namespace ASP_NET_CORE_CPP_ENTRY.Models
{
    public class TetrisStateDto
    {
        public int Score { get; set; }
        public int Lines { get; set; }
        public int Level { get; set; }
        public int NextPiece { get; set; }
        public bool GameOver { get; set; }
        // Single flat array - much faster to serialize
        public int[] BoardMatrix { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public TetrisStateDto(int width, int height)
        {
            Width = width;
            Height = height;
            BoardMatrix = new int[width * height];
        }
    }


    public class AiWeightsDto
    {
        public double LinesWeight { get; set; }
        public double HeightWeight { get; set; }
        public double HolesWeight { get; set; }
        public double BumpinessWeight { get; set; }
    }
}
