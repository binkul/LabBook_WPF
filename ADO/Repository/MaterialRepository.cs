using LabBook.ADO.Common;
using LabBook.ADO.Exceptions;
using LabBook.Dto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LabBook.ADO.Repository
{
    public class MaterialRepository : RepositoryCommon<MaterialDto>
    {
        public static readonly string AllQuery = "Select id, name, is_intermediate, is_danger, is_production, is_active, intermediate_nrD, clp_signal_word_id, function_id, " +
            "price, currency_id, unit_id, density, solids, ash_450, VOC, remarks, login_id, date_created, date_update From LabBook.dbo.Material Where is_intermediate = 'false' " +
            "Order by name";
        public static readonly string SaveQuery = "Insert Into LabBook.dbo.Material(name, is_intermediate, is_danger, is_production, is_active, intermediate_nrD, clp_signal_word_id, " +
            "function_id, price, currency_id, unit_id, density, solids, ash_450, VOC, remarks, login_id, date_created, date_update) Values(@name, @is_intermediate, @is_danger, " +
            "@is_production, @is_active, @intermediate_nrD, @clp_signal_word_id, @function_id, @price, @currency_id, @unit_id, @density, @solids, @ash_450, @VOC, @remarks, " +
            "@login_id, @date_created, @date_update)";
        public static readonly string UpdateQuery = "Update LabBook.dbo.Material Set name=@name, is_danger=@is_danger, is_production=@is_production," +
            "is_active=@is_active, clp_signal_word_id=@clp_signal_word_id, function_id=@function_id, price=@price, currency_id=@currency_id, unit_id=@unit_id, density=@density, " +
            "solids=@solids, ash_450=@ash_450, VOC=@VOC, remarks=@remarks, date_update=@date_update Where id=@id";
        public static readonly string DeleteQuery = "";

        public void RefreshMainTable(DataTable dataTable)
        {
            using (var connection = new SqlConnection(Application.Current.FindResource("ConnectionString").ToString()))
            {
                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(AllQuery, connection);
                    adapter.Fill(dataTable);
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Problem z połączeniem z serwerem. Prawdopodobnie serwer jest wyłączony, błąd w nazwie serwera lub dostępie do bazy: '" + ex.Message + "'. Błąd z poziomu GetAll VisRepository.",
                        "Błąd połaczenia", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Problem z połączeniem z serwerem. Prawdopodobnie serwer jest wyłączony: '" + ex.Message + "'. Błąd z poziomu GetAll VisRepository.",
                        "Błąd połączenia", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
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
                    cmd.Parameters.AddWithValue("@name", row["name"]);
                    cmd.Parameters.AddWithValue("@is_danger", row["is_danger"]);
                    cmd.Parameters.AddWithValue("@is_production", row["is_production"]);
                    cmd.Parameters.AddWithValue("@is_active", row["is_active"]);
                    cmd.Parameters.AddWithValue("@clp_signal_word_id", row["clp_signal_word_id"]);
                    cmd.Parameters.AddWithValue("@function_id", row["function_id"]);

                    if (string.IsNullOrEmpty(row["price"].ToString())) cmd.Parameters.AddWithValue("@price", DBNull.Value);
                    else cmd.Parameters.AddWithValue("@price", row["price"]);

                    cmd.Parameters.AddWithValue("@currency_id", row["currency_id"]);
                    cmd.Parameters.AddWithValue("@unit_id", row["unit_id"]);

                    if (string.IsNullOrEmpty(row["density"].ToString())) cmd.Parameters.AddWithValue("@density", DBNull.Value);
                    else cmd.Parameters.AddWithValue("@density", row["density"]);

                    if (string.IsNullOrEmpty(row["solids"].ToString())) cmd.Parameters.AddWithValue("@solids", DBNull.Value);
                    else cmd.Parameters.AddWithValue("@solids", row["solids"]);

                    if (string.IsNullOrEmpty(row["ash_450"].ToString())) cmd.Parameters.AddWithValue("@ash_450", DBNull.Value);
                    else cmd.Parameters.AddWithValue("@ash_450", row["ash_450"]);

                    if (string.IsNullOrEmpty(row["VOC"].ToString())) cmd.Parameters.AddWithValue("@VOC", DBNull.Value);
                    else cmd.Parameters.AddWithValue("@VOC", row["VOC"]);

                    if (string.IsNullOrEmpty(row["remarks"].ToString())) cmd.Parameters.AddWithValue("@remarks", DBNull.Value);
                    else cmd.Parameters.AddWithValue("@remarks", row["remarks"]);

                    cmd.Parameters.AddWithValue("@date_update", DateTime.Now);
                    cmd.Parameters.AddWithValue("@id", row["id"]);
                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    error = ExceptionCode.SqlError;
                    MessageBox.Show("Problem z połączeniem z serwerem. Prawdopodobnie serwer jest wyłączony, błąd w nazwie serwera lub dostępie do bazy: '" + ex.Message + "'",
                        "Błąd połaczenia", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (Exception ex)
                {
                    error = ExceptionCode.SqlConnectionError;
                    MessageBox.Show("Problem z połączeniem z serwerem. Prawdopodobnie serwer jest wyłączony: '" + ex.Message + "'",
                        "Błąd połączenia", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    connection.Close();
                }
            }

            return error;
        }

        public override ExceptionCode Save(DataRow data, string query)
        {
            ExceptionCode error = ExceptionCode.NoError;


            return error;
        }
    }
}
