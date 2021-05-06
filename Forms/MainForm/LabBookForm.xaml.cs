using LabBook.Security;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Data;
using LabBook.Forms.MainForm.ModelView;

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
            _viscosityMV.SetService = mainModelView.GetVisService;

            _filterMV = this.Resources["filter"] as FilterMV;
            _filterMV.SetWindowEdit(mainModelView);
            
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

        private void DgLabBook_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            #region scroll to selected row
            var index = DgLabBook.SelectedIndex;
            if (index < 0) return;

            var item = DgLabBook.Items[index];
            DataGridRow row = DgLabBook.ItemContainerGenerator.ContainerFromIndex(index) as DataGridRow;
            if (row == null)
            {
                DgLabBook.ScrollIntoView(item);
            }
            DgLabBook.Focus();
            #endregion
        }
    }
}
