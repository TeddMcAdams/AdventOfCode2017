using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Xunit;

namespace XUnitTestProject
{
    public class Day6
    {
        [Fact]
        public void Test()
        {
            //Part1
            Assert.Equal(5, CalculateCycles(new int[] { 0, 2, 7, 0 }));
            Assert.Equal(3156, CalculateCycles(new int[] { 2, 8, 8, 5, 4, 2, 3, 1, 5, 5, 1, 2, 15, 13, 5, 14 }));

            //Part2
            Assert.Equal(4, CalculateCycles(new int[] { 0, 2, 7, 0 }, true));
            Assert.Equal(1610, CalculateCycles(new int[] { 2, 8, 8, 5, 4, 2, 3, 1, 5, 5, 1, 2, 15, 13, 5, 14 }, true));
        }

        private int CalculateCycles(int[] input, bool loopTwice = false)
        {
            bool cleared = false;
            var cycles = 0;
            var states = new List<BigInteger>();

            do
            {
                cycles++;
                var blocks = 0;
                int index = Array.IndexOf(input, input.Max());
                blocks = input[index];
                input[index] = 0;

                do
                {
                    index++;
                    if (index == input.Length)
                        index = 0;
                    input[index] += 1;
                    blocks--;
                } while (blocks > 0);

                string bankState = string.Join("", input);

                states.Add(BigInteger.Parse(bankState));

                if (loopTwice && !cleared)
                {
                    if (states.Count != states.Distinct().Count())
                    {
                        cleared = true;
                        states.Clear();
                        states.Add(BigInteger.Parse(bankState));
                        cycles = 0;
                    }
                }

            } while (states.Count == states.Distinct().Count());

            return cycles;
        }
    }
}
