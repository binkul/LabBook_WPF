using LabBook.Forms.Tools;
using LabBook.Security;
using LabBook.ADO.Services;
using System.Data;
using System.Windows;

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
        private DataTable _labBookTable;
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
            _labBookTable = _labBookService.GetAll();
            _labBookView = new DataView(_labBookTable);
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
    }
}
