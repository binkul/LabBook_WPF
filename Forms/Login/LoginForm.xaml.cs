using LabBook.Forms.MainForm;
using LabBook.Forms.Register;
using LabBook.Security;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace LabBook.Forms.Login
{
    /// <summary>
    /// Logika interakcji dla klasy LoginForm.xaml
    /// </summary>
    public partial class LoginForm : Window
    {
        private readonly string _loginPath = "\\Data\\login.txt";
        private readonly List<string> _logins;

        public LoginForm()
        {
            InitializeComponent();

            _logins = GetLogins();
            //CmbUserName.SetBinding(ItemsControl.ItemsSourceProperty, new Binding { Source = _logins });
            CmbUserName.ItemsSource = _logins;
        }

        private void BtnSubbmit_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection connection = new SqlConnection(Application.Current.FindResource("ConnectionString").ToString());
            
            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                string query = "Select us.id, us.name, us.surname, us.e_mail, us.login, us.permission, us.identifier, us.active "
                                + "From LabBook.dbo.Users us Where us.login='@username' and us.password='@password'";
                query = query.Replace("@username", CmbUserName.Text);
                query = query.Replace("@password", Encrypt.MD5Encrypt(TxtPassword.Password));

                SqlCommand sqlCmd = new SqlCommand(query, connection);
                SqlDataReader reader = sqlCmd.ExecuteReader(CommandBehavior.CloseConnection);
                User user = null;
                if (reader.HasRows)
                {
                    reader.Read();
                    var id = Convert.ToInt64(reader[0]);
                    var name = Convert.ToString(reader[1]);
                    var surname = Convert.ToString(reader[2]);
                    var email = Convert.ToString(reader[3]);
                    var login = Convert.ToString(reader[4]);
                    var permission = Convert.ToString(reader[5]);
                    var identifier = Convert.ToString(reader[6]);
                    var isActive = Convert.ToBoolean(reader[7]);
                    user = new User(id, name, surname, email, login, permission, identifier, isActive, connection);
                }

                reader.Close();
                OpenLabBookForm(user);
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
                SaveLogins();
            }
        }

        private void OpenLabBookForm(User user)
        {
            if (user != null)
            {
                LabBookForm dashboard = new LabBookForm(user);
                dashboard.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Nieprawidłowy login lub hasło. Spróbuj ponownie",
                    "Błąd logowania", MessageBoxButton.OK, MessageBoxImage.Information);
            }

        }

        private void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            RegisterForm registerForm = new RegisterForm();
            registerForm.Show();
            this.Close();
        }

        private List<string> GetLogins()
        {
            List<string> logins = new List<string>();
            string line = "";

            if (File.Exists(Environment.CurrentDirectory + _loginPath))
            {
                StreamReader file = new StreamReader(Environment.CurrentDirectory + _loginPath);
                while ((line = file.ReadLine()) != null)
                {
                    logins.Add(line);
                }
                file.Close();
            }
            return logins;
        }

        private void SaveLogins()
        {
            var login = CmbUserName.Text;
            var file = Environment.CurrentDirectory + "\\Data\\login.txt";

            _logins.Sort();
            _logins.Remove(login);
            if (!_logins.Contains(login))
            {
                _logins.Insert(0, login);
            }

            if (!Directory.Exists(Path.GetDirectoryName(file)))
                Directory.CreateDirectory(Path.GetDirectoryName(file));

            File.WriteAllLines(Environment.CurrentDirectory + "\\Data\\login.txt", _logins);

        }

        private void FrmLogin_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                BtnSubbmit_Click(null, null);
            }
        }
    }
}
