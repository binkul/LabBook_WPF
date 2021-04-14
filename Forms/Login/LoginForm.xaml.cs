using LabBook.Forms.Register;
using LabBook.Security;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
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

                string query = "Select Count(1) From LabBook.dbo.Users usr Where usr.login=@username and usr.password=@password";
                SqlCommand sqlCmd = new SqlCommand(query, connection);
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.Parameters.AddWithValue("@username", CmbUserName.Text);
                sqlCmd.Parameters.AddWithValue("@password", Encrypt.MD5Encrypt(TxtPassword.Password));

                Login(Convert.ToInt32(sqlCmd.ExecuteScalar()));
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

        private void Login(int count)
        {
            if (count == 1)
            {
                MainWindow dashboard = new MainWindow();
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
