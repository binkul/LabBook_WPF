using LabBook.Security;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace LabBook.Forms.MainForm
{
    /// <summary>
    /// Logika interakcji dla klasy LabBookForm.xaml
    /// </summary>
    public partial class LabBookForm : Window
    {
        private readonly User _user;
        private DataTable _labBookTable;
        private DataView _labBookView;

        public LabBookForm(User user)
        {
            InitializeComponent();
            this._user = user;
            SetLabBookTable();
        }

        private void SetLabBookTable()
        {
            string query = "Select bk.id, bk.title, bk.density, bk.remarks, bk.observation, bk.user_id, bk.cycle, " +
                " bk.created, bk.modified From LabBook.dbo.ExpLabBook bk Order by id";
            SqlDataAdapter adapter = new SqlDataAdapter(query, _user.Connection);
            _labBookTable = new DataTable();
            adapter.Fill(_labBookTable);
            DataColumn id = new DataColumn();
            DataColumn[] klucz = new DataColumn[1];
            id = _labBookTable.Columns["id"];
            klucz[0] = id;
            _labBookTable.PrimaryKey = klucz;
            _labBookView = new DataView(_labBookTable);

            DgLabBook.ItemsSource = _labBookView;
        }
    }
}
