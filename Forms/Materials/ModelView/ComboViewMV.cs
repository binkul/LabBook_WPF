using LabBook.ADO.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabBook.Forms.Materials.ModelView
{
    public class ComboViewMV : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ComboListService _service = new ComboListService();

        protected void OnPropertyChanged(params string[] names)
        {
            if (PropertyChanged != null)
            {
                foreach (string name in names)
                    PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public DataView GetCurrencyView
        {
            get
            {
                return _service.GetComboView(ComboType.Currency);
            }
        }

        public DataView GetFunctionView
        {
            get
            {
                return _service.GetComboView(ComboType.MaterialFunction);
            }
        }

        public DataView GetUnitView
        {
            get
            {
                return _service.GetComboView(ComboType.Unit);
            }
        }


    }
}
