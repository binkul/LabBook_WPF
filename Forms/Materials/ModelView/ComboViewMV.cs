using LabBook.ADO.Service;
using System.ComponentModel;
using System.Data;

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

        public DataView GetCurrencyView => _service.GetComboView(ComboType.Currency);

        public DataView GetFunctionView => _service.GetComboView(ComboType.MaterialFunction);

        public DataView GetUnitView => _service.GetComboView(ComboType.Unit);

        public DataView GetSignalWordView => _service.GetComboView(ComboType.Signal);

        public DataView GetSemiProductTypeView => _service.GetComboView(ComboType.SemiProduct);
    }
}
