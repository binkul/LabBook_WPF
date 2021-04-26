using LabBook.Forms.Tools;
using LabBook.Security;
using LabBook.ADO.Service;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Data;

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

        public DataView GetUserView
        {
            get { return _userView; }
        }

        public DataView GetExpCycle
        {
            get { return _expCycleView; }
        }

        private void PrepareForm()
        {
            _labBookView = _labBookService.GetAll();
            DataContext = _labBookView;

            _expCycleView = _expCycleService.GetAll();
            CmbCycleFilter.ItemsSource = _expCycleView;
            CmbCycle.ItemsSource = _expCycleView;

            _userView = _userService.GetAll();
            Resources["UserTable"] = _userView;
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
            startPos += (int)(ColNumberD.ActualWidth);
            Canvas.SetLeft(TxtTitleFilter, startPos);
            startPos += (int)(ColTitle.ActualWidth);
            Canvas.SetLeft(CmbUserFilter, startPos);
            startPos += (int)(ColUser.ActualWidth);
            Canvas.SetLeft(CmbCycleFilter, startPos);
            startPos += (int)(ColCycle.ActualWidth);
            Canvas.SetLeft(DpDate, startPos);
        }

        private void BtnNavigation_Click(object sender, RoutedEventArgs e)
        {
            Navigation.Navigate(DgLabBook, TxtNavieRec, LblNavieRec, sender);
        }

        private void TxtBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                TextBox tBox = (TextBox)sender;
                DependencyProperty prop = TextBox.TextProperty;

                BindingExpression binding = BindingOperations.GetBindingExpression(tBox, prop);
                if (binding != null) { binding.UpdateSource(); }

                TraversalRequest tRequest = new TraversalRequest(FocusNavigationDirection.Next);
                if (Keyboard.FocusedElement is UIElement keyboardFocus)
                {
                    keyboardFocus.MoveFocus(tRequest);
                }
            }
        }

        private void DgLabBook_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Navigation.SetNavigationText(DgLabBook.SelectedIndex + 1, DgLabBook.Items.Count, TxtNavieRec, LblNavieRec);
        }
    }
}
