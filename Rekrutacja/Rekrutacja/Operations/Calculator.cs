using System;

namespace Rekrutacja.Operations
{
    internal class Calculator
    {
        public double Calculate(double variableX, double variableY, string operation)
        {
            double result;
            Rekrutacja.Intercace.ICalculator calculate = null;
            switch (operation)
            {
                case "+":
                    calculate = new Addition();
                    break;
                case "-":
                    calculate = new Subtraction();
                    break;
                case "*":
                    calculate = new Multiplication();
                    break;
                case "/":
                    calculate = new Division();
                    break;
                default:
                    break;
            }

            result = calculate.Calculate(variableX, variableY);
            return Math.Round(result, 4);
        }
    }
}
