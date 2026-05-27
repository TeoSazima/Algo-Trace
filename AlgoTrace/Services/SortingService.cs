using AlgoTrace.Models;

namespace AlgoTrace.Services
{
    public class SortStep
    {
        public int[] Array { get; set; } = [];
        public int CompareIndexA { get; set; } = -1;
        public int CompareIndexB { get; set; } = -1;
        public bool IsSwap { get; set; }
        public int SortedUpToIndex { get; set; } = -1;
    }

    public class SortingService
    {
        public List<SortStep> GenerateSteps(string slug, int[] input)
        {
            return slug.ToLower() switch
            {
                "bubble" => BubbleSort(input),
                "selection" => SelectionSort(input),
                _ => BubbleSort(input)
            };
        }

        private List<SortStep> BubbleSort(int[] input)
        {
            var steps = new List<SortStep>();
            int[] arr = (int[])input.Clone();
            int completed = 0;

            for (int i = 0; i < arr.Length; i++)
            {
                for (int j = 0; j < arr.Length - 1 - completed; j++)
                {
                    bool swap = arr[j] > arr[j + 1];

                    steps.Add(new SortStep
                    {
                        Array = (int[])arr.Clone(),
                        CompareIndexA = j,
                        CompareIndexB = j + 1,
                        IsSwap = swap,
                        SortedUpToIndex = arr.Length - completed
                    });

                    if (swap)
                        (arr[j], arr[j + 1]) = (arr[j + 1], arr[j]);
                }
                completed++;
            }

            steps.Add(new SortStep { Array = (int[])arr.Clone(), SortedUpToIndex = 0 });
            return steps;
        }

        private List<SortStep> SelectionSort(int[] input)
        {
            var steps = new List<SortStep>();
            int[] arr = (int[])input.Clone();

            for (int i = 0; i < arr.Length - 1; i++)
            {
                int minIdx = i;

                for (int j = i + 1; j < arr.Length; j++)
                {
                    steps.Add(new SortStep
                    {
                        Array = (int[])arr.Clone(),
                        CompareIndexA = minIdx,
                        CompareIndexB = j,
                        IsSwap = false,
                        SortedUpToIndex = i
                    });

                    if (arr[j] < arr[minIdx])
                        minIdx = j;
                }

                if (minIdx != i)
                {
                    steps.Add(new SortStep
                    {
                        Array = (int[])arr.Clone(),
                        CompareIndexA = i,
                        CompareIndexB = minIdx,
                        IsSwap = true,
                        SortedUpToIndex = i
                    });

                    (arr[i], arr[minIdx]) = (arr[minIdx], arr[i]);
                }
            }

            steps.Add(new SortStep { Array = (int[])arr.Clone(), SortedUpToIndex = 0 });
            return steps;
        }
    }
}