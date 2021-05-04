using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace LabBook.Forms.MainForm
{
    public class NavigationKeyBH : Behavior<Window>
    {

        public static readonly DependencyProperty ButtonProperty =
            DependencyProperty.Register(
                "NaviButton",
                typeof(Button),
                typeof(NavigationKeyBH),
                new PropertyMetadata(null, Button_Click)
                );

        public Button NaviButton
        {
            get { return (Button)GetValue(ButtonProperty); }
            set { SetValue(ButtonProperty, value); }
        }

        private static void Button_Click(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //Window window = (d as NavigationKeyBH).AssociatedObject;

            //void button_Click(object sender, RoutedEventArgs _e)
            //{
            //    Window lab = (LabBookForm)window;
            //    WindowEditMV ed = (WindowEditMV)lab.DataContext;
            //    DataGrid dataGrid = ed.MainGrid;





            //    if (dataGrid.Items.Count == 0) return;

            //    var button = (Button)sender;
            //    var name = button.Name;
            //    var index = dataGrid.SelectedIndex;

            //    switch (name)
            //    {
            //        case "BtnNaviLeftFirst":
            //            index = 0;
            //            break;
            //        case "BtnNaviLeft":
            //            _ = index > 0 ? index-- : index = 0;
            //            break;
            //        case "BtnNaviRight":
            //            _ = index < dataGrid.Items.Count - 1 ? index++ : index = dataGrid.Items.Count - 1;
            //            break;
            //        case "BtnNaviRightLast":
            //            index = dataGrid.Items.Count - 1;
            //            break;
            //    }



            //    var item = dataGrid.Items[index];
            //    dataGrid.SelectedItem = item;

            //    DataGridRow row = dataGrid.ItemContainerGenerator.ContainerFromIndex(index) as DataGridRow;
            //    if (row == null)
            //    {
            //        dataGrid.ScrollIntoView(item);
            //    }
            //    dataGrid.Focus();




            //}

            //if (e.OldValue != null) ((Button)e.OldValue).Click -= button_Click;
            //if (e.NewValue != null) ((Button)e.NewValue).Click += button_Click;
            
        }
    }
}
