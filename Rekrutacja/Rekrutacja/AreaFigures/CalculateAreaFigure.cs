using Rekrutacja.Enums;
using Rekrutacja.Intercace;
using Soneta.Business.UI.Diagram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
