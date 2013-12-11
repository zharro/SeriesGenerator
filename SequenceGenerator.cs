namespace SeriesGenerator
{
    using System.Collections.Generic;
    using System.Linq;

    public class SequenceGenerator
    {
        /// <summary>
        /// Computes Fibonacci sequence until the consumer wants.
        /// </summary>
        /// <returns>Fibonacci sequence</returns>
        public static IEnumerable<long> Fibonacci()
        {
            long current = 0;
            long next = 1;
            checked
            {
                while (true)
                {
                    yield return current;
                    var temp = next;
                    next = current + next;
                    current = temp;
                }
            }
        }

        /// <summary>
        /// Computes Bell sequence until the consumer wants.
        /// </summary>
        /// <returns>Bell sequence</returns>
        public static IEnumerable<long> Bell()
        {
            var triangle = new Dictionary<long, List<long>>
            {
                {
                    1, 
                    new List<long>(new long[] {1})
                }
            };
            yield return 1;
            for (var i = 2;; i++)
            {
                yield return triangle[i - 1].Last();
                triangle.Add(i, new List<long>());
                triangle[i].Add(triangle[i - 1].Last());
                checked
                {
                    for (var k = 1; k < i; k++)
                    {
                        var lastVal = triangle[i][k - 1] + triangle[i - 1][k - 1];
                        triangle[i].Add(lastVal);
                    }
                }
                triangle.Remove(i - 2);
            }
        }

        /// <summary>
        /// Computes Catalan sequence until the consumer wants.
        /// </summary>
        /// <returns>Catalan sequence</returns>
        public static IEnumerable<long> Catalan()
        {
            long i = 1;
            long current = 1;
            yield return 1;
            checked
            {
                while (true)
                {
                    current = current*2*(2*i - 1)/(i + 1);
                    yield return current;
                    i++;
                }
            }
        }
    }
}