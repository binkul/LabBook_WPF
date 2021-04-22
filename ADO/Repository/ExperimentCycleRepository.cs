using LabBook.ADO.Common;
using LabBook.Dto;
using LabBook.Security;
using System;
using System.Data;
using System.Data.SqlClient;

namespace LabBook.ADO.Repository
{
    public class ExperimentCycleRepository : IRepository<ExperimentCycleDto>
    {
        private const string getAllQuery = "Select cy.id, cy.name, cy.user_id, cy.date From LabBook.dbo.ExpCycle cy Order by name";
        private readonly User _user;

        public ExperimentCycleRepository(User user)
        {
            _user = user;
        }

        public bool Delete()
        {
            return false;
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

        public ExperimentCycleDto Save(ExperimentCycleDto data)
        {
            throw new NotImplementedException();
        }

        public ExperimentCycleDto Update(ExperimentCycleDto data)
        {
            throw new NotImplementedException();
        }
    }
}
