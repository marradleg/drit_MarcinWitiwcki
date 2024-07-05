using Rekrutacja.Intercace;
using System;

namespace Rekrutacja.AreaFigures
{
    internal class AreaCircle : ICalculateArea
    {
        public int CalculateArea(uint[] variable)
        {
            int result;

            uint radius = variable[0];

            result = (int)(Math.PI * radius * radius);
            return result;
        }
    }
}
