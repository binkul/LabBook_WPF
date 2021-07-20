using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabBook.Forms.ClpData.Model
{
    public class SelectedClpData
    {
        public IDictionary<int, bool> GHS { get; set; }
        public IList<int> CLP { get; set; }

        public SelectedClpData(IDictionary<int, bool> gHS, IList<int> cLP)
        {
            GHS = gHS;
            CLP = cLP;
        }
    }
}
