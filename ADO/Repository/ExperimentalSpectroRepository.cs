using LabBook.ADO.Common;
using LabBook.ADO.Exceptions;
using LabBook.Dto;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace LabBook.ADO.Repository
{
    public class ExperimentalSpectroRepository : RepositoryCommon<ExperimentalSpectroDto>
    {
        private static readonly string _allQueryByLabId = "Select id, labbook_id, date_created, date_update, DATEDIFF(DAY, date_created, date_update) as days, " +
                "L_m, a_m, b_m, WI_m, YI_m, L_s, a_s, b_s, WI_s, YI_s, X, Y, Z, comment From labbook.dbo.ExpSpectro Where labbook_id = ";
        public static readonly string SaveQuery = "Insert Into LabBook.dbo.ExpSpectro(labbook_id, date_created, date_update, L_m, a_m, b_m, WI_m, YI_m, L_s, a_s, b_s, WI_s, YI_s, " +
            "   X, Y, Z, comment) Values(@labbook_id, @date_created, @date_update, @L_m, @a_m, @b_m, @WI_m, @YI_m, @L_s, @a_s, @b_s, @WI_s, @YI_s, @X, @Y, @Z, @comment)";
        public static readonly string UpdateQuery = "Update LabBook.dbo.ExpSpectro Set labbook_id=@labbook_id, date_update=@date_update, " +
            "L_m=@L_m, a_m=@a_m, b_m=@b_m, WI_m=@WI_m, YI_m=@YI_m, L_s=@L_s, a_s=@a_s, b_s=@b_s, WI_s=@WI_s, YI_s=@YI_s, X=@X, Y=@Y, Z=@Z, comment=@comment Where id=@id";
        public static readonly string DelQuery = "Delete LabBook.dbo.ExpSpectro Where id = ";

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
                    cmd.Parameters.AddWithValue("@L_m", row["L_m"]);
                    cmd.Parameters.AddWithValue("@a_m", row["a_m"]);
                    cmd.Parameters.AddWithValue("@b_m", row["b_m"]);
                    cmd.Parameters.AddWithValue("@WI_m", row["WI_m"]);
                    cmd.Parameters.AddWithValue("@YI_m", row["YI_m"]);
                    cmd.Parameters.AddWithValue("@L_s", row["L_s"]);
                    cmd.Parameters.AddWithValue("@a_s", row["a_s"]);
                    cmd.Parameters.AddWithValue("@b_s", row["b_s"]);
                    cmd.Parameters.AddWithValue("@WI_s", row["WI_s"]);
                    cmd.Parameters.AddWithValue("@YI_s", row["YI_s"]);
                    cmd.Parameters.AddWithValue("@X", row["X"]);
                    cmd.Parameters.AddWithValue("@Y", row["Y"]);
                    cmd.Parameters.AddWithValue("@Z", row["Z"]);
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
                    cmd.Parameters.AddWithValue("@date_created", row["date_created"]);
                    cmd.Parameters.AddWithValue("@date_update", row["date_update"]);
                    cmd.Parameters.AddWithValue("@L_m", row["L_m"]);
                    cmd.Parameters.AddWithValue("@a_m", row["a_m"]);
                    cmd.Parameters.AddWithValue("@b_m", row["b_m"]);
                    cmd.Parameters.AddWithValue("@WI_m", row["WI_m"]);
                    cmd.Parameters.AddWithValue("@YI_m", row["YI_m"]);
                    cmd.Parameters.AddWithValue("@L_s", row["L_s"]);
                    cmd.Parameters.AddWithValue("@a_s", row["a_s"]);
                    cmd.Parameters.AddWithValue("@b_s", row["b_s"]);
                    cmd.Parameters.AddWithValue("@WI_s", row["WI_s"]);
                    cmd.Parameters.AddWithValue("@YI_s", row["YI_s"]);
                    cmd.Parameters.AddWithValue("@X", row["X"]);
                    cmd.Parameters.AddWithValue("@Y", row["Y"]);
                    cmd.Parameters.AddWithValue("@Z", row["Z"]);
                    cmd.Parameters.AddWithValue("@comment", row["comment"]);
                    cmd.Parameters.AddWithValue("@id", row["id"]);
                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    error = ExceptionCode.SqlError;
                    MessageBox.Show("Problem z połączeniem z serwerem. Prawdopodobnie serwer jest wyłączony, błąd w nazwie serwera lub dostępie do bazy: '" + ex.Message + "'. Błąd z poziomu Update spectro repository.",
                        "Błąd połaczenia", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (Exception ex)
                {
                    error = ExceptionCode.SqlConnectionError;
                    MessageBox.Show("Problem z połączeniem z serwerem. Prawdopodobnie serwer jest wyłączony: '" + ex.Message + "'. Błąd z poziomu Update spectro repository.",
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
