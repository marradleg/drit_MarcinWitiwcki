namespace Rekrutacja.Operations
{
    internal class Subtraction : Rekrutacja.Intercace.ICalculator
    {
        public double Calculate(double variableX, double variableY)
        {
            double result;

            result = variableX - variableY;
            return result;
        }
    }
}
