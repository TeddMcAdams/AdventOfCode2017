using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace XUnitTestProject
{
    public class Day2
    {
        private readonly string _input1 = @"5 1 9 5
7 5 3
2 4 6 8";

        private readonly string _input2 = @"5 9 2 8
9 4 7 3
3 8 6 5";

        private readonly string _input3 = @"1364	461	1438	1456	818	999	105	1065	314	99	1353	148	837	590	404	123
204	99	235	2281	2848	3307	1447	3848	3681	963	3525	525	288	278	3059	821
280	311	100	287	265	383	204	380	90	377	398	99	194	297	399	87
7698	2334	7693	218	7344	3887	3423	7287	7700	2447	7412	6147	231	1066	248	208
3740	837	4144	123	155	2494	1706	4150	183	4198	1221	4061	95	148	3460	550
1376	1462	73	968	95	1721	544	982	829	1868	1683	618	82	1660	83	1778
197	2295	5475	2886	2646	186	5925	237	3034	5897	1477	196	1778	3496	5041	3314
179	2949	3197	2745	1341	3128	1580	184	1026	147	2692	212	2487	2947	3547	1120
460	73	52	373	41	133	671	61	634	62	715	644	182	524	648	320
169	207	5529	4820	248	6210	255	6342	4366	5775	5472	3954	3791	1311	7074	5729
5965	7445	2317	196	1886	3638	266	6068	6179	6333	229	230	1791	6900	3108	5827
212	249	226	129	196	245	187	332	111	126	184	99	276	93	222	56
51	592	426	66	594	406	577	25	265	578	522	57	547	65	564	622
215	2092	1603	1001	940	2054	245	2685	206	1043	2808	208	194	2339	2028	2580
378	171	155	1100	184	937	792	1436	1734	179	1611	1349	647	1778	1723	1709
4463	4757	201	186	3812	2413	2085	4685	5294	5755	2898	200	5536	5226	1028	180";

        [Fact]
        public void Test()
        {
            //Part1
            Assert.Equal(18, Part1(_input1));
            Assert.Equal(53460, Part1(_input3));

            //Part2
            Assert.Equal(9, Part2(_input2));
            Assert.Equal(282, Part2(_input3));
        }

        private int Part1(string input)
        {
            int checksum = 0;

            ParseInputString(input)
                .ForEach(m => checksum += MaxMinDifference(m));

            return checksum;
        }

        private int Part2(string input)
        {
            int checksum = 0;

            ParseInputString(input)
                .ForEach(m => checksum += EvenDivisionResult(m));

            return checksum;
        }

        private List<int[]> ParseInputString(string input)
        {
            var parsedInput = new List<int[]>();

            string[] rows = input.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

            foreach (string row in rows)
            {
                string[] numbers = row.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

                parsedInput.Add(numbers.Select(m => int.Parse(m)).ToArray());
            }

            return parsedInput;
        }

        private int MaxMinDifference(int[] input)
        {
            return input.Max() - input.Min();
        }

        private int EvenDivisionResult(int[] input)
        {
            var pairs = new List<int[]>();

            int[] orderedNumbers = input.OrderByDescending(m => m).ToArray();

            for (int i = 0; i < orderedNumbers.Length; i++)
            {
                int x = orderedNumbers[i];

                for (int j = i + 1; j < orderedNumbers.Length; j++)
                {
                    int y = orderedNumbers[j];

                    pairs.Add(new[] { x, y });
                }
            }

            int[] dividesEvenly = pairs.Single(m => m[0] % m[1] == 0);

            return dividesEvenly[0] / dividesEvenly[1];
        }
    }
}
