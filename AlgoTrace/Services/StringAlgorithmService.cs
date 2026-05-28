using AlgoTrace.Models;

namespace AlgoTrace.Services
{
    public class StringAlgorithmService
    {
        public static List<AlgorithmData> algorithms => new()
        {
            new AlgorithmData
            {
                Slug = "naive",
                Name = "Naive Search",
                Description = "The simplest string search algorithm. It slides the pattern over the text one position at a time and checks each window for a match. Simple but inefficient for large inputs.",
                TimeComplexityBest = "O(N)",
                TimeComplexityAverage = "O(N·M)",
                TimeComplexityWorst = "O(N·M)",
                SpaceComplexity = "O(1)",
                Pseudocode = new()
                {
                    new PseudocodeLine { Text = "procedure naiveSearch(text, pattern)", IsKeyword = true },
                    new PseudocodeLine { Text = "    for i := 0 to len(text) - len(pattern) do", IsKeyword = true },
                    new PseudocodeLine { Text = "        match := true", IsComment = false },
                    new PseudocodeLine { Text = "        for j := 0 to len(pattern) - 1 do", IsKeyword = true },
                    new PseudocodeLine { Text = "            if text[i+j] != pattern[j] then", IsKeyword = true },
                    new PseudocodeLine { Text = "                match := false; break", IsComment = false },
                    new PseudocodeLine { Text = "        if match then report(i)", IsKeyword = true }
                },
                LogicFlow = new()
                {
                    new LogicStep { Number = 1, Title = "Initialize", Description = "Start at position 0 of the text." },
                    new LogicStep { Number = 2, Title = "Compare Window", Description = "Compare each character of the pattern against the current window in the text." },
                    new LogicStep { Number = 3, Title = "Shift on Mismatch", Description = "On mismatch, shift window one position to the right." },
                    new LogicStep { Number = 4, Title = "Report Match", Description = "If all pattern characters matched, record the position." }
                }
            },

            new AlgorithmData
            {
                Slug = "kmp",
                Name = "KMP Search",
                Description = "Knuth-Morris-Pratt algorithm pre-processes the pattern to build a failure function table. On mismatch, it uses this table to skip characters instead of backtracking — achieving linear time.",
                TimeComplexityBest = "O(N)",
                TimeComplexityAverage = "O(N+M)",
                TimeComplexityWorst = "O(N+M)",
                SpaceComplexity = "O(M)",
                Pseudocode = new()
                {
                    new PseudocodeLine { Text = "procedure kmpSearch(text, pattern)", IsKeyword = true },
                    new PseudocodeLine { Text = "    lps := buildFailureTable(pattern)  // O(M)", IsComment = true },
                    new PseudocodeLine { Text = "    i := 0; j := 0", IsComment = false },
                    new PseudocodeLine { Text = "    while i < len(text) do", IsKeyword = true },
                    new PseudocodeLine { Text = "        if text[i] == pattern[j] then i++; j++", IsKeyword = true },
                    new PseudocodeLine { Text = "        else if j > 0 then j := lps[j-1]", IsKeyword = true },
                    new PseudocodeLine { Text = "        else i++", IsComment = false },
                    new PseudocodeLine { Text = "        if j == len(pattern) then report(i-j)", IsKeyword = true }
                },
                LogicFlow = new()
                {
                    new LogicStep { Number = 1, Title = "Build Failure Table", Description = "Pre-process pattern to compute the LPS (Longest Proper Prefix Suffix) array." },
                    new LogicStep { Number = 2, Title = "Advance on Match", Description = "When text[i] matches pattern[j], advance both pointers." },
                    new LogicStep { Number = 3, Title = "Jump on Mismatch", Description = "Use LPS table to jump j without moving i backwards in text." },
                    new LogicStep { Number = 4, Title = "Report & Continue", Description = "Record match position and resume using the failure table." }
                }
            },

            new AlgorithmData
            {
                Slug = "rabin-karp",
                Name = "Rabin-Karp",
                Description = "Uses rolling hash to compare the pattern hash against each window hash. Only performs character-by-character verification on hash matches, allowing near O(N) average performance.",
                TimeComplexityBest = "O(N+M)",
                TimeComplexityAverage = "O(N+M)",
                TimeComplexityWorst = "O(N·M)",
                SpaceComplexity = "O(1)",
                Pseudocode = new()
                {
                    new PseudocodeLine { Text = "procedure rabinKarp(text, pattern)", IsKeyword = true },
                    new PseudocodeLine { Text = "    ph := hash(pattern)", IsComment = false },
                    new PseudocodeLine { Text = "    wh := hash(text[0..m-1])", IsComment = false },
                    new PseudocodeLine { Text = "    for i := 0 to n - m do", IsKeyword = true },
                    new PseudocodeLine { Text = "        if wh == ph then          // hash match", IsComment = true },
                    new PseudocodeLine { Text = "            if text[i..i+m] == pattern then report(i)", IsKeyword = true },
                    new PseudocodeLine { Text = "        wh := rollHash(wh, text[i], text[i+m])", IsComment = false }
                },
                LogicFlow = new()
                {
                    new LogicStep { Number = 1, Title = "Compute Hashes", Description = "Compute hash of pattern and first window of text using polynomial rolling hash." },
                    new LogicStep { Number = 2, Title = "Compare Hashes", Description = "If hashes differ, roll the window hash by removing first char and adding next." },
                    new LogicStep { Number = 3, Title = "Verify on Match", Description = "Hash match found — verify character by character to rule out collisions." },
                    new LogicStep { Number = 4, Title = "Report Match", Description = "Character verification passed — record the match position." }
                }
            }
        };

        public AlgorithmData? GetBySlug(string slug) =>
            algorithms.FirstOrDefault(a => a.Slug.Equals(slug, StringComparison.OrdinalIgnoreCase));
    }
}
