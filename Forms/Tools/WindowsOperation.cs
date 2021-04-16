using System;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;

namespace LabBook.Forms.Tools
{
    public static class WindowsOperation
    {
        public static void SaveWindowPosition(Window form, DataGrid grid, int rowcount, string file)
        {
            var declaration = new XDeclaration("1.0", "utf-8", "yes");
            var comment = new XComment("Window position, size and DataGrid columns width");
            var parameters = new XElement("parameters");
            var window = new XElement("window");
            var dataGrid = new XElement("datagrid");
            var position = new XElement("position");
            var x = new XElement("x", form.Left);
            var y = new XElement("y", form.Top);
            var size = new XElement("size");
            var width = new XElement("width", form.Width);
            var height = new XElement("height", form.Height);

            position.Add(x);
            position.Add(y);
            size.Add(width);
            size.Add(height);
            window.Add(position);
            window.Add(size);

            if (grid != null)
            {
                for (int i = 0; i < rowcount; i++)
                {
                    var columnWidth = new XElement("W" + i.ToString(), grid.Columns[i].ActualWidth);
                    dataGrid.Add(columnWidth);
                }
                window.Add(dataGrid);
            }
            parameters.Add(window);

            XDocument xml = new XDocument { Declaration = declaration };
            xml.Add(comment);
            xml.Add(parameters);

            string path = Directory.GetCurrentDirectory() + file;
            if (!Directory.Exists(Path.GetDirectoryName(path)))
                Directory.CreateDirectory(Path.GetDirectoryName(path));

            xml.Save(path);
        }

        public static bool LoadWindowPosition(Window form, DataGrid grid, int rowcount, string file)
        {
            string path = Directory.GetCurrentDirectory() + file;
            if (!File.Exists(path))
                return false;
            try
            {
                XDocument xml = XDocument.Load(path);

                XElement position = xml.Root.Element("window").Element("position");
                form.Left = double.Parse(position.Element("x").Value, CultureInfo.InvariantCulture);
                form.Top = double.Parse(position.Element("y").Value, CultureInfo.InvariantCulture);

                XElement size = xml.Root.Element("window").Element("size");
                form.Width = double.Parse(size.Element("width").Value, CultureInfo.InvariantCulture);
                form.Height = double.Parse(size.Element("height").Value, CultureInfo.InvariantCulture);

                if (grid != null)
                {
                    XElement datagrid = xml.Root.Element("window").Element("datagrid");
                    for (int i = 0; i < rowcount; i++)
                    {
                        grid.Columns[i].Width = double.Parse(datagrid.Element("W" + i.ToString()).Value, CultureInfo.InvariantCulture);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd w czasie pobierania formularza danych z pliku xlm: '" + ex.Message + "'", "Błąd odczytu",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
        }

    }
}
