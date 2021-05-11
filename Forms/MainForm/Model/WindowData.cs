namespace LabBook.Forms.MainForm.Model
{
    public class WindowData
    {
        public double FormXpos { get; set; }
        public double FormYpos { get; set; }
        public double FormWidth { get; set; }
        public double FormHeight { get; set; }
        public double ColStatus { get; set; }
        public double ColId { get; set; }
        public double ColTitle { get; set; }
        public double ColUser { get; set; }
        public double ColCycle { get; set; }
        public double ColDensity { get; set; }
        public double ColDate { get; set; }

        public WindowData(double formXpos, double formYpos, double formWidth, double formHeight, double colStatus, double colId, 
            double colTitle, double colUser, double colCycle, double colDensity, double colDate)
        {
            FormXpos = formXpos;
            FormYpos = formYpos;
            FormWidth = formWidth;
            FormHeight = formHeight;
            ColStatus = colStatus;
            ColId = colId;
            ColTitle = colTitle;
            ColUser = colUser;
            ColCycle = colCycle;
            ColDensity = colDensity;
            ColDate = colDate;
        }
    }
}
