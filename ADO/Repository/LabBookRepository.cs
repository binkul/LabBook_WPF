using LabBook.ADO.Common;
using LabBook.Dto;
using LabBook.Security;
using System.Data;
using System.Data.SqlClient;

namespace LabBook.ADO.Repository
{
    public class LabBookRepository : IRepository<LabBookDto>
    {
        private const string getAllQuery = "Select bk.id, bk.title, bk.density, bk.remarks, bk.observation, bk.user_id, bk.cycle, " +
                            " bk.created, bk.modified From LabBook.dbo.ExpLabBook bk Order by id";
        private readonly User _user;

        public LabBookRepository(User user)
        {
            _user = user;
        }

        public DataTable GetAll()
        {
            SqlDataAdapter adapter = new SqlDataAdapter(getAllQuery, _user.Connection);

            DataTable table = new DataTable();
            adapter.Fill(table);

            DataColumn[] klucz = new DataColumn[1];
            DataColumn id = table.Columns["id"];
            klucz[0] = id;
            table.PrimaryKey = klucz;

            return table;
        }
    }
}
