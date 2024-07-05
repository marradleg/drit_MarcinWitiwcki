using Rekrutacja.Intercace;

namespace Rekrutacja.AreaFigures
{
    internal class AreaTriangle : ICalculateArea
    {
        public int CalculateArea(uint[] variable)
        {
            int result;
            uint @base = variable[0];
            uint height = variable[1];

            result = (int)(0.5 * @base * height);

            return result;
        }
    }
}
