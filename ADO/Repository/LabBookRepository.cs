using LabBook.ADO.Common;
using LabBook.ADO.Exceptions;
using LabBook.Dto;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace LabBook.ADO.Repository
{
    public class LabBookRepository : RepositoryCommon<LabBookDto>
    {
        public static readonly string AllQuery = "Select bk.id, bk.title, bk.density, bk.remarks, bk.observation, bk.user_id, " +
                            "bk.cycle_id, bk.created, bk.modified, bk.deleted From LabBook.dbo.ExpLabBook bk Order by id";
        public static readonly string SaveQuery = "Insert Into LabBook.dbo.ExpLabBook(title, density, observation, remarks, user_id, cycle_id, created, modified, deleted) " +
                            "Values(@title, @density, @observation, @remarks, @user_id, @cycle_id, @created, @modified, @deleted)";
        public static readonly string UpdateQuery = "Update LabBook.dbo.ExpLabBook Set title=@title, density=@density, observation=@observation, remarks=@remarks, user_id=@user_id, " +
                            "cycle_id=@cycle_id, created=@created, modified=@modified, deleted=@deleted Where id=@id";
        public static readonly string DeleteQuery = "Update LabBook.dbo.ExpLabBook Set deleted='true' Where id=";
        private static readonly string _getIdByUserIdQuery = "Select Max(id) as id From LabBook.dbo.ExpLabBook Where user_id = @id";
        public static readonly string GetByIdQuery = "Select id, title, density, remarks, observation, user_id, " +
                            "cycle_id, created, modified, deleted From LabBook.dbo.ExpLabBook Where id = @id";

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

        private long GetId(long userId)
        {
            var id = 0L;

            using (var connection = new SqlConnection(Application.Current.FindResource("ConnectionString").ToString()))
            {
                try
                {
                    var sqlCmd = new SqlCommand(_getIdByUserIdQuery, connection) { CommandType = CommandType.Text };
                    sqlCmd.Parameters.AddWithValue("@id", userId);
                    connection.Open();
                    id = Convert.ToInt64(sqlCmd.ExecuteScalar());
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
                finally
                {
                    connection.Close();
                }
            }

            return id;
        }

        public override LabBookDto Save(LabBookDto data, string query)
        {
            LabBookDto labBook = null;
            var error = false;

            using (SqlCommand cmd = new SqlCommand())
            {
                var connection = new SqlConnection(Application.Current.FindResource("ConnectionString").ToString());
                try
                {
                    cmd.Connection = connection;
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@title", data.Title);
                    cmd.Parameters.AddWithValue("@density", data.Density);
                    if (string.IsNullOrEmpty(data.Observation))
                        cmd.Parameters.AddWithValue("@observation", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@observation", data.Observation);
                    if (string.IsNullOrEmpty(data.Observation))
                        cmd.Parameters.AddWithValue("@remarks", DBNull.Value);
                    else
                        cmd.Parameters.AddWithValue("@remarks", data.Remarks);
                    cmd.Parameters.AddWithValue("@user_id", data.UserId);
                    cmd.Parameters.AddWithValue("@cycle_id", data.CycleId);
                    cmd.Parameters.AddWithValue("@created", data.Created);
                    cmd.Parameters.AddWithValue("@modified", data.Modified);
                    cmd.Parameters.AddWithValue("@deleted", data.Deleted);
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    labBook = data;
                }
                catch (SqlException ex)
                {
                    error = true;
                    MessageBox.Show("Problem z połączeniem z serwerem. Prawdopodobnie serwer jest wyłączony, błąd w nazwie serwera lub dostępie do bazy: '" + ex.Message + "'",
                        "Błąd połaczenia", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (Exception ex)
                {
                    error = true;
                    MessageBox.Show("Problem z połączeniem z serwerem. Prawdopodobnie serwer jest wyłączony: '" + ex.Message + "'",
                        "Błąd połączenia", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    connection.Close();
                }
            }

            if (!error)
                labBook.Id = GetId(data.UserId);

            return labBook;
        }
    
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

        public LabBookDto GetById(string query, long id)
        {
            LabBookDto labBookDto = null;

            using (var connection = new SqlConnection(Application.Current.FindResource("ConnectionString").ToString()))
            {
                try
                {
                    var sqlCmd = new SqlCommand(query, connection) { CommandType = CommandType.Text };
                    sqlCmd.Parameters.AddWithValue("@id", id);
                    connection.Open();

                    SqlDataReader reader = sqlCmd.ExecuteReader();
                    if (!reader.HasRows)
                        return labBookDto;

                    reader.Read();
                    var nrD = Convert.ToInt64(reader.GetInt64(0));
                    var title = Convert.ToString(reader.GetString(1));
                    var density = reader.GetValue(2) != DBNull.Value ? Convert.ToDecimal(reader.GetDecimal(2)) : 0;
                    var remarks = reader.GetValue(3) != DBNull.Value ? Convert.ToString(reader.GetString(3)) : null;
                    var observation = reader.GetValue(4) != DBNull.Value ? Convert.ToString(reader.GetString(4)) : null;
                    var userId = Convert.ToInt64(reader.GetInt64(5));
                    var cycleId = Convert.ToInt64(reader.GetInt64(6));
                    var created = Convert.ToDateTime(reader.GetDateTime(7));
                    var updated = Convert.ToDateTime(reader.GetDateTime(8));
                    var deleted = Convert.ToBoolean(reader.GetBoolean(9));
                    reader.Close();
                    labBookDto = new LabBookDto(nrD, title, density, observation, remarks, userId, cycleId, created, updated, deleted);
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
                finally
                {
                    connection.Close();
                }
            }
            return labBookDto;
        }
    }
}
