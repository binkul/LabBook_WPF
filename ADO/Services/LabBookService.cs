using LabBook.Security;
using System.Data;
using System.Data.SqlClient;

namespace LabBook.ADO.Services
{
    public class LabBookService
    {
        private readonly User _user;

        public LabBookService(User user)
        {
            _user = user;
        }

        public DataTable GetAll()
        {
            string query = "Select bk.id, bk.title, bk.density, bk.remarks, bk.observation, bk.user_id, bk.cycle, " +
                            " bk.created, bk.modified From LabBook.dbo.ExpLabBook bk Order by id";
            SqlDataAdapter adapter = new SqlDataAdapter(query, _user.Connection);

            DataTable table = new DataTable();
            adapter.Fill(table);

            DataColumn id = new DataColumn();
            DataColumn[] klucz = new DataColumn[1];
            id = table.Columns["id"];
            klucz[0] = id;
            table.PrimaryKey = klucz;

            return table;
        }
    }
}
