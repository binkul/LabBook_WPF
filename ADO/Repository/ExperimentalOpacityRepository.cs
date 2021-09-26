using LabBook.ADO.Common;
using LabBook.ADO.Exceptions;
using LabBook.Dto;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace LabBook.ADO.Repository
{
    public class ExperimentalOpacityRepository : RepositoryCommon<ExperimentalGlossDto>
    {
        private static readonly string _allQueryByLabId = "Select id, labbook_id, date_created, date_update, DATEDIFF(DAY, date_created, date_update) as days, " +
            "contrast_75, tw_75, sp_75, contrast_100, tw_100, sp_100, contrast_150, tw_150, sp_150, contrast_240, tw_240, sp_240, other_a_contrast, other_a_type, " +
            "other_b_contrast, other_b_type, contrast_class, contrast_yield, comment From labbook.dbo.ExpContrast Where labbook_id = ";
        public static readonly string ClassQuery = "Select * From LabBook.dbo.CmbContrastClass Order by Name";
        public static readonly string YieldQuery = "Select * From LabBook.dbo.CmbContrastYield Order by Name";
        public static readonly string AppTypeQuery = "Select * From LabBook.dbo.CmbContrastType Order by Name";
        public static readonly string DelQuery = "Delete LabBook.dbo.ExpContrast Where id = ";
        public static readonly string SaveQuery = "Insert Into LabBook.dbo.ExpContrast(labbook_id, date_created, date_update, contrast_75, tw_75, sp_75, contrast_100, " +
            "tw_100, sp_100, contrast_150, tw_150, sp_150, contrast_240, tw_240, sp_240, other_a_contrast, other_a_type, other_b_contrast, other_b_type, contrast_class, " +
            "contrast_yield, comment) Values(@labbook_id, @date_created, @date_update, @contrast_75, @tw_75, @sp_75, @contrast_100, @tw_100, @sp_100, @contrast_150, @tw_150, " +
            "@sp_150, @contrast_240, @tw_240, @sp_240, @other_a_contrast, @other_a_type, @other_b_contrast, @other_b_type, @contrast_class, @contrast_yield, @comment)";
        public static readonly string UpdateQuery = "Update LabBook.dbo.ExpContrast Set labbook_id=@labbook_id, date_update=@date_update, " +
            "contrast_75=@contrast_75, tw_75=@tw_75, sp_75=@sp_75, contrast_100=@contrast_100, tw_100=@tw_100, sp_100=@sp_100, contrast_150=@contrast_150, tw_150=@tw_150, " +
            "sp_150=@sp_150, contrast_240=@contrast_240, tw_240=@tw_240, sp_240=@sp_240, other_a_contrast=@other_a_contrast, other_a_type=@other_a_type, other_b_contrast=@other_b_contrast, " +
            "other_b_type=@other_b_type, contrast_class=@contrast_class, contrast_yield=@contrast_yield, comment=@comment Where id=@id";

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

        public override ExceptionCode Save(DataRow row, string query)
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
                    cmd.Parameters.AddWithValue("@contrast_75", row["contrast_75"]);
                    cmd.Parameters.AddWithValue("@tw_75", row["tw_75"]);
                    cmd.Parameters.AddWithValue("@sp_75", row["sp_75"]);
                    cmd.Parameters.AddWithValue("@contrast_100", row["contrast_100"]);
                    cmd.Parameters.AddWithValue("@tw_100", row["tw_100"]);
                    cmd.Parameters.AddWithValue("@sp_100", row["sp_100"]);
                    cmd.Parameters.AddWithValue("@contrast_150", row["contrast_150"]);
                    cmd.Parameters.AddWithValue("@tw_150", row["tw_150"]);
                    cmd.Parameters.AddWithValue("@sp_150", row["sp_150"]);
                    cmd.Parameters.AddWithValue("@contrast_240", row["contrast_240"]);
                    cmd.Parameters.AddWithValue("@tw_240", row["tw_240"]);
                    cmd.Parameters.AddWithValue("@sp_240", row["sp_240"]);
                    cmd.Parameters.AddWithValue("@other_a_contrast", row["other_a_contrast"]);
                    cmd.Parameters.AddWithValue("@other_a_type", row["other_a_type"]);
                    cmd.Parameters.AddWithValue("@other_b_contrast", row["other_b_contrast"]);
                    cmd.Parameters.AddWithValue("@other_b_type", row["other_b_type"]);
                    cmd.Parameters.AddWithValue("@contrast_class", row["contrast_class"]);
                    cmd.Parameters.AddWithValue("@contrast_yield", row["contrast_yield"]);
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

        public override ExceptionCode Update(DataRow row, string query)
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
                    cmd.Parameters.AddWithValue("@contrast_75", row["contrast_75"]);
                    cmd.Parameters.AddWithValue("@tw_75", row["tw_75"]);
                    cmd.Parameters.AddWithValue("@sp_75", row["sp_75"]);
                    cmd.Parameters.AddWithValue("@contrast_100", row["contrast_100"]);
                    cmd.Parameters.AddWithValue("@tw_100", row["tw_100"]);
                    cmd.Parameters.AddWithValue("@sp_100", row["sp_100"]);
                    cmd.Parameters.AddWithValue("@contrast_150", row["contrast_150"]);
                    cmd.Parameters.AddWithValue("@tw_150", row["tw_150"]);
                    cmd.Parameters.AddWithValue("@sp_150", row["sp_150"]);
                    cmd.Parameters.AddWithValue("@contrast_240", row["contrast_240"]);
                    cmd.Parameters.AddWithValue("@tw_240", row["tw_240"]);
                    cmd.Parameters.AddWithValue("@sp_240", row["sp_240"]);
                    cmd.Parameters.AddWithValue("@other_a_contrast", row["other_a_contrast"]);
                    cmd.Parameters.AddWithValue("@other_a_type", row["other_a_type"]);
                    cmd.Parameters.AddWithValue("@other_b_contrast", row["other_b_contrast"]);
                    cmd.Parameters.AddWithValue("@other_b_type", row["other_b_type"]);
                    cmd.Parameters.AddWithValue("@contrast_class", row["contrast_class"]);
                    cmd.Parameters.AddWithValue("@contrast_yield", row["contrast_yield"]);
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
