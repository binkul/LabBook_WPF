using LabBook.Forms.ClpData.Model;
using System;
using System.ComponentModel;

namespace LabBook.Forms.ClpData.ModelView
{
    public class ClpMV : INotifyPropertyChanged, IComparable<ClpMV>
    {
        private Clp _clpModel;

        public ClpMV(Clp clpModel)
        {
            _clpModel = clpModel;
        }

        public ClpMV(int id, string clpHP, string clpClass, string clpDescription, int ordering)
        {
            _clpModel = new Clp(id, clpHP, clpClass, clpDescription, ordering);
        }

        #region Get properties

        public int Id => _clpModel.Id;

        public string ClpHP => _clpModel.ClpHP;

        public string ClpClass => _clpModel.ClpClass;

        public string ClpDescription => _clpModel.ClpDescription;

        public int Ordering => _clpModel.Ordering;

        public bool ClpSelected => _clpModel.ClpSelected;

        public Clp GetModel => _clpModel;

        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        public int CompareTo(ClpMV other)
        {
            return Ordering - other.Ordering;
        }

        protected void OnPropertyChanged(params string[] names)
        {
            if (PropertyChanged != null)
            {
                foreach (string name in names)
                    PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

    }
}
