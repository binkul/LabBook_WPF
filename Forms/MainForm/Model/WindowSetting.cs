using LabBook.Forms.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabBook.Forms.MainForm.Model
{
    static class WindowSetting
    {
        private static readonly string _path = "\\Data\\Forms\\LabBookForm.xml";
        private static readonly WindowData defaultData = new WindowData(50d, 50d, 900d, 600d, 140d, 140d, 140d, 140d, 140d, 140d);

        public static WindowData Read()
        {
            IList<double> list = WindowsOperation.LoadWindowSettings(_path);
            try
            {
                return new WindowData(list[0], list[1], list[2], list[3], list[4], list[5], list[6], list[7], list[8], list[9]);
            }
            catch
            {
                return defaultData;
            }
        }

        public static void Save(WindowData data)
        {
            IList<double> list = new List<double>
            {
                data.FormXpos,
                data.FormYpos,
                data.FormWidth,
                data.FormHeight,
                data.ColId,
                data.ColTitle,
                data.ColUser,
                data.ColCycle,
                data.ColDensity,
                data.ColDate
            };
            WindowsOperation.SaveWindowsSettings(list, _path);

        }
    }
}
