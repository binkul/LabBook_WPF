using LabBook.Forms.Materials.ModelView;
using LabBook.Forms.Navigation;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace LabBook.Forms.Materials
{
    /// <summary>
    /// Logika interakcji dla klasy MaterialForm.xaml
    /// </summary>
    public partial class MaterialForm : Window
    {
        public MaterialForm()
        {
            InitializeComponent();

            MaterialFormMV materialFormMV = new MaterialFormMV();
            DataContext = materialFormMV;

            FilterMV filterMV = Resources["filter"] as FilterMV;
            NavigationMV naviMV = Resources["navi"] as NavigationMV;
            filterMV.SetWindowEdit(materialFormMV);
            naviMV.ModelView = materialFormMV;
            materialFormMV.NavigationMV = naviMV;
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
