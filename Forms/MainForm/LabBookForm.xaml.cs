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

        public LabBookForm()
        {
            InitializeComponent();

            WindowEditMV mainModelView = new WindowEditMV();
            ViscosityMV _viscosityMV = this.Resources["viscosity"] as ViscosityMV;
            GlossMV _glossMV = this.Resources["gloss"] as GlossMV;
            OpacityMV _opacityMV = this.Resources["opacity"] as OpacityMV;
            SpectroMV _spectroMV = this.Resources["spectro"] as SpectroMV;

            this.DataContext = mainModelView;
            _viscosityMV.SetWindowEditMV = mainModelView;
            _glossMV.SetWindowEditMV = mainModelView;
            _opacityMV.SetWindowEditMV = mainModelView;
            _spectroMV.SetWindowEditMV = mainModelView;
            mainModelView.SetViscosityMV = _viscosityMV;
            mainModelView.SetGlossMV = _glossMV;
            mainModelView.SetOpacityMV = _opacityMV;
            mainModelView.SetSpectroMV = _spectroMV;

            FilterMV _filterMV = this.Resources["filter"] as FilterMV;
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
