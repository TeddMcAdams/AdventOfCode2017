using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace XUnitTestProject
{
    public class Day3
    {
        private readonly Dictionary<(int, int), int> _allCoordinatesWithTheirValues;
        private readonly AssignValueSettings _settings;

        public Day3()
        {
            _allCoordinatesWithTheirValues = new Dictionary<(int, int), int>();
            _settings = new AssignValueSettings()
            {
                IncreaseValueBy = IncreaseValueBy.One,
                InitialValue = 1,
                MaxValue = 1,
                StartingCoordinates = (0, 0)
            };
        }

        [Fact]
        public void Test()
        {
            //Part1
            Assert.Equal(0, Part1(1));
            Assert.Equal(3, Part1(12));
            Assert.Equal(2, Part1(23));
            Assert.Equal(31, Part1(1024));
            Assert.Equal(371, Part1(368078));

            //Part2
            Assert.Equal(4, Part2(2));
            Assert.Equal(25, Part2(23));
            Assert.Equal(142, Part2(133));
            Assert.Equal(806, Part2(747));
            Assert.Equal(369601, Part2(368078));
        }

        private int Part1(int maxValue)
        {
            _settings.MaxValue = maxValue;
            _allCoordinatesWithTheirValues.Clear();

            AssignValueToSpiralCoordinates();

            return CalculateSteps();
        }

        private int Part2(int maxValue)
        {
            _settings.IncreaseValueBy = IncreaseValueBy.SurroundingValues;
            _settings.MaxValue = maxValue;
            _allCoordinatesWithTheirValues.Clear();

            AssignValueToSpiralCoordinates();

            return _allCoordinatesWithTheirValues
                .First(m => m.Value > _settings.MaxValue)
                .Value;
        }

        private int CalculateSteps()
        {
            (int xCoordForMaxValue, int yCoordForMaxValue) = _allCoordinatesWithTheirValues
                .Single(d => d.Value == _settings.MaxValue).Key;

            return
                Math.Abs(_settings.StartingCoordinates.xCoord - xCoordForMaxValue) +
                Math.Abs(_settings.StartingCoordinates.yCoord - yCoordForMaxValue);
        }

        private void AssignValueToSpiralCoordinates()
        {
            int value = _settings.InitialValue;
            (int x, int y) = _settings.StartingCoordinates;

            _allCoordinatesWithTheirValues.Add((x, y), value);

            for (int i = 1; _settings.MaxValue > value || _allCoordinatesWithTheirValues.LastOrDefault().Value == _settings.MaxValue; i++)
            {
                do
                {
                    x++;
                    value = IncreaseValue((x, y), value);
                    _allCoordinatesWithTheirValues.Add((x, y), value);
                } while (x < i);

                do
                {
                    y++;
                    value = IncreaseValue((x, y), value);
                    _allCoordinatesWithTheirValues.Add((x, y), value);
                } while (y < i);

                do
                {
                    x--;
                    value = IncreaseValue((x, y), value);
                    _allCoordinatesWithTheirValues.Add((x, y), value);
                } while (Math.Abs(x) < i);

                do
                {
                    y--;
                    value = IncreaseValue((x, y), value);
                    _allCoordinatesWithTheirValues.Add((x, y), value);
                } while (Math.Abs(y) < i);

                do
                {
                    x++;
                    value = IncreaseValue((x, y), value);
                    _allCoordinatesWithTheirValues.Add((x, y), value);
                } while (x < i);
            }
        }

        private int IncreaseValue((int x, int y) currentCoordinates, int value)
        {
            int newValue = 0;

            switch (_settings.IncreaseValueBy)
            {
                case IncreaseValueBy.One:
                    newValue = ++value;
                    break;
                case IncreaseValueBy.SurroundingValues:
                    newValue += _allCoordinatesWithTheirValues
                        .SingleOrDefault(m => m.Key.Equals((currentCoordinates.x + 1, currentCoordinates.y + 1)))
                        .Value;
                    newValue += _allCoordinatesWithTheirValues
                        .SingleOrDefault(m => m.Key.Equals((currentCoordinates.x + 1, currentCoordinates.y + 0)))
                        .Value;
                    newValue += _allCoordinatesWithTheirValues
                        .SingleOrDefault(m => m.Key.Equals((currentCoordinates.x + 1, currentCoordinates.y - 1)))
                        .Value;
                    newValue += _allCoordinatesWithTheirValues
                        .SingleOrDefault(m => m.Key.Equals((currentCoordinates.x + 0, currentCoordinates.y + 1)))
                        .Value;
                    newValue += _allCoordinatesWithTheirValues
                        .SingleOrDefault(m => m.Key.Equals((currentCoordinates.x + 0, currentCoordinates.y - 1)))
                        .Value;
                    newValue += _allCoordinatesWithTheirValues
                        .SingleOrDefault(m => m.Key.Equals((currentCoordinates.x - 1, currentCoordinates.y + 1)))
                        .Value;
                    newValue += _allCoordinatesWithTheirValues
                        .SingleOrDefault(m => m.Key.Equals((currentCoordinates.x - 1, currentCoordinates.y + 0)))
                        .Value;
                    newValue += _allCoordinatesWithTheirValues
                        .SingleOrDefault(m => m.Key.Equals((currentCoordinates.x - 1, currentCoordinates.y - 1)))
                        .Value;
                    break;
                default:
                    break;
            }

            return newValue;
        }

        //private int[,] AssignValueToCoordinates2(int maxValue)
        //{
        //    var twoDimensionalArray = new int[maxValue,maxValue];


        //}

        private class AssignValueSettings
        {
            internal IncreaseValueBy IncreaseValueBy;
            internal int InitialValue;
            internal int MaxValue;
            internal (int xCoord, int yCoord) StartingCoordinates;
        }

        private enum IncreaseValueBy
        {
            One,
            SurroundingValues
        }
    }
}
