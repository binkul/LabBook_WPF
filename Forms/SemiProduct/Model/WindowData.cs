namespace LabBook.Forms.SemiProduct.Model
{
    public class WindowData
    {
        public double FormXpos { get; set; }
        public double FormYpos { get; set; }
        public double FormWidth { get; set; }
        public double FormHeight { get; set; }
        public double NumberDWidth { get; set; }
        public double NameWidth { get; set; }
        public double FunctionWidth { get; set; }
        public double PriceWidth { get; set; }
        public double DengerWidth { get; set; }
        public double VOCWidth { get; set; }
        public double RemarksWidth { get; set; }
        public double DataWidth { get; set; }

        public WindowData(double formXpos, double formYpos, double formWidth, double formHeight, double numberDWidth, 
            double nameWidth, double functionWidth, double priceWidth, double dengerWidth, double vOCWidth, 
            double remarksWidth, double dataWidth)
        {
            FormXpos = formXpos;
            FormYpos = formYpos;
            FormWidth = formWidth;
            FormHeight = formHeight;
            NumberDWidth = numberDWidth;
            NameWidth = nameWidth;
            FunctionWidth = functionWidth;
            PriceWidth = priceWidth;
            DengerWidth = dengerWidth;
            VOCWidth = vOCWidth;
            RemarksWidth = remarksWidth;
            DataWidth = dataWidth;
        }
    }
}
