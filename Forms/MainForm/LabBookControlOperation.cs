using System.Windows.Controls;

namespace LabBook.Forms.MainForm
{
    public class LabBookControlOperation
    {
        public static void SetFilterColntorlsSize(LabBookForm form)
        {
            int startPos = 28;
            Canvas.SetLeft(form.ChbFilter, 5);
            Canvas.SetLeft(form.TxtNumerDFilter, startPos);
            startPos += (int)(form.ColNumberD.ActualWidth);
            Canvas.SetLeft(form.TxtTitleFilter, startPos);
            startPos += (int)(form.ColTitle.ActualWidth);
            Canvas.SetLeft(form.CmbUserFilter, startPos);
            startPos += (int)(form.ColUser.ActualWidth);
            Canvas.SetLeft(form.CmbCycleFilter, startPos);
            startPos += (int)(form.ColCycle.ActualWidth) + 1;
            Canvas.SetLeft(form.TxtDensityFilter, startPos);
            startPos += (int)(form.ColDensity.ActualWidth) + 2;
            Canvas.SetLeft(form.DpDate, startPos);
        }
    }
}
