using LabBook.Security;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Data;
using LabBook.Forms.MainForm.ModelView;
using System.Data;
using System;

namespace LabBook.Forms.MainForm
{
    /// <summary>
    /// Logika interakcji dla klasy LabBookForm.xaml
    /// </summary>
    public partial class LabBookForm : Window
    {
        private readonly User _user;
        private readonly FilterMV _filterMV;
        private readonly ViscosityMV _viscosityMV;

        public LabBookForm(User user)
        {
            InitializeComponent();
            _user = user;

            WindowEditMV mainModelView = new WindowEditMV(_user);
            this.DataContext = mainModelView;

            _viscosityMV = this.Resources["viscosity"] as ViscosityMV;
            _viscosityMV.ExpService = mainModelView.GetVisService;
            _viscosityMV.SetWindowEditMV = mainModelView;
            mainModelView.SetViscosityMV = _viscosityMV;

            _filterMV = this.Resources["filter"] as FilterMV;
            _filterMV.SetWindowEdit(mainModelView);

            DgLabBook.Focus();
        }

        private void TxtBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                TextBox tBox = (TextBox)sender;
                DependencyProperty prop = TextBox.TextProperty;

                BindingExpression binding = BindingOperations.GetBindingExpression(tBox, prop);
                if (binding != null) { binding.UpdateSource(); }

                TraversalRequest tRequest = new TraversalRequest(FocusNavigationDirection.Next);
                if (Keyboard.FocusedElement is UIElement keyboardFocus)
                {
                    keyboardFocus.MoveFocus(tRequest);
                }
            }
        }

    }
}
