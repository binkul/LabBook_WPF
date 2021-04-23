using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace LabBook.Forms.MainForm
{
    public class Navigation
    {

        public static void SetNavigationText(int value, int maxValue, TextBox textBox, Label label)
        {
            textBox.Text = value.ToString();
            label.Content = "z " + maxValue.ToString();
        }

        public static void Navigate(DataGrid dataGrid, TextBox textBox, Label label, object sender)
        {
            if (dataGrid.Items.Count == 0) return;

            var button = (Button)sender;
            var name = button.Name;
            var index = dataGrid.SelectedIndex;

            switch (name)
            {
                case "BtnNaviLeftFirst":
                    index = 0;
                    break;
                case "BtnNaviLeft":
                    _ = index > 0 ? index-- : index = 0;
                    break;
                case "BtnNaviRight":
                    _ = index < dataGrid.Items.Count - 1 ? index++ : index = dataGrid.Items.Count - 1;
                    break;
                case "BtnNaviRightLast":
                    index = dataGrid.Items.Count - 1;
                    break;
            }

            SelectRowByIndex(dataGrid, index);
            SetNavigationText(index + 1, dataGrid.Items.Count, textBox, label);
        }

        public static void SelectRowByIndex(DataGrid dataGrid, int rowIndex)
        {
            if (!dataGrid.SelectionUnit.Equals(DataGridSelectionUnit.FullRow))
                throw new ArgumentException("The SelectionUnit of the DataGrid must be set to FullRow.");

            if (rowIndex < 0 || rowIndex > (dataGrid.Items.Count - 1))
                throw new ArgumentException(string.Format("{0} is an invalid row index.", rowIndex));

            var item = dataGrid.Items[rowIndex];
            dataGrid.SelectedItem = item;

            DataGridRow row = dataGrid.ItemContainerGenerator.ContainerFromIndex(rowIndex) as DataGridRow;
            if (row == null)
            {
                dataGrid.ScrollIntoView(item);
                //                row = dataGrid.ItemContainerGenerator.ContainerFromIndex(rowIndex) as DataGridRow;
            }

                //        row.MoveFocus(new TraversalRequest(FocusNavigationDirection.First));
            dataGrid.Focus();
        }

    }
}
