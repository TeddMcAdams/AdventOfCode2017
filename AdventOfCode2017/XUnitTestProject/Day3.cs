using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace XUnitTestProject
{
    public class Day3
    {
        [Fact]
        public void Test()
        {
            //Part1
            Assert.Equal(0, Part1(1));
            Assert.Equal(3, Part1(12));
            Assert.Equal(2, Part1(23));
            Assert.Equal(31, Part1(1024));
            Assert.Equal(371, Part1(368078));
        }

        private int Part1(int location)
        {
            Dictionary<int, int[]> allCoordinates = AssignAllCoordinates(location);

            return CalculateSteps(allCoordinates[location]);
        }

        private int CalculateSteps(int[] coordinates)
        {
            return Math.Abs(0 - coordinates[0]) + Math.Abs(0 - coordinates[1]);
        }

        // assigning X, Y coordinates for every location in a counter-clockwise spiral pattern
        private Dictionary<int, int[]> AssignAllCoordinates(int maxValue)
        {
            maxValue++;

            var allCoordinates = new Dictionary<int, int[]>();

            int location = 1;
            int x = 0;
            int y = 0;

            allCoordinates.Add(location, new[] { x, y });
            location++;

            for (int i = 1; i < maxValue && location < maxValue; i++)
            {
                if (location < maxValue)
                {
                    do
                    {
                        x++;
                        allCoordinates.Add(location, new[] { x, y });
                        location++;
                    } while (x < i && location < maxValue);
                }

                if (location < maxValue)
                {
                    do
                    {
                        y++;
                        allCoordinates.Add(location, new[] { x, y });
                        location++;
                    } while (y < i && location < maxValue);
                }

                if (location < maxValue)
                {
                    do
                    {
                        x--;
                        allCoordinates.Add(location, new[] { x, y });
                        location++;
                    } while (Math.Abs(x) < i && location < maxValue);
                }

                if (location < maxValue)
                {
                    do
                    {
                        y--;
                        allCoordinates.Add(location, new[] { x, y });
                        location++;
                    } while (Math.Abs(y) < i && location < maxValue);
                }

                if (location < maxValue)
                {
                    do
                    {
                        x++;
                        allCoordinates.Add(location, new[] { x, y });
                        location++;
                    } while (x < i && location < maxValue);
                }
            }

            return allCoordinates;
        }
    }
}
