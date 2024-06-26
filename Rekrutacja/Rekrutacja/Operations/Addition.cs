namespace Rekrutacja.Operations
{
    internal class Addition : Rekrutacja.Intercace.ICalculator
    {
        public double Calculate(double variableX, double variableY)
        {
            double result;
            result = (double)(variableX + variableY);
            return result;
        }
    }
}
