using System.Collections;
using System.Collections.Generic;

namespace LabBook.Forms.ClpData.Model
{
    public class ClpList : IEnumerable<Clp>
    {
        private List<Clp> clpList = new List<Clp>();

        public void Add(Clp clp)
        {
            clpList.Add(clp);
        }

        public int ClpListCount => clpList.Count;

        public Clp this[int index] => clpList[index];

        public IEnumerator<Clp> GetEnumerator()
        {
            return clpList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
