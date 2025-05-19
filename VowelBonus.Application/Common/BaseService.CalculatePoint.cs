using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VowelBonus.Application.Common
{

    public interface ICalculatePointService
    {
        int CalculateVowelPoint(string input);
    }

    public class CalculatePointService : ICalculatePointService
    {
        public int CalculateVowelPoint(string input)
        {

            int point = 0;
            input = input.ToLower();
            var len = input.Length - 1;

            if(len == 0)
            {
                if (BaseConst.VOWEL_SCORES.ContainsKey(input[0])){

                    return BaseConst.VOWEL_SCORES[input[0]];
                }
                else
                {
                    return 1;
                }
            }

            for (int i = 0; i <= len; i++)
            {
                if (char.IsLetter(input[i]))
                {
                    if (i == 0 && BaseConst.VOWEL_SCORES.ContainsKey(input[i]) && BaseConst.VOWEL_SCORES.ContainsKey(input[i + 1]))
                    {
                        point += BaseConst.VOWEL_SCORES[input[i]] * 2;
                    }
                    else if (i == len && BaseConst.VOWEL_SCORES.ContainsKey(input[i]) && BaseConst.VOWEL_SCORES.ContainsKey(input[i - 1]))
                    {
                        point += BaseConst.VOWEL_SCORES[input[i]] * 2;
                    }
                    else if (BaseConst.VOWEL_SCORES.TryGetValue(input[i], out int value1) &&
                                                                i != 0 &&
                                                                i != len &&
                                                                (BaseConst.VOWEL_SCORES.ContainsKey(input[i - 1]) ||
                                                                BaseConst.VOWEL_SCORES.ContainsKey(input[i + 1])))
                    {
                        point += value1 * 2;
                    }
                    else if (BaseConst.VOWEL_SCORES.TryGetValue(input[i], out int value2))
                    {
                        point += value2;
                    }
                    else
                    {
                        point += 1;
                    }
                }
            }

            return point;
        }
    }
}
