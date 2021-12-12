using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application
{
    class Program
    {
        private static void PrintPermutations(List<List<string>> input)
        {

            var enumerators = input.Select(list => list.GetEnumerator()).ToArray();
            // Initial MoveNext in order to set Current to first element of collection.
            for (int i = 0; i < enumerators.Length; i++)
            {
                enumerators[i].MoveNext();
            }

            var sb = new StringBuilder();
            var collectResults = true;

            while (collectResults)
            {
                foreach (var enumerator in enumerators)
                {
                    sb.AppendFormat("{0} ", enumerator.Current);
                }

                sb.AppendLine();

                var needMoveEnumeratorToNext = true;
                var enumeratorIndex = enumerators.Length - 1;

                // We move enumerators to next element from the bottom row. If last element has been reached in some row,
                // we reset enumerator to the first element and move enumerator of previous row one position forward.
                while (needMoveEnumeratorToNext)
                {
                    if (enumerators[enumeratorIndex].MoveNext())
                    {
                        needMoveEnumeratorToNext = false;
                    }
                    else
                    {
                        if (enumeratorIndex == 0)
                        {
                            // If we completed first row in input, we have all permutations.
                            collectResults = false;
                            break;
                        }
                        // Reset enumerator to first element again
                        enumerators[enumeratorIndex] = input[enumeratorIndex].GetEnumerator();
                        enumerators[enumeratorIndex].MoveNext();
                        enumeratorIndex--;
                    }
                }
            }

            Console.WriteLine(sb.ToString());
        }

        static void Main(string[] args)
        {
            var input = new List<List<string>>()
            {
                new List<string>(){"1", "a", "2"},
                new List<string>(){"3", "b", "3"},
                new List<string>(){"4", "c", "5"}
            };

            PrintPermutations(input);

            Console.ReadKey();
        }
    }
}
