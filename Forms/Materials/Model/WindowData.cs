using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabBook.Forms.Materials.Model
{
    public class WindowData
    {
        public double FormXpos { get; set; }
        public double FormYpos { get; set; }
        public double FormWidth { get; set; }
        public double FormHeight { get; set; }
        public double NameWidth { get; set; }
        public double FunctionWidth { get; set; }
        public double PriceWidth { get; set; }
        public double CurrencyWidth { get; set; }
        public double UnitWidth { get; set; }
        public double DengerWidth { get; set; }
        public double ProdWidth { get; set; }
        public double ActivWidth { get; set; }

        public WindowData(double formXpos, double formYpos, double formWidth, double formHeight, double nameWidth, double functionWidth, double priceWidth, 
            double currencyWidth, double unitWidth, double dengerWidth, double prodWidth, double activWidth)
        {
            FormXpos = formXpos;
            FormYpos = formYpos;
            FormWidth = formWidth;
            FormHeight = formHeight;
            NameWidth = nameWidth;
            FunctionWidth = functionWidth;
            PriceWidth = priceWidth;
            CurrencyWidth = currencyWidth;
            UnitWidth = unitWidth;
            DengerWidth = dengerWidth;
            ProdWidth = prodWidth;
            ActivWidth = activWidth;
        }
    }
}
