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
        private readonly ExperimentCycleService _expCycleService;
        private readonly UserService _userService;
        private DataView _labBookView;
        private DataView _expCycleView;
        private DataView _userView;

        public LabBookForm(User user)
        {
            InitializeComponent();
            _user = user;
            _labBookService = new LabBookService(_user);
            _expCycleService = new ExperimentCycleService(_user);
            _userService = new UserService(_user);
            PrepareForm();
        }

        private void PrepareForm()
        {
            _labBookView = _labBookService.GetAll();
            DataContext = _labBookView;

            _expCycleView = _expCycleService.GetAll();
            CmbCycleFilter.ItemsSource = _expCycleView;

            _userView = _userService.GetAll();
            CmbUserFilter.ItemsSource = _userView;

            WindowsOperation.LoadWindowPosition(this, DgLabBook, DgLabBook.Columns.Count, _path);
        }

        private void FrmLabBook_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            WindowsOperation.SaveWindowPosition(this, DgLabBook, DgLabBook.Columns.Count, _path);
        }

        private void FrmLabBook_Loaded(object sender, RoutedEventArgs e)
        {
            Navigation.SelectRowByIndex(DgLabBook, 0);
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

        private void BtnNavigation_Click(object sender, RoutedEventArgs e)
        {
            Navigation.Navigate(DgLabBook, TxtNavieRec, sender);
        }
    }
}
