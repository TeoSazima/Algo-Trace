using AlgoTrace.Models;

namespace AlgoTrace.Services
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
                Description = "A simple sorting algorithm that repeatedly passes through the list and swaps adjacent elements if they are in the wrong order.",
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
                    new LogicStep { Number = 1, Title = "Pointer Initialization", Description = "Setting the pointers to the beginning of the array." },
                    new LogicStep { Number = 2, Title = "Compare Adjacent Elements", Description = "Comparing two adjacent elements (A[i] > A[i+1])." },
                    new LogicStep { Number = 3, Title = "Conditional Swap", Description = "If the left element is larger than the right one, they are swapped." }
                }
            },

            // --- SELECTION SORT ---
            new AlgorithmData
            {
                Slug = "selection",
                Name = "Selection Sort",
                Description = "The algorithm works by finding the smallest element in the unsorted part of the array and swapping it with the first element.",
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
                    new LogicStep { Number = 1, Title = "Find Minimum", Description = "Searching the unsorted part of the array and finding the minimum." },
                    new LogicStep { Number = 2, Title = "Swap with Start", Description = "Swapping the found minimum with the first element of the unsorted part." }
                }
            }
        };

        // Method that returns a specific algorithm based on its slug
        public AlgorithmData? GetAlgorithmBySlug(string slug)
        {
            return algorithms.FirstOrDefault(a => a.Slug.Equals(slug, StringComparison.OrdinalIgnoreCase));
        }
    }
}
