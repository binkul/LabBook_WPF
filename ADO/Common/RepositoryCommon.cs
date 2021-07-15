using LabBook.ADO.Exceptions;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace LabBook.ADO.Common
{
    public abstract class RepositoryCommon<T> : IRepository<T>
    {
        virtual public bool Delete(long id, string query)
        {
            bool error = false;

            using (SqlCommand cmd = new SqlCommand())
            {
                var connection = new SqlConnection(Application.Current.FindResource("ConnectionString").ToString());
                try
                {
                    cmd.Connection = connection;
                    cmd.CommandText = query + id;
                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    error = true;
                    MessageBox.Show("Problem z połączeniem z serwerem. Prawdopodobnie serwer jest wyłączony, błąd w nazwie serwera lub dostępie do bazy: '" + ex.Message + "'. Błąd z poziomu Delete VisRepository.",
                        "Błąd połaczenia", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (Exception ex)
                {
                    error = true;
                    MessageBox.Show("Problem z połączeniem z serwerem. Prawdopodobnie serwer jest wyłączony: '" + ex.Message + "'. Błąd z poziomu Delete VisRepository.",
                        "Błąd połączenia", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    connection.Close();
                }
            }

            return error;
        }

        virtual public DataTable GetAll(string query)
        {
            var table = new DataTable();

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
                    MessageBox.Show("Problem z połączeniem z serwerem. Prawdopodobnie serwer jest wyłączony, błąd w nazwie serwera lub dostępie do bazy: '" + ex.Message + "'. Błąd z poziomu GetAll VisRepository.",
                        "Błąd połaczenia", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Problem z połączeniem z serwerem. Prawdopodobnie serwer jest wyłączony: '" + ex.Message + "'. Błąd z poziomu GetAll VisRepository.",
                        "Błąd połączenia", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            return table;
        }

        virtual public T Save(T data)
        {
            throw new NotImplementedException();
        }

        virtual public ExceptionCode Save(DataRow data, string query)
        {
            throw new NotImplementedException();
        }

        virtual public void Update(T data)
        {
            throw new NotImplementedException();
        }

        virtual public ExceptionCode Update(DataRow data, string query)
        {
            throw new NotImplementedException();
        }

        virtual public bool ExistById(long id, string query)
        {
            bool result = false;

            using (var connection = new SqlConnection(Application.Current.FindResource("ConnectionString").ToString()))
            {
                try
                {
                    var sqlCmd = new SqlCommand(query, connection) { CommandType = CommandType.Text };
                    sqlCmd.Parameters.AddWithValue("@id", id);
                    connection.Open();

                    if (Convert.ToInt32(sqlCmd.ExecuteScalar()) >= 1)
                        result = true;
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
            return result;
        }

        virtual public bool ExistByName(string name, string query)
        {
            bool result = false;

            using (var connection = new SqlConnection(Application.Current.FindResource("ConnectionString").ToString()))
            {
                try
                {
                    var sqlCmd = new SqlCommand(query, connection) { CommandType = CommandType.Text };
                    sqlCmd.Parameters.AddWithValue("@name", name);
                    connection.Open();

                    if (Convert.ToInt32(sqlCmd.ExecuteScalar()) >= 1)
                        result = true;
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
            return result;
        }

        virtual public T GetById(long id, string query)
        {
            throw new NotImplementedException();
        }
    }
}
