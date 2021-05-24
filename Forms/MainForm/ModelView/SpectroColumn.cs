using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabBook.Forms.MainForm.ModelView
{
    public static class SpectroColumn
    {
        private static readonly IDictionary<string, bool> Dry = new Dictionary<string, bool>
        {
            { "L_s", true },
            { "a_s", true },
            { "b_s", true },
            { "WI_s", true },
            { "YI_s", true }
        };

        private static readonly IDictionary<string, bool> Full = new Dictionary<string, bool>
        {
            { "L_m", true },
            { "a_m", true },
            { "b_m", true },
            { "WI_m", true },
            { "YI_m", true },
            { "L_s", true },
            { "a_s", true },
            { "b_s", true },
            { "WI_s", true },
            { "YI_s", true }
        };

        private static readonly IDictionary<string, bool> XYZ = new Dictionary<string, bool>
        {
            { "X", true },
            { "Y", true },
            { "Z", true }
        };

        private static readonly IDictionary<string, IDictionary<string, bool>> Columns = new Dictionary<string, IDictionary<string, bool>>
        {
            { "dry", Dry },
            { "full", Full },
            { "xyz", XYZ }
        };

        public static IDictionary<string, bool> GetColumn(string name)
        {
            if (Columns.ContainsKey(name))
                return Columns[name];
            else
                throw new ArgumentOutOfRangeException("Brak takiej kolumny: '" + name + "'");
        }

    }
}
