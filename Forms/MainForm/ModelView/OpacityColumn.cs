using System;
using System.Collections.Generic;

namespace LabBook.Forms.MainForm.ModelView
{
    public static class OpacityColumn
    {
        private static readonly IDictionary<string, bool> Standard = new Dictionary<string, bool>
        {
            { "contrast_75", true },
            { "contrast_100", true },
            { "contrast_150", true },
            { "contrast_240", true },
            { "contrast_class", true },
            { "contrast_yield", true }
        };

        private static readonly IDictionary<string, bool> Extend = new Dictionary<string, bool>
        {
            { "contrast_75", true },
            { "tw_75", true },
            { "sp_75", true },
            { "contrast_100", true },
            { "tw_100", true },
            { "sp_100", true },
            { "contrast_150", true },
            { "tw_150", true },
            { "sp_150", true },
            { "contrast_240", true },
            { "tw_240", true },
            { "sp_240", true },
            { "contrast_class", true },
            { "contrast_yield", true }
        };

        private static readonly IDictionary<string, bool> Extra = new Dictionary<string, bool>
        {
            { "other_a_contrast", true },
            { "other_a_type", true },
            { "other_b_contrast", true },
            { "other_b_type", true }
        };

        private static readonly IDictionary<string, IDictionary<string, bool>> Columns = new Dictionary<string, IDictionary<string, bool>>
        {
            { "standard", Standard },
            { "extend", Extend },
            { "extra", Extra }
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
