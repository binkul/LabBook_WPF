using LabBook.ADO.Common;
using LabBook.ADO.Service;
using LabBook.EntityFramework;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;

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
        //public IList<CmbCurrency> GetCurrencyView
        //{
        //    get
        //    {
        //        using (LabBookContex dataContex = new LabBookContex())
        //        {
        //            IOrderedQueryable<CmbCurrency> result = dataContex.CmbCurrency.OrderBy(n => n.name);
        //            return result.ToList();
        //        }
        //    }
        //}

        public DataView GetFunctionView => _service.GetComboView(ComboType.MaterialFunction);
        //public IList<CmbMaterialFunction> GetFunctionView
        //{
        //    get
        //    {
        //        using (LabBookContex dataContex = new LabBookContex())
        //        {
        //            IOrderedQueryable<CmbMaterialFunction> result = dataContex.CmbMaterialFunction.OrderBy(n => n.name);
        //            return result.ToList();
        //        }
        //    }
        //}

        public DataView GetUnitView => _service.GetComboView(ComboType.Unit);
        //public IList<CmbUnit> GetUnitView
        //{
        //    get
        //    {
        //        using (LabBookContex dataContex = new LabBookContex())
        //        {
        //            IOrderedQueryable<CmbUnit> result = dataContex.CmbUnit.OrderBy(n => n.name);
        //            return result.ToList();
        //        }
        //    }
        //}

        public DataView GetSignalWordView => _service.GetComboView(ComboType.Signal);

        public DataView GetSemiProductTypeView => _service.GetComboView(ComboType.SemiProduct);

        // It is working, but is much slower
        //public IList<CmbSemiProductType> GetSemiProductTypeView
        //{
        //    get
        //    {
        //        using (LabBookContex dataContex = new LabBookContex())
        //        {
        //            var result = dataContex.CmbSemiProductType.SqlQuery(ComboListRepository.SemiProductType); //dataContex.CmbSemiProductType.OrderBy(n => n.name);
        //            return result.ToList();
        //        }
        //    }
        //}
    }
}
