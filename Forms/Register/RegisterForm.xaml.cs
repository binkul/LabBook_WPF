using LabBook.Forms.Login;
using LabBook.Security;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Input;

namespace LabBook.Forms.Register
{
    /// <summary>
    /// Logika interakcji dla klasy RegisterForm.xaml
    /// </summary>
    public partial class RegisterForm : Window
    {
        public RegisterForm()
        {
            InitializeComponent();
        }

        private void FrmRegister_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                BtnSubbmit_Click(null, null);
            }
        }

        private bool ValidateTextBox()
        {
            var name = string.IsNullOrEmpty(TxtName.Text);
            var surName = string.IsNullOrEmpty(TxtSurname.Text);
            var email = string.IsNullOrEmpty(TxtEmail.Text);
            var login = string.IsNullOrEmpty(TxtLogin.Text);
            var password = string.IsNullOrEmpty(TxtPassword.Password);
            var passwordRepeat = string.IsNullOrEmpty(TxtPasswordRepeat.Password);

            if (name || surName || email || email || login || password || passwordRepeat)
            {
                MessageBox.Show("Należy wypełnić wszystkie pola.", "Puste pola", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (TxtPassword.Password.Length < 3)
            {
                MessageBox.Show("Hasło jest za krókie. Wprowadź inne hasło.", "Błąd hasła", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (TxtPassword.Password != TxtPasswordRepeat.Password)
            {
                MessageBox.Show("Powtórzone hasło się nie zgadza!", "Błąd hasła", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return true;
        }

        private bool ValidateUser(SqlConnection connection)
        {
            var result = true;
            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                string query = "Select Count(1) From LabBook.dbo.Users usr Where usr.login=@username";
                SqlCommand sqlCmd = new SqlCommand(query, connection) { CommandType = CommandType.Text };
                sqlCmd.Parameters.AddWithValue("@username", TxtLogin.Text);

                if (Convert.ToInt32(sqlCmd.ExecuteScalar()) >= 1)
                {
                    MessageBox.Show("Użytkownik o loginie: '" + TxtLogin.Text + "' istnieje już w bazie. Użyj innego loginu do rejestracji.",
                        "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);
                    result = false;
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Problem z połączeniem z serwerem. Prawdopodobnie serwer jest wyłączony, błąd w nazwie serwera lub dostępie do bazy: '" + ex.Message + "'",
                    "Błąd połaczenia", MessageBoxButton.OK, MessageBoxImage.Error);
                result = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problem z połączeniem z serwerem. Prawdopodobnie serwer jest wyłączony: '" + ex.Message + "'",
                    "Błąd połączenia", MessageBoxButton.OK, MessageBoxImage.Error);
                result = false;
            }
            finally
            {
                connection.Close();
            }

            return result;
        }

        private void BtnSubbmit_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateTextBox()) return;

            SqlConnection connection = new SqlConnection(Application.Current.FindResource("ConnectionString").ToString());
            if (!ValidateUser(connection)) return;

            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                string query = "Insert Into LabBook.dbo.Users(name, surname, e_mail, login, password, "
                    + "permission, identifier, active, date) Values(@name, @surname, @e_mail, @login, @password, "
                    + "@permission, @identifier, @active, @date)";
                SqlCommand sqlCmd = new SqlCommand(query, connection) { CommandType = CommandType.Text };
                sqlCmd.Parameters.AddWithValue("@name", TxtName.Text);
                sqlCmd.Parameters.AddWithValue("@surname", TxtSurname.Text);
                sqlCmd.Parameters.AddWithValue("@e_mail", TxtEmail.Text);
                sqlCmd.Parameters.AddWithValue("@login", TxtLogin.Text);
                sqlCmd.Parameters.AddWithValue("@password", Encrypt.MD5Encrypt(TxtPassword.Password));
                sqlCmd.Parameters.AddWithValue("@permission", "user");
                var identifier = TxtName.Text.Substring(0, 1).ToUpper() + TxtSurname.Text.Substring(0, 1).ToUpper();
                sqlCmd.Parameters.AddWithValue("@identifier", identifier);
                sqlCmd.Parameters.AddWithValue("@active", "true");
                sqlCmd.Parameters.AddWithValue("@date", DateTime.Now);
                sqlCmd.ExecuteNonQuery();
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
                LoginForm loginForm = new LoginForm();
                loginForm.Show();
                this.Close();
            }

        }
    }
}
