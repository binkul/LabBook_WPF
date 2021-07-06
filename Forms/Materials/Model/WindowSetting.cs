using LabBook.Forms.Tools;
using System;
using System.Collections.Generic;

namespace LabBook.Forms.Materials.Model
{
    public class WindowSetting
    {
        private static readonly string _path = "\\Data\\Forms\\MaterialForm.xml";
        private static readonly WindowData defaultData = new WindowData(50d, 50d, 900d, 600d, 100d, 100d, 100d, 100d, 100d, 50d, 50d, 50d);

        public static WindowData Read()
        {
            IList<double> list = WindowsOperation.LoadWindowSettings(_path);
            try
            {
                return list != null ? new WindowData(list[0], list[1], list[2], list[3], list[4], list[5], list[6], list[7], list[8], list[9], list[10], list[11]) : defaultData;
            }
            catch (Exception)
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
                data.NameWidth,
                data.FunctionWidth,
                data.PriceWidth,
                data.CurrencyWidth,
                data.UnitWidth,
                data.DengerWidth,
                data.ProdWidth,
                data.ActivWidth,
            };
            WindowsOperation.SaveWindowsSettings(list, _path);
        }
    }
}
