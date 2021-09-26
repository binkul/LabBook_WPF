using LabBook.ADO.Common;
using LabBook.Dto;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace LabBook.ADO.Repository
{
    public class CurrencyRepository : RepositoryCommon<CurrencyDto>
    {
        public static readonly string GetAllQuery = "Select id, name, rate, date_crated From LabBook.dbo.CmbCurrency Order By name";
        public static readonly string GetByIdQuery = "Select id, name, rate, date_crated From LabBook.dbo.CmbCurrency Where id=";

        public override CurrencyDto GetById(long id, string query)
        {
            CurrencyDto currency = new CurrencyDto();

            using (SqlConnection connection = new SqlConnection(Application.Current.FindResource("ConnectionString").ToString()))
            {
                try
                {
                    query = GetByIdQuery + id.ToString();
                    SqlCommand sqlCmd = new SqlCommand(query, connection) { CommandType = CommandType.Text };
                    connection.Open();
                    SqlDataReader reader = sqlCmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        _ = reader.Read();
                        currency.Id = reader.GetInt32(0);
                        currency.Name = reader.GetValue(1).ToString();
                        currency.Rate = reader.GetDecimal(2);
                        currency.DateCreated = reader.GetDateTime(3);
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
            return currency;
        }
    }
}
