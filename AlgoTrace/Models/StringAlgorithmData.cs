namespace AlgoTrace.Models
{
    public class StringStep
    {
        public string Text { get; set; } = string.Empty;
        public string Pattern { get; set; } = string.Empty;
        public int TextIndex { get; set; } = -1;
        public int PatternIndex { get; set; } = -1;
        public bool IsMatch { get; set; }
        public bool IsMismatch { get; set; }
        public bool IsFound { get; set; }
        public List<int> FoundPositions { get; set; } = new();
        public int WindowStart { get; set; } = -1;
        public int LogicFlowIndex { get; set; } = 0;
        public string StatusMessage { get; set; } = string.Empty;
        // KMP specific
        public int[]? KmpTable { get; set; }
        // Rabin-Karp specific
        public long TextHash { get; set; } = -1;
        public long PatternHash { get; set; } = -1;
    }
}