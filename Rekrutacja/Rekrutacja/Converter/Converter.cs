using Soneta.Business;
using System;

namespace Rekrutacja.Converter
{
    internal static class Converter
    {
        
        public static int ConvertToInt (this string variable)
        {
            int result = 0;
            int indexPozition = 0;
            bool isNegative = false;

            if (variable[0] == '-')
            {
                isNegative = true;
                indexPozition = 1;
            }

            for (int i = indexPozition; i < variable.Length; i++)
            {
                char getChar = variable[i];
                if (getChar == '.' || getChar == ',')
                {
                    break;
                }

                int value = getChar - '0';
                result = checked(result * 10 + value);
            }

            return isNegative ? -result : result;
        }


        
    }
}
