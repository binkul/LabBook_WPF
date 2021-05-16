using System;
using System.Collections.Generic;

namespace LabBook.Forms.MainForm.ModelView
{
    public static class ViscosityColumn
    {
        private static readonly IDictionary<string, bool> Brookfield = new Dictionary<string, bool>
        {
            { "brook_disc", true },
            { "brook_comment", true },
            { "brook_1", true },
            { "brook_5", true },
            { "brook_20", true }
        };

        private static readonly IDictionary<string, bool> BrookProfile = new Dictionary<string, bool>
        {
            { "brook_disc", true },
            { "brook_comment", true },
            { "brook_1", true },
            { "brook_5", true },
            { "brook_10", true },
            { "brook_20", true },
            { "brook_50", true },
            { "brook_100", true }
        };

        private static readonly IDictionary<string, bool> BrookFull = new Dictionary<string, bool>
        {
            { "brook_disc", true },
            { "brook_comment", true },
            { "brook_1", true },
            { "brook_5", true },
            { "brook_10", true },
            { "brook_20", true },
            { "brook_30", true },
            { "brook_40", true },
            { "brook_50", true },
            { "brook_60", true },
            { "brook_70", true },
            { "brook_80", true },
            { "brook_90", true },
            { "brook_100", true }
        };

        private static readonly IDictionary<string, bool> BrookfieldX = new Dictionary<string, bool>
        {
            { "brook_comment", true },
            { "brook_x_vis", true },
            { "brook_x_rpm", true },
            { "brook_x_disc", true }
        };

        private static readonly IDictionary<string, bool> Krebs = new Dictionary<string, bool>
        {
            { "krebs", true },
            { "krebs_comment", true }
        };

        private static readonly IDictionary<string, bool> Ici = new Dictionary<string, bool>
        {
            { "ici", true },
            { "ici_disc", true },
            { "ici_comment", true }
        };

        private static readonly IDictionary<string, IDictionary<string, bool>> Columns = new Dictionary<string, IDictionary<string, bool>>
        {
            { "brookfield", Brookfield },
            { "brookfieldProfile", BrookProfile },
            { "brookfieldFull", BrookFull },
            { "brookfieldX", BrookfieldX  },
            { "krebs", Krebs },
            { "ici", Ici}
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
