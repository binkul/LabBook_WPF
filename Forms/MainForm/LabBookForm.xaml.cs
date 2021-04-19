using LabBook.Forms.Tools;
using LabBook.Security;
using LabBook.ADO.Service;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace LabBook.Forms.MainForm
{
    /// <summary>
    /// Logika interakcji dla klasy LabBookForm.xaml
    /// </summary>
    public partial class LabBookForm : Window
    {
        private readonly string _path = "\\Data\\Forms\\LabBookForm.xml";
        private readonly User _user;
        private readonly LabBookService _labBookService;
 //       private readonly DataTable _labBookTable;
        private DataView _labBookView;

        public LabBookForm(User user)
        {
            InitializeComponent();
            _user = user;
            _labBookService = new LabBookService(_user);
            PrepareForm();
        }

        private void PrepareForm()
        {
            //_labBookTable = _labBookService.GetAll();
            _labBookView = _labBookService.GetAll(); // new DataView(_labBookTable);
            DataContext = _labBookView;

            WindowsOperation.LoadWindowPosition(this, DgLabBook, DgLabBook.Columns.Count, _path);
        }

        private void FrmLabBook_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            WindowsOperation.SaveWindowPosition(this, DgLabBook, DgLabBook.Columns.Count, _path);
        }

        private void FrmLabBook_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Column_SizedChanged(object sender, SizeChangedEventArgs e)
        {
            int startPos = 32;
            Canvas.SetLeft(ChbFilter, 10);
            Canvas.SetLeft(TxtNumerDFilter, startPos);
            startPos += (int)(ColNumerD.ActualWidth);
            Canvas.SetLeft(TxtTitleFilter, startPos);
            startPos += (int)(ColTytul.ActualWidth);
            Canvas.SetLeft(CmbUserFilter, startPos);
            startPos += (int)(ColWykonal.ActualWidth);
            Canvas.SetLeft(CmbCycleFilter, startPos);
            startPos += (int)(ColCykl.ActualWidth);
            Canvas.SetLeft(DpDate, startPos);
        }
    }
}
