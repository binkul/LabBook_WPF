using LabBook.Forms.MainForm.Model;
using System;
using System.Collections.Generic;
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
                var col = 0;
                for (int i = 0; i < rowcount; i++)
                {
                    if (grid.Columns[i].Visibility == Visibility.Visible)
                    {
                        var columnWidth = new XElement("W" + col.ToString(), grid.Columns[i].ActualWidth);
                        dataGrid.Add(columnWidth);
                        col++;
                    }
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
                    var col = 0;
                    for (int i = 0; i < rowcount; i++)
                    {
                        if (grid.Columns[i].Visibility == Visibility.Visible)
                        {
                            grid.Columns[i].Width = double.Parse(datagrid.Element("W" + col.ToString()).Value, CultureInfo.InvariantCulture);
                            col++;
                        }
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

        public static void SaveWindowsSettings(IList<double> data, string file)
        {
            var declaration = new XDeclaration("1.0", "utf-8", "yes");
            var comment = new XComment("Window position, size and DataGrid columns width");
            var parameters = new XElement("parameters");
            var window = new XElement("window");
            var dataGrid = new XElement("datagrid");
            var position = new XElement("position");
            var size = new XElement("size");
            var x = new XElement("x", data[0]);
            var y = new XElement("y", data[1]);
            var width = new XElement("width", data[2]);
            var height = new XElement("height", data[3]);

            position.Add(x);
            position.Add(y);
            size.Add(width);
            size.Add(height);
            window.Add(position);
            window.Add(size);

            var col = 0;
            for (int i = 4; i < data.Count; i++)
            {
                var columnWidth = new XElement("W" + col.ToString(), data[i]);
                dataGrid.Add(columnWidth);
                col++;
            }
            window.Add(dataGrid);

            parameters.Add(window);

            XDocument xml = new XDocument { Declaration = declaration };
            xml.Add(comment);
            xml.Add(parameters);

            string path = Directory.GetCurrentDirectory() + file;
            if (!Directory.Exists(Path.GetDirectoryName(path)))
                Directory.CreateDirectory(Path.GetDirectoryName(path));

            xml.Save(path);
        }

        public static IList<double> LoadWindowSettings(string file)
        {
            string path = Directory.GetCurrentDirectory() + file;
            IList<double> result = new List<double>();

            if (!File.Exists(path))
                return null;
            try
            {
                XDocument xml = XDocument.Load(path);

                XElement position = xml.Root.Element("window").Element("position");
                result.Add(double.Parse(position.Element("x").Value, CultureInfo.InvariantCulture));
                result.Add(double.Parse(position.Element("y").Value, CultureInfo.InvariantCulture));

                XElement size = xml.Root.Element("window").Element("size");
                result.Add(double.Parse(size.Element("width").Value, CultureInfo.InvariantCulture));
                result.Add(double.Parse(size.Element("height").Value, CultureInfo.InvariantCulture));

                return LoadGridData(result, xml);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd w czasie pobierania formularza danych z pliku xlm: '" + ex.Message + "'", "Błąd odczytu",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return null;
            }
        }

        private static IList<double> LoadGridData(IList<double> list, XDocument xml)
        {
            if (list == null && list.Count == 0)
                return list;

            XElement datagrid = xml.Root.Element("window").Element("datagrid");
            var col = 0;
            for (int i = 0; i < 10000; i++)
            {
                var read = datagrid.Element("W" + col.ToString());
                if (read != null)
                {
                    list.Add(double.Parse(datagrid.Element("W" + col.ToString()).Value, CultureInfo.InvariantCulture));
                }
                else
                {
                    return list;
                }
                col++;
            }

            return list;
        }
    }
}
