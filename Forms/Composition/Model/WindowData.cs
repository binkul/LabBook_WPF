namespace LabBook.Forms.Composition.Model
{
    public class WindowData
    {
        public double FormXpos { get; set; }
        public double FormYpos { get; set; }
        public double FormWidth { get; set; }
        public double FormHeight { get; set; }

        public double ColumnLP { get; set; }
        public double ColumnComponent { get; set; }
        public double ColumnAmount { get; set; }
        public double ColumnMass { get; set; }
        public double ColumnPriceKg { get; set; }
        public double ColumnPrice { get; set; }
        public double ColumnVOC { get; set; }
        public double ColumnComment { get; set; }

        public WindowData(double formXpos, double formYpos, double formWidth, double formHeight, 
            double columnLP, double columnComponent, double columnAmount, double columnMass, 
            double columnPriceKg, double columnPrice, double columnVOC, double columnComment)
        {
            FormXpos = formXpos;
            FormYpos = formYpos;
            FormWidth = formWidth;
            FormHeight = formHeight;
            ColumnLP = columnLP;
            ColumnComponent = columnComponent;
            ColumnAmount = columnAmount;
            ColumnMass = columnMass;
            ColumnPriceKg = columnPriceKg;
            ColumnPrice = columnPrice;
            ColumnVOC = columnVOC;
            ColumnComment = columnComment;
        }
    }
}
