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
        public int LogicFlowIndex { get; set; } = 0;
    }

    public class SortingService
    {
        public List<SortStep> GenerateSteps(string slug, int[] input)
        {
            switch (slug.ToLower())
            {
                case "bubble":
                    return BubbleSort(input);
                case "selection":
                    return SelectionSort(input);
                case "insertion":
                    return InsertionSort(input);
                case "quick":
                    return QuickSort(input);
                default:
                    return BubbleSort(input);
            }
        }

        private List<SortStep> BubbleSort(int[] input)
        {
            var steps = new List<SortStep>();
            int[] arr = (int[])input.Clone();
            int completed = 0;

            // Krok 1 — Pointer Initialization
            steps.Add(new SortStep
            {
                Array = (int[])arr.Clone(),
                CompareIndexA = -1,
                CompareIndexB = -1,
                SortedUpToIndex = -1,
                LogicFlowIndex = 1
            });

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
                        SortedUpToIndex = arr.Length - completed,
                        LogicFlowIndex = swap ? 3 : 2
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
                        SortedUpToIndex = i,
                        LogicFlowIndex = 1  
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
                        SortedUpToIndex = i,
                        LogicFlowIndex = 2  
                    });

                    (arr[i], arr[minIdx]) = (arr[minIdx], arr[i]);
                }
            }

            steps.Add(new SortStep { Array = (int[])arr.Clone(), SortedUpToIndex = 0 });
            return steps;
        }

        private List<SortStep> InsertionSort(int[] input)
        {
            var steps = new List<SortStep>();
            int[] arr = (int[])input.Clone();

            for (int i = 1; i < arr.Length; i++)
            {
                int key = arr[i];
                int j = i - 1;

                steps.Add(new SortStep
                {
                    Array = (int[])arr.Clone(),
                    CompareIndexA = i,
                    CompareIndexB = -1,
                    IsSwap = false,
                    SortedUpToIndex = i - 1,
                    LogicFlowIndex = 1
                });

                while (j >= 0)
                {
                    steps.Add(new SortStep
                    {
                        Array = (int[])arr.Clone(),
                        CompareIndexA = j,
                        CompareIndexB = j + 1,
                        IsSwap = false,
                        SortedUpToIndex = i - 1,
                        LogicFlowIndex = 2  
                    });

                    if (arr[j] > key)
                    {
                        arr[j + 1] = arr[j];

                        steps.Add(new SortStep
                        {
                            Array = (int[])arr.Clone(),
                            CompareIndexA = j,
                            CompareIndexB = j + 1,
                            IsSwap = true,
                            SortedUpToIndex = i - 1,
                            LogicFlowIndex = 2  
                        });

                        j--;
                    }
                    else
                    {
                        break;
                    }
                }

                arr[j + 1] = key;


                steps.Add(new SortStep
                {
                    Array = (int[])arr.Clone(),
                    CompareIndexA = j + 1,
                    CompareIndexB = -1,
                    IsSwap = false,
                    SortedUpToIndex = i,
                    LogicFlowIndex = 3
                });
            }

            steps.Add(new SortStep { Array = (int[])arr.Clone(), SortedUpToIndex = arr.Length - 1 });
            return steps;
        }



        private List<SortStep> QuickSort(int[] input)
        {
            var steps = new List<SortStep>();
            int[] arr = (int[])input.Clone();


            QuickSortRecursive(arr, 0, arr.Length - 1, steps);


            steps.Add(new SortStep { Array = (int[])arr.Clone(), SortedUpToIndex = arr.Length - 1 });

            return steps;
        }

        private int Partition(int[] arr, int low, int high, List<SortStep> steps)
        {
            int pivot = arr[high];
            int i = low - 1;

            // Krok 1 — Pivot Selection (jen jednou na začátku partition)
            steps.Add(new SortStep
            {
                Array = (int[])arr.Clone(),
                CompareIndexA = high,
                CompareIndexB = -1,
                IsSwap = false,
                LogicFlowIndex = 1
            });

            for (int j = low; j < high; j++)
            {
                steps.Add(new SortStep
                {
                    Array = (int[])arr.Clone(),
                    CompareIndexA = j,
                    CompareIndexB = high,
                    IsSwap = false,
                    LogicFlowIndex = 2  // Partitioning
                });

                if (arr[j] < pivot)
                {
                    i++;

                    if (i != j)
                    {
                        (arr[i], arr[j]) = (arr[j], arr[i]);

                        steps.Add(new SortStep
                        {
                            Array = (int[])arr.Clone(),
                            CompareIndexA = i,
                            CompareIndexB = j,
                            IsSwap = true,
                            LogicFlowIndex = 2  // stále Partitioning
                        });
                    }
                }
            }

            i++;
            if (i != high)
            {
                (arr[i], arr[high]) = (arr[high], arr[i]);

                steps.Add(new SortStep
                {
                    Array = (int[])arr.Clone(),
                    CompareIndexA = i,
                    CompareIndexB = high,
                    IsSwap = true,
                    LogicFlowIndex = 2
                });
            }

            return i;
        }

        private void QuickSortRecursive(int[] arr, int low, int high, List<SortStep> steps)
        {
            if (low < high)
            {
                int pivotIndex = Partition(arr, low, high, steps);

                // Krok 3 — Recursive Call
                steps.Add(new SortStep
                {
                    Array = (int[])arr.Clone(),
                    CompareIndexA = low,
                    CompareIndexB = high,
                    IsSwap = false,
                    LogicFlowIndex = 3
                });

                QuickSortRecursive(arr, low, pivotIndex - 1, steps);
                QuickSortRecursive(arr, pivotIndex + 1, high, steps);
            }
        }
    }
}
