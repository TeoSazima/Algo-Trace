using AlgoTrace.Models;

namespace AlgoTrace.Services
{
    public class AlgorithmService
    {
        public static List<AlgorithmData> algorithms => new()
        {
            // =================================================================
            // BASIC AND SIMPLE ALGORITHMS (O(n²))
            // =================================================================

            // --- BUBBLE SORT ---
            new AlgorithmData
            {
                Slug = "bubble",
                Name = "Bubble Sort",
                Description = "A simple sorting algorithm that repeatedly traverses the list and swaps adjacent elements if they are in the wrong order. The largest elements gradually 'bubble up' to the end.",
                TimeComplexityBest = "O(N)",
                TimeComplexityAverage = "O(N²)",
                TimeComplexityWorst = "O(N²)",
                SpaceComplexity = "O(1)",
                Pseudocode = new()
                {
                    new PseudocodeLine { Text = "procedure bubbleSort(A : list of sortable items)", IsKeyword = true },
                    new PseudocodeLine { Text = "    n := length(A)", IsComment = true },
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
                    new LogicStep { Number = 3, Title = "Conditional Swap", Description = "If the left element is greater than the right one, they are swapped." }
                }
            },

            // --- SELECTION SORT ---
            new AlgorithmData
            {
                Slug = "selection",
                Name = "Selection Sort",
                Description = "The algorithm works by finding the smallest element in the unsorted part of the array and swapping it with the first element of this unsorted part.",
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
            },

            // --- INSERTION SORT ---
            new AlgorithmData
            {
                Slug = "insertion",
                Name = "Insertion Sort",
                Description = "Gradually takes elements from the unsorted part and inserts them into the correct position in the already sorted left part of the array. Similar to how you sort cards in your hand.",
                TimeComplexityBest = "O(N)",
                TimeComplexityAverage = "O(N²)",
                TimeComplexityWorst = "O(N²)",
                SpaceComplexity = "O(1)",
                Pseudocode = new()
                {
                    new PseudocodeLine { Text = "procedure insertionSort(A : list of sortable items)", IsKeyword = true },
                    new PseudocodeLine { Text = "    for i := 1 to length(A) - 1 do", IsKeyword = true },
                    new PseudocodeLine { Text = "        key := A[i]", IsComment = false },
                    new PseudocodeLine { Text = "        j := i - 1", IsComment = false },
                    new PseudocodeLine { Text = "        while j >= 0 and A[j] > key do", IsKeyword = true },
                    new PseudocodeLine { Text = "            A[j + 1] := A[j]", IsComment = false },
                    new PseudocodeLine { Text = "            j := j - 1", IsComment = false },
                    new PseudocodeLine { Text = "        A[j + 1] := key", IsComment = false }
                },
                LogicFlow = new()
                {
                    new LogicStep { Number = 1, Title = "Pick Key Element", Description = "Selecting the current element as the 'key' that will be inserted." },
                    new LogicStep { Number = 2, Title = "Shift Elements", Description = "Shifting larger elements in the sorted part to the right until the correct position is found." },
                    new LogicStep { Number = 3, Title = "Insert Key", Description = "Inserting the key into its new, correct position in the sorted segment." }
                }
            },

            // =================================================================
            // EFFICIENT ALGORITHMS (O(n log n))
            // =================================================================

            // --- QUICK SORT ---
            new AlgorithmData
            {
                Slug = "quick",
                Name = "Quick Sort",
                Description = "A highly efficient algorithm based on the 'Divide and Conquer' principle. It selects an element called the pivot and splits the array into elements smaller and larger than the pivot. Then it recursively calls itself on both halves.",
                TimeComplexityBest = "O(N log N)",
                TimeComplexityAverage = "O(N log N)",
                TimeComplexityWorst = "O(N²)",
                SpaceComplexity = "O(log N)",
                Pseudocode = new()
                {
                    new PseudocodeLine { Text = "procedure quickSort(A, low, high)", IsKeyword = true },
                    new PseudocodeLine { Text = "    if low < high then", IsKeyword = true },
                    new PseudocodeLine { Text = "        p := partition(A, low, high)", IsComment = false },
                    new PseudocodeLine { Text = "        quickSort(A, low, p - 1)", IsComment = false },
                    new PseudocodeLine { Text = "        quickSort(A, p + 1, high)", IsComment = false }
                },
                LogicFlow = new()
                {
                    new LogicStep { Number = 1, Title = "Pivot Selection", Description = "Choosing a reference element (pivot) from the current segment of the array." },
                    new LogicStep { Number = 2, Title = "Partitioning", Description = "Rearranging elements – those smaller than the pivot go to the left, larger ones go to the right." },
                    new LogicStep { Number = 3, Title = "Recursive Call", Description = "Independently running the same process for the left and right parts around the pivot." }
                }
            }
        };

        public AlgorithmData? GetAlgorithmBySlug(string slug)
        {
            return algorithms.FirstOrDefault(a => a.Slug.Equals(slug, StringComparison.OrdinalIgnoreCase));
        }
    }
}
