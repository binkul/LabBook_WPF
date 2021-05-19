using LabBook.ADO.Common;
using LabBook.Dto;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace LabBook.ADO.Repository
{
    public class ExperimentalGlossRepository : RepositoryCommon<ExperimentalGlossDto>
    {
        private static readonly string _allQueryByLabId = "Select id, labbook_id, date_created, date_update, " +
            "DATEDIFF(DAY, date_created, date_update) as days, gloss_20, gloss_60, gloss_85, gloss_class, comment " +
            "From labbook.dbo.ExpGloss Where labbook_id = ";
        public static readonly string ClassQuery = "Select * From LabBook.dbo.CmbGlosClass Order By name";

        public DataTable CreateTable()
        {
            var query = _allQueryByLabId + "-1";
            DataTable table = new DataTable();

            using (var connection = new SqlConnection(Application.Current.FindResource("ConnectionString").ToString()))
            {
                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    adapter.Fill(table);

                    DataColumn[] klucz = new DataColumn[1];
                    DataColumn id = table.Columns["id"];
                    klucz[0] = id;
                    table.PrimaryKey = klucz;
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Problem z połączeniem z serwerem. Prawdopodobnie serwer jest wyłączony, błąd w nazwie serwera lub dostępie do bazy: '" + ex.Message + "'. Błąd z poziomu CreateTable VisRepository.",
                        "Błąd połaczenia", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Problem z połączeniem z serwerem. Prawdopodobnie serwer jest wyłączony: '" + ex.Message + "'. Błąd z poziomu CreateTable VisRepository.",
                        "Błąd połączenia", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            return table;
        }

    }
}
