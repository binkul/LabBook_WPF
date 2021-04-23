using LabBook.ADO.Common;
using LabBook.Dto;
using LabBook.Security;
using System.Data;
using System.Data.SqlClient;

namespace LabBook.ADO.Repository
{
    public class LabBookRepository : IRepository<LabBookDto>
    {
        private const string getAllQuery = "Select bk.id, bk.title, bk.density, bk.remarks, bk.observation, us.identifier, bk.user_id, " +
                            "ck.name as cycle, bk.cycle_id, bk.created, bk.modified, bk.deleted From LabBook.dbo.ExpLabBook bk Left Join " +
                            "LabBook.dbo.Users us on bk.user_id=us.id Left Join LabBook.dbo.ExpCycle ck on bk.cycle_id= ck.id Order by id";

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

        public bool Delete()
        {
            throw new System.NotImplementedException();
        }

        public LabBookDto Save(LabBookDto data)
        {
            throw new System.NotImplementedException();
        }

        public LabBookDto Update(LabBookDto data)
        {
            throw new System.NotImplementedException();
        }
    }
}
