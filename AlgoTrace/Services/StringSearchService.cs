using AlgoTrace.Models;

namespace AlgoTrace.Services
{
    public class StringSearchService
    {
        public List<StringStep> GenerateSteps(string slug, string text, string pattern)
        {
            if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(pattern))
                return new List<StringStep>();

            return slug.ToLower() switch
            {
                "kmp" => KmpSearch(text, pattern),
                "rabin-karp" => RabinKarpSearch(text, pattern),
                _ => NaiveSearch(text, pattern)
            };
        }

        private List<StringStep> NaiveSearch(string text, string pattern)
        {
            var steps = new List<StringStep>();
            var found = new List<int>();

            steps.Add(new StringStep
            {
                Text = text,
                Pattern = pattern,
                WindowStart = 0,
                TextIndex = -1,
                PatternIndex = -1,
                LogicFlowIndex = 1,
                StatusMessage = "Starting naive search..."
            });

            for (int i = 0; i <= text.Length - pattern.Length; i++)
            {
                steps.Add(new StringStep
                {
                    Text = text,
                    Pattern = pattern,
                    WindowStart = i,
                    TextIndex = i,
                    PatternIndex = 0,
                    LogicFlowIndex = 2,
                    StatusMessage = $"Aligning pattern at position {i}"
                });

                int j = 0;
                bool matched = true;
                while (j < pattern.Length)
                {
                    bool charMatch = text[i + j] == pattern[j];
                    steps.Add(new StringStep
                    {
                        Text = text,
                        Pattern = pattern,
                        WindowStart = i,
                        TextIndex = i + j,
                        PatternIndex = j,
                        IsMatch = charMatch,
                        IsMismatch = !charMatch,
                        FoundPositions = new List<int>(found),
                        LogicFlowIndex = charMatch ? 2 : 3,
                        StatusMessage = charMatch
                            ? $"Match: '{text[i + j]}' == '{pattern[j]}'"
                            : $"Mismatch: '{text[i + j]}' != '{pattern[j]}', shifting window"
                    });
                    if (!charMatch) { matched = false; break; }
                    j++;
                }

                if (matched)
                {
                    found.Add(i);
                    steps.Add(new StringStep
                    {
                        Text = text,
                        Pattern = pattern,
                        WindowStart = i,
                        TextIndex = i,
                        PatternIndex = 0,
                        IsFound = true,
                        FoundPositions = new List<int>(found),
                        LogicFlowIndex = 4,
                        StatusMessage = $"Pattern found at index {i}!"
                    });
                }
            }

            steps.Add(new StringStep
            {
                Text = text,
                Pattern = pattern,
                WindowStart = -1,
                IsFound = found.Count > 0,
                FoundPositions = new List<int>(found),
                LogicFlowIndex = 4,
                StatusMessage = found.Count > 0
                    ? $"Search complete. Found {found.Count} occurrence(s) at: [{string.Join(", ", found)}]"
                    : "Search complete. Pattern not found."
            });

            return steps;
        }

        private List<StringStep> KmpSearch(string text, string pattern)
        {
            var steps = new List<StringStep>();
            var found = new List<int>();

            // Build failure function
            int[] lps = BuildKmpTable(pattern);

            steps.Add(new StringStep
            {
                Text = text,
                Pattern = pattern,
                WindowStart = 0,
                TextIndex = -1,
                PatternIndex = -1,
                KmpTable = lps,
                LogicFlowIndex = 1,
                StatusMessage = $"KMP failure table built: [{string.Join(", ", lps)}]"
            });

            int i = 0, j = 0;
            while (i < text.Length)
            {
                bool charMatch = text[i] == pattern[j];
                steps.Add(new StringStep
                {
                    Text = text,
                    Pattern = pattern,
                    WindowStart = i - j,
                    TextIndex = i,
                    PatternIndex = j,
                    IsMatch = charMatch,
                    IsMismatch = !charMatch,
                    KmpTable = lps,
                    FoundPositions = new List<int>(found),
                    LogicFlowIndex = charMatch ? 2 : 3,
                    StatusMessage = charMatch
                        ? $"Match: text[{i}]='{text[i]}' == pattern[{j}]='{pattern[j]}'"
                        : $"Mismatch at text[{i}]='{text[i]}' vs pattern[{j}]='{pattern[j]}'"
                });

                if (charMatch)
                {
                    i++; j++;
                    if (j == pattern.Length)
                    {
                        found.Add(i - j);
                        steps.Add(new StringStep
                        {
                            Text = text,
                            Pattern = pattern,
                            WindowStart = i - j,
                            TextIndex = i - 1,
                            PatternIndex = j - 1,
                            IsFound = true,
                            KmpTable = lps,
                            FoundPositions = new List<int>(found),
                            LogicFlowIndex = 4,
                            StatusMessage = $"Full match found at index {i - j}! Jumping by failure table."
                        });
                        j = lps[j - 1];
                    }
                }
                else
                {
                    if (j != 0)
                    {
                        int prevJ = j;
                        j = lps[j - 1];
                        steps.Add(new StringStep
                        {
                            Text = text,
                            Pattern = pattern,
                            WindowStart = i - j,
                            TextIndex = i,
                            PatternIndex = j,
                            IsMismatch = true,
                            KmpTable = lps,
                            FoundPositions = new List<int>(found),
                            LogicFlowIndex = 3,
                            StatusMessage = $"Using failure table: j {prevJ} → {j} (no backtrack on text)"
                        });
                    }
                    else
                    {
                        i++;
                    }
                }
            }

            steps.Add(new StringStep
            {
                Text = text,
                Pattern = pattern,
                WindowStart = -1,
                IsFound = found.Count > 0,
                KmpTable = lps,
                FoundPositions = new List<int>(found),
                LogicFlowIndex = 4,
                StatusMessage = found.Count > 0
                    ? $"KMP complete. Found {found.Count} match(es) at: [{string.Join(", ", found)}]"
                    : "KMP complete. Pattern not found."
            });

            return steps;
        }

