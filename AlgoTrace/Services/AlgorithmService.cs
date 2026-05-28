using AlgoTrace.Models;

namespace AlgoTrace.Services
{
    public class AlgorithmService
    {
        public static List<AlgorithmData> algorithms => new()
        {
            // =================================================================
            // ZÁKLADNÍ A JEDNODUCHÉ ALGORITMY (O(n²))
            // =================================================================

            // --- BUBBLE SORT ---
            new AlgorithmData
            {
                Slug = "bubble",
                Name = "Bubble Sort",
                Description = "Jednoduchý řadicí algoritmus, který opakovaně prochází seznam a prohazuje sousední prvky, pokud jsou ve špatném pořadí. Největší prvky postupně 'probublávají' na konec.",
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
                Description = "Algoritmus pracuje tak, že najde nejmenší prvek v neseřazené části pole a vymění ho s prvním prvkem této neseřazené části.",
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
            },

            // --- INSERTION SORT ---
            new AlgorithmData
            {
                Slug = "insertion",
                Name = "Insertion Sort",
                Description = "Postupně bere prvky z neseřazené části a vkládá (inseruje) je na správné místo v již seřazené levé části pole. Podobá se řazení karet v ruce.",
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
                    new LogicStep { Number = 1, Title = "Pick Key Element", Description = "Výběr aktuálního prvku jako 'klíče', který budeme vkládat." },
                    new LogicStep { Number = 2, Title = "Shift Elements", Description = "Posouvání větších prvků v seřazené části doprava, dokud nenajdeme správnou pozici." },
                    new LogicStep { Number = 3, Title = "Insert Key", Description = "Vložení klíče na jeho nové, správné místo v seřazeném úseku." }
                }
            },

            // =================================================================
            // EFEKTIVNÍ ALGORITMY (O(n log n))
            // =================================================================

            // --- QUICK SORT ---
            new AlgorithmData
            {
                Slug = "quick",
                Name = "Quick Sort",
                Description = "Vysoce efektivní algoritmus na principu 'Rozděl a panuj'. Vybere prvek zvaný pivot a rozdělí pole na prvky menší a větší než pivot. Následně se rekurzivně zavolá na obě poloviny.",
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
                    new LogicStep { Number = 1, Title = "Pivot Selection", Description = "Zvolení referenčního prvku (pivota) z aktuálního úseku pole." },
                    new LogicStep { Number = 2, Title = "Partitioning", Description = "Přerovnání prvků – menší než pivot jdou vlevo, větší jdou vpravo." },
                    new LogicStep { Number = 3, Title = "Recursive Call", Description = "Nezávislé spuštění stejného procesu pro levou a pravou část od pivota." }
                }
            },
            new AlgorithmData
{
                Slug = "bogo",
                Name = "Bogo Sort",
                Description = "Extrémně neefektivní a humorný řadicí algoritmus založený na čisté náhodě. Funguje tak, že zkontroluje, zda je pole seřazené, a pokud ne, celé ho náhodně promíchá a zkouší to znovu.",
                TimeComplexityBest = "O(N)",
                TimeComplexityAverage = "O(N · N!)",
                TimeComplexityWorst = "O(∞)",
                SpaceComplexity = "O(1)",
                Pseudocode = new()
                {
                    new PseudocodeLine { Text = "procedure bogoSort(A : list of sortable items)", IsKeyword = true },
                    new PseudocodeLine { Text = "    while not isSorted(A) do", IsKeyword = true },
                    new PseudocodeLine { Text = "        shuffle(A)", IsComment = false }
                },
                LogicFlow = new()
                {
                    new LogicStep { Number = 1, Title = "Check Sortedness", Description = "Kontrola, zda jsou všechny prvky v poli již správně seřazeny od nejmenšího po největší." },
                    new LogicStep { Number = 2, Title = "Random Shuffle", Description = "Pokud pole seřazené není, kompletně a zcela náhodně se promíchají všechny jeho prvky." },
                    new LogicStep { Number = 3, Title = "Repeat Until Lucky", Description = "Opakování celého procesu kontroly a míchání znovu a znovu, dokud algoritmus nemá prosté štěstí." }
                }
            }
        };

        public AlgorithmData? GetAlgorithmBySlug(string slug)
        {
            return algorithms.FirstOrDefault(a => a.Slug.Equals(slug, StringComparison.OrdinalIgnoreCase));
        }
    }
}
