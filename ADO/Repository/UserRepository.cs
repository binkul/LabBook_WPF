using LabBook.ADO.Common;
using LabBook.Dto;
using LabBook.Security;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace LabBook.ADO.Repository
{
    public enum RegisterStatus
    {
        Ok,
        Exist,
        Error
    }

    public class UserRepository : IRepository<UserDto>
    {
        private const string getAllQuery = "Select us.id, (us.name + ' ' + us.surname) as name From LabBook.dbo.Users us";
        private const string existQuery = "Select Count(1) From LabBook.dbo.Users usr Where usr.login=@username";
        private const string loginQuery = "Select us.id, us.name, us.surname, us.e_mail, us.login, us.permission, us.identifier, us.active "
                                           + "From LabBook.dbo.Users us Where us.login='@username' and us.password='@password'";
        private const string registerQuery = "Insert Into LabBook.dbo.Users(name, surname, e_mail, login, password, "
                                           + "permission, identifier, active, date) Values(@name, @surname, @e_mail, @login, @password, "
                                           + "@permission, @identifier, @active, @date)";

        private readonly User _user;

        public UserRepository() { }

        public UserRepository(User user)
        {
            _user = user;
        }

        public bool Delete()
        {
            throw new NotImplementedException();
        }

        public DataTable GetAll()
        {
            var adapter = new SqlDataAdapter(getAllQuery, _user.Connection);

            var table = new DataTable();
            adapter.Fill(table);

            DataColumn[] klucz = new DataColumn[1];
            DataColumn id = table.Columns["id"];
            klucz[0] = id;
            table.PrimaryKey = klucz;

            return table;
        }

        public UserDto Save(UserDto data)
        {
            throw new NotImplementedException();
        }

        public RegisterStatus ExistUserByLogin(string login)
        {
            var result = RegisterStatus.Ok;
            var connection = new SqlConnection(Application.Current.FindResource("ConnectionString").ToString());

            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                var sqlCmd = new SqlCommand(existQuery, connection) { CommandType = CommandType.Text };
                sqlCmd.Parameters.AddWithValue("@username", login);

                if (Convert.ToInt32(sqlCmd.ExecuteScalar()) >= 1)
                {
                    MessageBox.Show("Użytkownik o loginie: '" + login + "' istnieje już w bazie. Użyj innego loginu do rejestracji.",
                        "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);
                    result = RegisterStatus.Exist;
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Problem z połączeniem z serwerem. Prawdopodobnie serwer jest wyłączony, błąd w nazwie serwera lub dostępie do bazy: '" + ex.Message + "'",
                    "Błąd połaczenia", MessageBoxButton.OK, MessageBoxImage.Error);
                result = RegisterStatus.Error;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problem z połączeniem z serwerem. Prawdopodobnie serwer jest wyłączony: '" + ex.Message + "'",
                    "Błąd połączenia", MessageBoxButton.OK, MessageBoxImage.Error);
                result = RegisterStatus.Error;
            }
            finally
            {
                connection.Close();
            }

            return result;
        }

        public RegisterStatus Register(UserDto data)
        {
            var connection = new SqlConnection(Application.Current.FindResource("ConnectionString").ToString());
            
            var result = ExistUserByLogin(data.Login);
            if (result != RegisterStatus.Ok) return result;

            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                var sqlCmd = new SqlCommand(registerQuery, connection) { CommandType = CommandType.Text };
                sqlCmd.Parameters.AddWithValue("@name", data.Name);
                sqlCmd.Parameters.AddWithValue("@surname", data.Surname);
                sqlCmd.Parameters.AddWithValue("@e_mail", data.Email);
                sqlCmd.Parameters.AddWithValue("@login", data.Login);
                sqlCmd.Parameters.AddWithValue("@password", data.Password);
                sqlCmd.Parameters.AddWithValue("@permission", data.Permission);
                sqlCmd.Parameters.AddWithValue("@identifier", data.Identifieri);
                sqlCmd.Parameters.AddWithValue("@active", data.Active);
                sqlCmd.Parameters.AddWithValue("@date", data.Date);
                sqlCmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Problem z połączeniem z serwerem. Prawdopodobnie serwer jest wyłączony, błąd w nazwie serwera lub dostępie do bazy: '" + ex.Message + "'",
                    "Błąd połaczenia", MessageBoxButton.OK, MessageBoxImage.Error);
                result = RegisterStatus.Error;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problem z połączeniem z serwerem. Prawdopodobnie serwer jest wyłączony: '" + ex.Message + "'",
                    "Błąd połączenia", MessageBoxButton.OK, MessageBoxImage.Error);
                result = RegisterStatus.Error;
            }
            finally
            {
                connection.Close();
            }

            return result;
        }

        public User GetUserByLoginAndPassword(string login, string password)
        {
            User user = null;
            SqlConnection connection = new SqlConnection(Application.Current.FindResource("ConnectionString").ToString());

            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                var query = loginQuery.Replace("@username", login);
                query = query.Replace("@password", Encrypt.MD5Encrypt(password));

                SqlCommand sqlCmd = new SqlCommand(query, connection);
                SqlDataReader reader = sqlCmd.ExecuteReader(CommandBehavior.CloseConnection);
                if (reader.HasRows)
                {
                    reader.Read();
                    var id = Convert.ToInt64(reader[0]);
                    var name = Convert.ToString(reader[1]);
                    var surname = Convert.ToString(reader[2]);
                    var email = Convert.ToString(reader[3]);
                    var log = Convert.ToString(reader[4]);
                    var permission = Convert.ToString(reader[5]);
                    var identifier = Convert.ToString(reader[6]);
                    var isActive = Convert.ToBoolean(reader[7]);
                    user = new User(id, name, surname, email, login, permission, identifier, isActive, connection);
                }

                reader.Close();
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

            return user;
        }

        public UserDto Update(UserDto data)
        {
            throw new NotImplementedException();
        }
    }
}