        private int[] BuildKmpTable(string pattern)
        {
            int[] lps = new int[pattern.Length];
            int len = 0, i = 1;
            while (i < pattern.Length)
            {
                if (pattern[i] == pattern[len]) { lps[i++] = ++len; }
                else if (len != 0) { len = lps[len - 1]; }
                else { lps[i++] = 0; }
            }
            return lps;
        }

        private List<StringStep> RabinKarpSearch(string text, string pattern)
        {
            var steps = new List<StringStep>();
            var found = new List<int>();
            const long BASE = 31, MOD = 1_000_000_007;
            int m = pattern.Length, n = text.Length;

            // Compute BASE^(m-1) for removing leading character
            long pow = 1;
            for (int k = 0; k < m - 1; k++) pow = pow * BASE % MOD;

            // Hash left-to-right: h = s[0]*BASE^(m-1) + s[1]*BASE^(m-2) + ... + s[m-1]
            long patHash = 0, winHash = 0;
            for (int k = 0; k < m; k++)
            {
                patHash = (patHash * BASE + (pattern[k] - 'a' + 1)) % MOD;
                winHash = (winHash * BASE + (text[k] - 'a' + 1)) % MOD;
            }

            steps.Add(new StringStep
            {
                Text = text,
                Pattern = pattern,
                WindowStart = 0,
                PatternHash = patHash,
                TextHash = winHash,
                LogicFlowIndex = 1,
                StatusMessage = $"Initial hashes — Pattern: {patHash}, Window[0..{m - 1}]: {winHash}"
            });

            for (int i = 0; i <= n - m; i++)
            {
                bool hashMatch = winHash == patHash;
                steps.Add(new StringStep
                {
                    Text = text,
                    Pattern = pattern,
                    WindowStart = i,
                    TextIndex = i,
                    PatternIndex = 0,
                    IsMatch = hashMatch,
                    IsMismatch = !hashMatch,
                    PatternHash = patHash,
                    TextHash = winHash,
                    FoundPositions = new List<int>(found),
                    LogicFlowIndex = hashMatch ? 3 : 2,
                    StatusMessage = hashMatch
                        ? $"Hash match at [{i}]! Verifying characters..."
                        : $"Hash mismatch at [{i}] ({winHash} ≠ {patHash}), rolling hash..."
                });

                if (hashMatch)
                {
                    bool charOk = text.Substring(i, m) == pattern;
                    if (charOk)
                    {
                        found.Add(i);
                        steps.Add(new StringStep
                        {
                            Text = text,
                            Pattern = pattern,
                            WindowStart = i,
                            IsFound = true,
                            PatternHash = patHash,
                            TextHash = winHash,
                            FoundPositions = new List<int>(found),
                            LogicFlowIndex = 4,
                            StatusMessage = $"Character verification passed. Match at index {i}!"
                        });
                    }
                    else
                    {
                        steps.Add(new StringStep
                        {
                            Text = text,
                            Pattern = pattern,
                            WindowStart = i,
                            IsMismatch = true,
                            PatternHash = patHash,
                            TextHash = winHash,
                            FoundPositions = new List<int>(found),
                            LogicFlowIndex = 3,
                            StatusMessage = "Hash collision — character mismatch! False positive."
                        });
                    }
                }

                if (i < n - m)
                {
                    // Roll: remove leading char, shift left, add new char
                    winHash = (winHash - (text[i] - 'a' + 1) * pow % MOD + MOD) % MOD;
                    winHash = (winHash * BASE + (text[i + m] - 'a' + 1)) % MOD;
                }
            }

            steps.Add(new StringStep
            {
                Text = text,
                Pattern = pattern,
                WindowStart = -1,
                IsFound = found.Count > 0,
                FoundPositions = new List<int>(found),
                LogicFlowIndex = 4,
                StatusMessage = found.Count > 0
                    ? $"Rabin-Karp complete. Found {found.Count} match(es) at: [{string.Join(", ", found)}]"
                    : "Rabin-Karp complete. Pattern not found."
            });

            return steps;
        }

        private long ComputeHash(string s, int start, int len, int base_, long mod)
        {
            long h = 0, p = 1;
            for (int i = start; i < start + len; i++)
            {
                h = (h + (s[i] - 'a' + 1) * p) % mod;
                p = p * base_ % mod;
            }
            return h;
        }
    }
}