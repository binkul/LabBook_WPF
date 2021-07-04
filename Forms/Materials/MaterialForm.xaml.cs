using LabBook.Forms.Materials.ModelView;
using System.Windows;

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
        }
    }
}
