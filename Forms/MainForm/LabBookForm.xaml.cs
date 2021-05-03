using LabBook.Security;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Data;
using LabBook.Forms.MainForm.ModelView;

namespace LabBook.Forms.MainForm
{
    /// <summary>
    /// Logika interakcji dla klasy LabBookForm.xaml
    /// </summary>
    public partial class LabBookForm : Window
    {
        //private readonly string _allUser = "-- Wszyscy --";
        //private readonly string _path = "\\Data\\Forms\\LabBookForm.xml";
        private readonly User _user;
        private readonly FilterMV _filterMV;
        //private readonly LabBookService _labBookService;
        //private readonly ExperimentCycleService _expCycleService;
        //private readonly UserService _userService;
        //private DataView _labBookView;
        //private DataView _expCycleView;
        //private DataView _userView;

        public LabBookForm(User user)
        {
            InitializeComponent();
            _user = user;
            //_labBookService = new LabBookService(_user);
            //_expCycleService = new ExperimentCycleService(_user);
            //_userService = new UserService(_user);

            WindowEditMV mainModelView = new WindowEditMV(_user, this);
            this.DataContext = mainModelView;
            _filterMV = this.Resources["filter"] as FilterMV;
            _filterMV.SetWindowEdit(mainModelView);

            //PrepareForm();
        }

        private void PrepareForm()
        {
            //_labBookView = _labBookService.GetAll();
            //var dataContex = this.DataContext as WindowEdit;
            //DgLabBook.DataContext = _labBookView;

            //_expCycleView = _expCycleService.GetAll();
            //CmbCycleFilter.ItemsSource = _expCycleView;
            //CmbCycle.ItemsSource = _expCycleView;

            //_userView = _userService.GetAll();
            //DataTable usersFilter = _userView.ToTable();
            //DataRow row = usersFilter.NewRow();
            //row["id"] = -1;
            //row["name"] = _allUser;
            //row["identifier"] = "Brak";
            //usersFilter.Rows.Add(row);
            //DataView viewFilter = new DataView(usersFilter) { Sort = "name" };
            //CmbUserFilter.ItemsSource = viewFilter;

        }

        //private void Column_SizedChanged(object sender, SizeChangedEventArgs e)
        //{
        //    LabBookControlOperation.SetFilterColntorlsSize(this);
        //}

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

        }

        public void GoToFirstRecord()
        {
            Navigation.SelectFirstRow(DgLabBook);
        }
    }
}
