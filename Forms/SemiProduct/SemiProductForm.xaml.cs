using LabBook.Dto;
using LabBook.Forms.Navigation;
using LabBook.Forms.SemiProduct.ModelView;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace LabBook.Forms.SemiProduct
{
    /// <summary>
    /// Logika interakcji dla klasy SemiProductForm.xaml
    /// </summary>
    public partial class SemiProductForm : Window
    {
        public SemiProductForm(LabBookDto labBookDto)
        {
            InitializeComponent();

            SemiProductFormMV semiProductFormMV = new SemiProductFormMV(labBookDto);
            DataContext = semiProductFormMV;

            FilterMV filterMV = Resources["filter"] as FilterMV;
            NavigationMV naviMV = Resources["navi"] as NavigationMV;
            filterMV.SetWindowEdit(semiProductFormMV);
            naviMV.ModelView = semiProductFormMV;
            semiProductFormMV.NavigationMV = naviMV;
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
