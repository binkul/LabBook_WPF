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
        public static readonly string AllFunctionQuery = "Select id, name, date_created From LabBook.dbo.CmbMaterialFunction Order By name";
        public static readonly string AllCurrencyQuery = "Select id, name, rate, date_crated from LabBook.dbo.CmbCurrency Order by name";
        public static readonly string SaveQuery = "Insert Into LabBook.dbo.Material(name, is_intermediate, is_danger, is_production, is_active, intermediate_nrD, clp_signal_word_id, " +
            "function_id, price, currency_id, unit_id, density, solids, ash_450, VOC, remarks, login_id, date_created, date_update) Values(@name, @is_intermediate, @is_danger, " +
            "@is_production, @is_active, @intermediate_nrD, @clp_signal_word_id, @function_id, @price, @currency_id, @unit_id, @density, @solids, @ash_450, @VOC, @remarks, " +
            "@login_id, @date_created, @date_update)";
        public static readonly string UpdateQuery = "";
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


            return error;
        }

        public override ExceptionCode Save(DataRow data, string query)
        {
            ExceptionCode error = ExceptionCode.NoError;


            return error;
        }
    }
}
