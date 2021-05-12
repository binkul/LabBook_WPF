using LabBook.ADO.Repository;
using LabBook.Dto;
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

        private void BtnSubbmit_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateTextBox()) return;

            var repository = new UserRepository();
            var status = repository.ExistUserByLogin(TxtLogin.Text);

            if (status == RegisterStatus.Ok)
            {
                var name = TxtName.Text;
                var surName = TxtSurname.Text;
                var email = TxtEmail.Text;
                var login = TxtLogin.Text;
                var identifier = name.ToUpper().Substring(0, 1) + surName.ToUpper().Substring(0, 1);
                var password = TxtPassword.Password;

                UserDto user = new UserDto(name, surName, email, login, "user", identifier, false, DateTime.Now, password);
                _ = repository.Save(user);

                LoginForm loginForm = new LoginForm();
                loginForm.Show();
                this.Close();
            }
        }
    }
}
