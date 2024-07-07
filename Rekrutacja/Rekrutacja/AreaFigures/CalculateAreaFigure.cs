using Rekrutacja.Enums;
using Rekrutacja.Intercace;
using System.Collections.Generic;

namespace Rekrutacja.AreaFigures
{
    internal class CalculateAreaFigure
    {
        private readonly Dictionary<eFigure, ICalculateArea> _calculate;
        public CalculateAreaFigure()
        {
            _calculate = new Dictionary<eFigure, ICalculateArea>
            {
                { eFigure.trókąt, new AreaTriangle() },
                { eFigure.kwadrat, new AreaSquare() },
                { eFigure.prostokąt, new AreaRectangle() },
                { eFigure.koło, new AreaCircle() }
            };
        }

        public int CalculateArea(eFigure figure, uint[] variables)
        {
            return _calculate[figure].CalculateArea(variables);
        }

    }
}
