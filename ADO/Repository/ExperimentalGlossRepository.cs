using LabBook.ADO.Common;
using LabBook.ADO.Exceptions;
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
        public static readonly string DelQuery = "Delete LabBook.dbo.ExpGloss Where id = ";
        public static readonly string SaveQuery = "Insert Into LabBook.dbo.ExpGloss(labbook_id, date_created, date_update, " +
            "gloss_20, gloss_60, gloss_85, gloss_class, comment) Values(@labbook_id, @date_created, @date_update, " +
            "@gloss_20, @gloss_60, @gloss_85, @gloss_class, @comment)";
        public static readonly string UpdateQuery = "Update LabBook.dbo.ExpGloss Set labbook_id=@labbook_id, " +
            "date_update=@date_update, gloss_20=@gloss_20, gloss_60=@gloss_60, gloss_85=@gloss_85, " +
            "gloss_class=@gloss_class, comment=@comment Where id=@id";

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

        public void RefreshMainTable(DataTable dataTable, long labBooklId)
        {
            var query = _allQueryByLabId + labBooklId;

            using (var connection = new SqlConnection(Application.Current.FindResource("ConnectionString").ToString()))
            {
                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    adapter.Fill(dataTable);
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Problem z połączeniem z serwerem. Prawdopodobnie serwer jest wyłączony, błąd w nazwie serwera lub dostępie do bazy: '" + ex.Message + "'. Błąd z poziomu RefreshMainTable VisRepository.",
                        "Błąd połaczenia", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Problem z połączeniem z serwerem. Prawdopodobnie serwer jest wyłączony: '" + ex.Message + "'. Błąd z poziomu RefreshMainTable VisRepository.",
                        "Błąd połączenia", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        override public ExceptionCode Save(DataRow row, string query)
        {
            ExceptionCode error = ExceptionCode.NoError;

            using (SqlCommand cmd = new SqlCommand())
            {
                var connection = new SqlConnection(Application.Current.FindResource("ConnectionString").ToString());
                try
                {
                    cmd.Connection = connection;
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@labbook_id", row["labbook_id"]);
                    cmd.Parameters.AddWithValue("@date_created", row["date_created"]);
                    cmd.Parameters.AddWithValue("@date_update", row["date_update"]);
                    cmd.Parameters.AddWithValue("@gloss_20", row["gloss_20"]);
                    cmd.Parameters.AddWithValue("@gloss_60", row["gloss_60"]);
                    cmd.Parameters.AddWithValue("@gloss_85", row["gloss_85"]);
                    cmd.Parameters.AddWithValue("@gloss_class", row["gloss_class"]);
                    cmd.Parameters.AddWithValue("@comment", row["comment"]);
                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    error = ExceptionCode.SqlError;
                    MessageBox.Show("Problem z połączeniem z serwerem. Prawdopodobnie serwer jest wyłączony, błąd w nazwie serwera lub dostępie do bazy: '" + ex.Message + "'. Błąd z poziomu Save Visrepository.",
                        "Błąd połaczenia", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (Exception ex)
                {
                    error = ExceptionCode.SqlConnectionError;
                    MessageBox.Show("Problem z połączeniem z serwerem. Prawdopodobnie serwer jest wyłączony: '" + ex.Message + "'. Błąd z poziomu Save Visrepository.",
                        "Błąd połączenia", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    connection.Close();
                }
            }

            return error;
        }

        override public ExceptionCode Update(DataRow row, string query)
        {
            ExceptionCode error = ExceptionCode.NoError;

            using (SqlCommand cmd = new SqlCommand())
            {
                var connection = new SqlConnection(Application.Current.FindResource("ConnectionString").ToString());
                try
                {
                    cmd.Connection = connection;
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@labbook_id", row["labbook_id"]);
                    cmd.Parameters.AddWithValue("@date_update", row["date_update"]);
                    cmd.Parameters.AddWithValue("@gloss_20", row["gloss_20"]);
                    cmd.Parameters.AddWithValue("@gloss_60", row["gloss_60"]);
                    cmd.Parameters.AddWithValue("@gloss_85", row["gloss_85"]);
                    cmd.Parameters.AddWithValue("@gloss_class", row["gloss_class"]);
                    cmd.Parameters.AddWithValue("@comment", row["comment"]);
                    cmd.Parameters.AddWithValue("@id", row["id"]);
                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    error = ExceptionCode.SqlError;
                    MessageBox.Show("Problem z połączeniem z serwerem. Prawdopodobnie serwer jest wyłączony, błąd w nazwie serwera lub dostępie do bazy: '" + ex.Message + "'. Błąd z poziomu Update Visrepository.",
                        "Błąd połaczenia", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (Exception ex)
                {
                    error = ExceptionCode.SqlConnectionError;
                    MessageBox.Show("Problem z połączeniem z serwerem. Prawdopodobnie serwer jest wyłączony: '" + ex.Message + "'. Błąd z poziomu Update Visrepository.",
                        "Błąd połączenia", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    connection.Close();
                }
            }

            return error;
        }

    }
}
