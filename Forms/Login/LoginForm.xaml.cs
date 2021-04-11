using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace LabBook.Forms.Login
{
    /// <summary>
    /// Logika interakcji dla klasy LoginForm.xaml
    /// </summary>
    public partial class LoginForm : Window
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void BtnSubbmit_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection connection = new SqlConnection(@"Data Source=LAPTOP-S66RS9QE\SQLEXPRESS_2012;Initial Catalog=LabBook;User ID=sa;Password=sata");
            
            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                string query = "Select Count(1) From LabBook.dbo.Users usr Where usr.login=@username and usr.password=@password";
                SqlCommand sqlCmd = new SqlCommand(query, connection);
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.Parameters.AddWithValue("@username", TxtUserName.Text);
                sqlCmd.Parameters.AddWithValue("@password", TxtPassword.Password);

                Login(Convert.ToInt32(sqlCmd.ExecuteScalar()));
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Problem z połączeniem z serwerem. Prawdopodobnie błąd w nazwie serwera lub dostępie do bazy: '" + ex.Message + "'",
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
    }
}
