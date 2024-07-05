using Rekrutacja.Intercace;

namespace Rekrutacja.AreaFigures
{
    internal class AreaRectangle : ICalculateArea
    {
        public int CalculateArea(uint[] variable)
        {
            int result;

            uint sideA = variable[0];
            uint sideB = variable[1];

            result = (int)(sideA * sideB);

            return result;
        }
    }
}
