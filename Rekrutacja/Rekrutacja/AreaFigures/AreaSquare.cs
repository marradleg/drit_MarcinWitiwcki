using Rekrutacja.Intercace;

namespace Rekrutacja.AreaFigures
{
    internal class AreaSquare : ICalculateArea
    {
        public int CalculateArea(uint[] variable)
        {
            int result;

            uint side = variable[0];

            result = (int)(side * side);

            return result;
        }
    }
}
