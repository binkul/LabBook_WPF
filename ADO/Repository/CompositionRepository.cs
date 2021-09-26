using LabBook.ADO.Common;
using LabBook.Dto;
using LabBook.Forms.Composition.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace LabBook.ADO.Repository
{
    public class CompositionRepository : RepositoryCommon<CompositionDto>
    {
        public static readonly string AllRecipeQuery = "Select c.labbook_id, c.ordering, c.component, c.is_intermediate, c.amount, c.operation, " +
            "o.name, c.comment, m.price, m.currency_id, r.rate, m.intermediate_nrD, m.VOC, m.density From LabBook.dbo.ExpComposition c " +
            "Left Join(LabBook.dbo.Material m Left Join LabBook.dbo.CmbCurrency r on m.currency_id= r.id) on c.component=m.name " +
            "Left Join LabBook.dbo.CmbCompOperation o on c.operation= o.id Where c.labbook_id=";
        public static readonly string MaterialListQuery = "Select name from LabBook.dbo.Material Order By name";
        public static readonly string RecipeDataQuery = "Select Top 1 c.id, c.labbook_id, c.version, c.mass, c.change_date, " +
            "c.comment, c.login_id, u.identifier, u.login, u.permission From LabBook.dbo.ExpCompositionData c Left Join " +
            "LabBook.dbo.Users u On c.login_id= u.id Where c.labbook_id=";
      
        public CompositionData GetRecipeData(long numberD, string title, decimal density)
        {
            CompositionData data = new CompositionData(numberD, title, density);

            using (SqlConnection connection = new SqlConnection(Application.Current.FindResource("ConnectionString").ToString()))
            {
                try
                {
                    string query = RecipeDataQuery + numberD.ToString();
                    SqlCommand sqlCmd = new SqlCommand(query, connection) { CommandType = CommandType.Text };
                    connection.Open();
                    SqlDataReader reader = sqlCmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        _ = reader.Read();
                        data.Id = reader.GetInt64(0);
                        data.LabBookId = numberD;
                        data.Version = reader.GetInt32(2);
                        data.Mass = Convert.ToDouble(reader.GetDecimal(3));
                        data.ChangeDate = reader.GetDateTime(4);
                        data.Comment = !reader.GetValue(5).Equals(DBNull.Value) ? reader.GetString(5) : null;
                        data.LoginId = reader.GetInt32(6);
                        data.LoginShortcut = reader.GetString(7);
                        data.Permision = reader.GetString(8);
                    }
                }
                catch (SqlException ex)
                {
                    _ = MessageBox.Show("Problem z połączeniem z serwerem. Prawdopodobnie serwer jest wyłączony, błąd w nazwie serwera lub dostępie do bazy: '" + ex.Message + "'",
                        "Błąd połaczenia", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (Exception ex)
                {
                    _ = MessageBox.Show("Problem z połączeniem z serwerem. Prawdopodobnie serwer jest wyłączony: '" + ex.Message + "'",
                        "Błąd połączenia", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    connection.Close();
                }
            }

            return data;
        }
    }
}
