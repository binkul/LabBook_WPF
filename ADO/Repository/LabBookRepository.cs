using LabBook.ADO.Common;
using LabBook.ADO.Exceptions;
using LabBook.Dto;
using LabBook.Security;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace LabBook.ADO.Repository
{
    public class LabBookRepository : IRepository<LabBookDto>
    {
        private const string _allQuery = "Select bk.id, bk.title, bk.density, bk.remarks, bk.observation, bk.user_id, " +
                            "bk.cycle_id, bk.created, bk.modified, bk.deleted From LabBook.dbo.ExpLabBook bk Order by id";
        private readonly string _saveQuery = "Insert Into LabBook.dbo.ExpLabBook(title, density, observation, remarks, user_id, cycle_id, created, modified, deleted) " +
                            "Values(@title, @density, @observation, @remarks, @user_id, @cycle_id, @created, @modified, @deleted)";
        private readonly string _updateQuery = "Update LabBook.dbo.ExpLabBook Set title=@title, density=@density, observation=@observation, remarks=@remarks, user_id=@user_id, " +
                            "cycle_id=@cycle_id, created=@created, modified=@modified, deleted=@deleted Where id=@id";
        private readonly string _deleteQuery = "Update LabBook.dbo.ExpLabBook Set deleted='true' Where id=@id";

        public DataTable GetAll()
        {
            DataTable table = new DataTable();

            using (var connection = new SqlConnection(Application.Current.FindResource("ConnectionString").ToString()))
            {
                try
                {
                    var adapter = new SqlDataAdapter(_allQuery, connection);
                    adapter.Fill(table);

                    DataColumn[] klucz = new DataColumn[1];
                    DataColumn id = table.Columns["id"];
                    klucz[0] = id;
                    table.PrimaryKey = klucz;
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Problem z połączeniem z serwerem. Prawdopodobnie serwer jest wyłączony, błąd w nazwie serwera lub dostępie do bazy: '" + ex.Message + "'",
                        "Błąd połaczenia", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Problem z połączeniem z serwerem. Prawdopodobnie serwer jest wyłączony: '" + ex.Message + "'",
                        "Błąd połączenia", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }

            return table;
        }

        public bool Delete(long id)
        {
            throw new System.NotImplementedException();
        }

        public LabBookDto Save(LabBookDto data)
        {
            throw new System.NotImplementedException();
        }

        public LabBookDto Update(LabBookDto data)
        {
            throw new System.NotImplementedException();
        }

        public ExceptionCode Save(DataRow data)
        {
            throw new System.NotImplementedException();
        }

        public ExceptionCode Update(DataRow row)
        {
            ExceptionCode error = ExceptionCode.NoError;

            using (SqlCommand cmd = new SqlCommand())
            {
                var connection = new SqlConnection(Application.Current.FindResource("ConnectionString").ToString());
                try
                {
                    cmd.Connection = connection;
                    cmd.CommandText = _updateQuery;
                    cmd.Parameters.AddWithValue("@title", row["title"]);
                    cmd.Parameters.AddWithValue("@density", row["density"]);
                    cmd.Parameters.AddWithValue("@observation", row["observation"]);
                    cmd.Parameters.AddWithValue("@remarks", row["remarks"]);
                    cmd.Parameters.AddWithValue("@user_id", row["user_id"]);
                    cmd.Parameters.AddWithValue("@cycle_id", row["cycle_id"]);
                    cmd.Parameters.AddWithValue("@created", row["created"]);
                    cmd.Parameters.AddWithValue("@modified", row["modified"]);
                    cmd.Parameters.AddWithValue("@deleted", row["deleted"]);
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
    }
}
