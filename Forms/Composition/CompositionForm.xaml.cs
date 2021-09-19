using LabBook.Dto;
using LabBook.Forms.Composition.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LabBook.Forms.Composition
{
    /// <summary>
    /// Logika interakcji dla klasy CompositionForm.xaml
    /// </summary>
    public partial class CompositionForm : Window
    {
        public CompositionForm(CompositionEnterDto recipe)
        {
            InitializeComponent();

            //CompositionFormMV compositionFormMV = new CompositionFormMV(recipe);
            //DataContext = compositionFormMV;
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
