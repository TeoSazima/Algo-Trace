using AlgoTrace2.Models;

namespace AlgoTrace2.Services
{
    public class AlgorithmService
    {
        public static List<AlgorithmData> algorithms => new()
        {
            // --- BUBBLE SORT ---
            new AlgorithmData
            {
                Slug = "bubble",
                Name = "Bubble Sort",
                Description = "Jednoduchý řadicí algoritmus, který opakovaně prochází seznam a prohazuje sousední prvky, pokud jsou ve špatném pořadí.",
                TimeComplexityBest = "O(N)",
                TimeComplexityAverage = "O(N²)",
                TimeComplexityWorst = "O(N²)",
                SpaceComplexity = "O(1)",
                Pseudocode = new()
                {
                    new PseudocodeLine { Text = "procedure bubbleSort(A : list of sortable items)", IsKeyword = true },
                    new PseudocodeLine { Text = "    n := length(A)", IsComment = false },
                    new PseudocodeLine { Text = "    repeat", IsKeyword = true },
                    new PseudocodeLine { Text = "        swapped := false", IsComment = false },
                    new PseudocodeLine { Text = "        for i := 1 to n-1 inclusive do", IsKeyword = true },
                    new PseudocodeLine { Text = "            if A[i-1] > A[i] then", IsKeyword = true },
                    new PseudocodeLine { Text = "                swap(A[i-1], A[i])", IsComment = false },
                    new PseudocodeLine { Text = "                swapped := true", IsComment = false }
                },
                LogicFlow = new()
                {
                    new LogicStep { Number = 1, Title = "Pointer Initialization", Description = "Nastavení ukazatelů na začátek pole." },
                    new LogicStep { Number = 2, Title = "Compare Adjacent Elements", Description = "Porovnání dvou sousedních prvků (A[i] > A[i+1])." },
                    new LogicStep { Number = 3, Title = "Conditional Swap", Description = "Pokud je levý prvek větší než pravý, prohodí se." }
                }
            },

            // --- SELECTION SORT ---
            new AlgorithmData
            {
                Slug = "selection",
                Name = "Selection Sort",
                Description = "Algoritmus pracuje tak, že najde nejmenší prvek v neseřazené části pole a vymění ho s prvním prvkem.",
                TimeComplexityBest = "O(N²)",
                TimeComplexityAverage = "O(N²)",
                TimeComplexityWorst = "O(N²)",
                SpaceComplexity = "O(1)",
                Pseudocode = new()
                {
                    new PseudocodeLine { Text = "procedure selectionSort(A : list of sortable items)", IsKeyword = true },
                    new PseudocodeLine { Text = "    for i := 0 to n-1 do", IsKeyword = true },
                    new PseudocodeLine { Text = "        min_idx := i", IsComment = false },
                    new PseudocodeLine { Text = "        for j := i+1 to n do", IsKeyword = true },
                    new PseudocodeLine { Text = "            if A[j] < A[min_idx] then min_idx := j", IsKeyword = true }
                },
                LogicFlow = new()
                {
                    new LogicStep { Number = 1, Title = "Find Minimum", Description = "Prohledání neseřazené části pole a nalezení minima." },
                    new LogicStep { Number = 2, Title = "Swap with Start", Description = "Výměna nalezeného minima s prvním prvkem neseřazené části." }
                }
            }
        };

        // Metoda, která podle slugu vrátí konkrétní algoritmus
        public AlgorithmData? GetAlgorithmBySlug(string slug)
        {
            return algorithms.FirstOrDefault(a => a.Slug.Equals(slug, StringComparison.OrdinalIgnoreCase));
        }
    }
}