using LabBook.Forms.ClpData.Model;
using LabBook.Forms.ClpData.ModelView;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LabBook.Forms.ClpData
{
    /// <summary>
    /// Logika interakcji dla klasy ClpForm.xaml
    /// </summary>
    public partial class ClpForm : Window
    {
        public ClpForm(IDictionary<int, bool> ghs, IList<int> clp, string name)
        {
            InitializeComponent();
            InitializeGHS(ghs);
            InitializeCLP(clp);
            LblGhs.Content = "Piktogramy GHS dla '" + name + "'";
            LblClp.Content = "Zwroty H i P dla '" + name + "'";
        }

        private void InitializeGHS(IDictionary<int, bool> ghs)
        {
            if (!ghs[1])
            {
                GHS01.Visibility = Visibility.Visible;
                GHS01_ok.Visibility = Visibility.Collapsed;
            }
            if (!ghs[2])
            {
                GHS02.Visibility = Visibility.Visible;
                GHS02_ok.Visibility = Visibility.Collapsed;
            }
            if (!ghs[3])
            {
                GHS03.Visibility = Visibility.Visible;
                GHS03_ok.Visibility = Visibility.Collapsed;
            }
            if (!ghs[4])
            {
                GHS04.Visibility = Visibility.Visible;
                GHS04_ok.Visibility = Visibility.Collapsed;
            }
            if (!ghs[5])
            {
                GHS05.Visibility = Visibility.Visible;
                GHS05_ok.Visibility = Visibility.Collapsed;
            }
            if (!ghs[6])
            {
                GHS06.Visibility = Visibility.Visible;
                GHS06_ok.Visibility = Visibility.Collapsed;
            }
            if (!ghs[7])
            {
                GHS07.Visibility = Visibility.Visible;
                GHS07_ok.Visibility = Visibility.Collapsed;
            }
            if (!ghs[8])
            {
                GHS08.Visibility = Visibility.Visible;
                GHS08_ok.Visibility = Visibility.Collapsed;
            }
            if (!ghs[9])
            {
                GHS09.Visibility = Visibility.Visible;
                GHS09_ok.Visibility = Visibility.Collapsed;
            }
        }

        private void InitializeCLP(IList<int> clp)
        {
            ClpListMV dataContex = DataContext as ClpListMV;
            List<ClpMV> dataList = new List<ClpMV>(dataContex.ClpDataList);

            foreach (int clp_id in clp)
            {
                ClpMV data = dataList.AsEnumerable().FirstOrDefault(i => i.Id == clp_id);
                if (data != null)
                    dataContex.ClpSelectedList.Add(data);
            }
        }

        private void GHS_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            string name = ((Image)sender).Name;

            switch (name)
            {
                case "GHS01":
                    GHS01.Visibility = Visibility.Collapsed;
                    GHS01_ok.Visibility = Visibility.Visible;
                    break;
                case "GHS02":
                    GHS02.Visibility = Visibility.Collapsed;
                    GHS02_ok.Visibility = Visibility.Visible;
                    break;
                case "GHS03":
                    GHS03.Visibility = Visibility.Collapsed;
                    GHS03_ok.Visibility = Visibility.Visible;
                    break;
                case "GHS04":
                    GHS04.Visibility = Visibility.Collapsed;
                    GHS04_ok.Visibility = Visibility.Visible;
                    break;
                case "GHS05":
                    GHS05.Visibility = Visibility.Collapsed;
                    GHS05_ok.Visibility = Visibility.Visible;
                    break;
                case "GHS06":
                    GHS06.Visibility = Visibility.Collapsed;
                    GHS06_ok.Visibility = Visibility.Visible;
                    break;
                case "GHS07":
                    GHS07.Visibility = Visibility.Collapsed;
                    GHS07_ok.Visibility = Visibility.Visible;
                    break;
                case "GHS08":
                    GHS08.Visibility = Visibility.Collapsed;
                    GHS08_ok.Visibility = Visibility.Visible;
                    break;
                case "GHS09":
                    GHS09.Visibility = Visibility.Collapsed;
                    GHS09_ok.Visibility = Visibility.Visible;
                    break;
                default:
                    break;
            }
        }

        private void GHS_ok_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            string name = ((Image)sender).Name;

            switch (name)
            {
                case "GHS01_ok":
                    GHS01_ok.Visibility = Visibility.Collapsed;
                    GHS01.Visibility = Visibility.Visible;
                    break;
                case "GHS02_ok":
                    GHS02_ok.Visibility = Visibility.Collapsed;
                    GHS02.Visibility = Visibility.Visible;
                    break;
                case "GHS03_ok":
                    GHS03_ok.Visibility = Visibility.Collapsed;
                    GHS03.Visibility = Visibility.Visible;
                    break;
                case "GHS04_ok":
                    GHS04_ok.Visibility = Visibility.Collapsed;
                    GHS04.Visibility = Visibility.Visible;
                    break;
                case "GHS05_ok":
                    GHS05_ok.Visibility = Visibility.Collapsed;
                    GHS05.Visibility = Visibility.Visible;
                    break;
                case "GHS06_ok":
                    GHS06_ok.Visibility = Visibility.Collapsed;
                    GHS06.Visibility = Visibility.Visible;
                    break;
                case "GHS07_ok":
                    GHS07_ok.Visibility = Visibility.Collapsed;
                    GHS07.Visibility = Visibility.Visible;
                    break;
                case "GHS08_ok":
                    GHS08_ok.Visibility = Visibility.Collapsed;
                    GHS08.Visibility = Visibility.Visible;
                    break;
                case "GHS09_ok":
                    GHS09_ok.Visibility = Visibility.Collapsed;
                    GHS09.Visibility = Visibility.Visible;
                    break;
                default:
                    break;
            }
        }

        private IDictionary<int, bool> GetResultGHS()
        {
            return new Dictionary<int, bool>() {
                {1, GHS01_ok.Visibility == Visibility.Visible },
                {2, GHS02_ok.Visibility == Visibility.Visible },
                {3, GHS03_ok.Visibility == Visibility.Visible },
                {4, GHS04_ok.Visibility == Visibility.Visible },
                {5, GHS05_ok.Visibility == Visibility.Visible },
                {6, GHS06_ok.Visibility == Visibility.Visible },
                {7, GHS07_ok.Visibility == Visibility.Visible },
                {8, GHS08_ok.Visibility == Visibility.Visible },
                {9, GHS09_ok.Visibility == Visibility.Visible }
            };
        }

        private IList<int> GetResultCLP()
        {
            IList<int> clp = new List<int>();
            ClpListMV dataContex = DataContext as ClpListMV;
            List<ClpMV> dataList = new List<ClpMV>(dataContex.ClpSelectedList);

            foreach(ClpMV row in dataList)
            {
                clp.Add(row.Id);
            }
            return clp;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        public SelectedClpData GetResult => new SelectedClpData(GetResultGHS(), GetResultCLP());
    }
}
