using LabBook.ADO.Common;
using LabBook.ADO.Exceptions;
using LabBook.Dto;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace LabBook.ADO.Repository
{
    public class ExperimentCycleRepository : IRepository<ExperimentCycleDto>
    {
        private const string getAllQuery = "Select cy.id, cy.name, cy.user_id, cy.date From LabBook.dbo.ExpCycle cy Order by name";

        public bool Delete(long id)
        {
            return false;
        }

        public DataTable GetAll()
        {
            DataTable table = new DataTable();

            using (var connection = new SqlConnection(Application.Current.FindResource("ConnectionString").ToString()))
            {
                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(getAllQuery, connection);
                    adapter.Fill(table);

                    DataColumn[] klucz = new DataColumn[1];
                    DataColumn id = table.Columns["id"];
                    klucz[0] = id;
                    table.PrimaryKey = klucz;
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Problem z połączeniem z serwerem. Prawdopodobnie serwer jest wyłączony, błąd w nazwie serwera lub dostępie do bazy: '" + ex.Message + "'.",
                        "Błąd połaczenia", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Problem z połączeniem z serwerem. Prawdopodobnie serwer jest wyłączony: '" + ex.Message + "'.",
                        "Błąd połączenia", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            return table;
        }

        public ExperimentCycleDto Save(ExperimentCycleDto data)
        {
            throw new NotImplementedException();
        }

        public ExceptionCode Save(DataRow data)
        {
            throw new NotImplementedException();
        }

        public ExperimentCycleDto Update(ExperimentCycleDto data)
        {
            throw new NotImplementedException();
        }

        public ExceptionCode Update(DataRow data)
        {
            throw new NotImplementedException();
        }
    }
}
