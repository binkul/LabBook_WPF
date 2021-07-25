using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabBook.Forms.SemiProduct.ModelView
{
    public class FilterMV
    {
        private SemiProductFormMV _semiProductMV;
        private bool _on = false;
        private string _numberD;
        private string _name;
        private string _functionId;
        private bool _danger;

        public void SetWindowEdit(SemiProductFormMV mv)
        {
            _semiProductMV = mv;
        }

        public bool FilterOn
        {
            private get => _on;
            set
            {
                _on = value;
                if (_semiProductMV != null)
                    _semiProductMV.SetFiltration(FilterOn, GetFilterString());
            }
        }

        public string NumberD
        {
            private get => _numberD;
            set
            {
                _numberD = value;
                if (_semiProductMV != null)
                    _semiProductMV.SetFiltration(FilterOn, GetFilterString());
            }
        }
        
        public string Name
        {
            private get => _name;
            set
            {
                _name = value;
                if (_semiProductMV != null)
                    _semiProductMV.SetFiltration(FilterOn, GetFilterString());
            }
        }

        public string Function
        {
            private get => _functionId;
            set
            {
                _functionId = value;
                if (_semiProductMV != null)
                    _semiProductMV.SetFiltration(FilterOn, GetFilterString());
            }
        }

        public bool Danger
        {
            private get => _danger;
            set
            {
                _danger = value;
                if (_semiProductMV != null)
                    _semiProductMV.SetFiltration(FilterOn, GetFilterString());
            }
        }

        public string GetFilterString()
        {
            StringBuilder filter = new StringBuilder("");
            if (FilterOn)
            {

            }
            return filter.ToString();
        }
    }
}
