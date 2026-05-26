namespace AlgoTrace2.Models
{
    public class AlgorithmData
    {
        public string Slug { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        // Komplexity pro live metrics
        public string TimeComplexityBest { get; set; } = string.Empty;
        public string TimeComplexityAverage { get; set; } = string.Empty;
        public string TimeComplexityWorst { get; set; } = string.Empty;
        public string SpaceComplexity { get; set; } = string.Empty;

        // Pseudokód rozdělený po řádcích (abychom je mohli zvýrazňovat)
        public List<PseudocodeLine> Pseudocode { get; set; } = new();

        // Kroky logického toku
        public List<LogicStep> LogicFlow { get; set; } = new();
    }

    public class PseudocodeLine
    {
        public string Text { get; set; } = string.Empty;
        public bool IsKeyword { get; set; }
        public bool IsComment { get; set; }
    }

    public class LogicStep
    {
        public int Number { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}