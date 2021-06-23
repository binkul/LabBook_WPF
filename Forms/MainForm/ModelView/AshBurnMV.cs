using LabBook.ADO.Service;
using LabBook.Forms.MainForm.Model;
using System.ComponentModel;
using System.Data;

namespace LabBook.Forms.MainForm.ModelView
{
    public class AshBurnMV : INotifyPropertyChanged
    {
        private readonly ExperimentalAshBurnService _service = new ExperimentalAshBurnService();
        private WindowEditMV _windowEditMV;
        private ExpAshBurns _ashBurnModel;
        private DataView _vocClassView;
        private bool _modified = false;
        public event PropertyChangedEventHandler PropertyChanged;

        public AshBurnMV()
        {
            _vocClassView = _service.GetVOCClass();
        }

        protected void OnPropertyChanged(params string[] names)
        {
            if (PropertyChanged != null)
            {
                foreach (string name in names)
                    PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public WindowEditMV SetWindowEditMV
        {
            set
            {
                _windowEditMV = value;
            }
        }

        public ExpAshBurns Model
        {
            private get
            {
                return _ashBurnModel;
            }
            set
            {
                _ashBurnModel = value;
                OnPropertyChanged(
                    nameof(Solids),
                    nameof(Ash450),
                    nameof(Ash900),
                    nameof(Organic),
                    nameof(Titanium),
                    nameof(Chalk),
                    nameof(Others),
                    nameof(VocClassId),
                    nameof(VocContent),
                    nameof(Crucible1),
                    nameof(Crucible2),
                    nameof(Crucible3),
                    nameof(Paint1),
                    nameof(Paint2),
                    nameof(Paint3),
                    nameof(Crucible105_1),
                    nameof(Crucible105_2),
                    nameof(Crucible105_3),
                    nameof(Crucible405_1),
                    nameof(Crucible405_2),
                    nameof(Crucible405_3),
                    nameof(Crucible900_1),
                    nameof(Crucible900_2),
                    nameof(Crucible900_3)
                    );
            }
        }

        public double Solids
        {
            get
            {
                return _ashBurnModel != null ? _ashBurnModel.Solid : 0f;
            }
            set
            {
                _ashBurnModel.Solid = value;
                _modified = true;
            }
        }

        public double Ash450
        {
            get
            {
                return _ashBurnModel != null ? _ashBurnModel.Ash450 : 0f;
            }
            set
            {
                _ashBurnModel.Ash450 = value;
                _modified = true;
            }
        }

        public double Ash900
        {
            get
            {
                return _ashBurnModel != null ? _ashBurnModel.Ash900 : 0f;
            }
            set
            {
                _ashBurnModel.Ash900 = value;
                _modified = true;
            }
        }

        public double Organic
        {
            get
            {
                return _ashBurnModel != null ? _ashBurnModel.Organic : 0f;
            }
            set
            {
                _ashBurnModel.Organic = value;
                _modified = true;
            }
        }

        public double Titanium
        {
            get
            {
                return _ashBurnModel != null ? _ashBurnModel.Titanium : 0f;
            }
            set
            {
                _ashBurnModel.Titanium = value;
                _modified = true;
            }
        }

        public double Chalk
        {
            get
            {
                return _ashBurnModel != null ? _ashBurnModel.Chalk : 0f;
            }
            set
            {
                _ashBurnModel.Chalk = value;
                _modified = true;
            }
        }

        public double Others
        {
            get
            {
                return _ashBurnModel != null ? _ashBurnModel.Others : 0f;
            }
            set
            {
                _ashBurnModel.Others = value;
                _modified = true;
            }
        }

        public int VocClassId
        {
            get
            {
                return _ashBurnModel != null ? _ashBurnModel.VocCatId : 1;
            }
            set
            {
                _ashBurnModel.VocCatId = value;
                _modified = true;
            }
        }

        public string VocContent
        {
            get
            {
                return _ashBurnModel != null ? _ashBurnModel.VocAmount : "";
            }
            set
            {
                _ashBurnModel.VocAmount = value;
                _modified = true;
            }
        }

        public double Crucible1
        {
            get
            {
                return _ashBurnModel != null ? _ashBurnModel.Crucible1 : 0f;
            }
            set
            {
                _ashBurnModel.Crucible1 = value;
                _modified = true;
            }
        }

        public double Crucible2
        {
            get
            {
                return _ashBurnModel != null ? _ashBurnModel.Crucible2 : 0f;
            }
            set
            {
                _ashBurnModel.Crucible2 = value;
                _modified = true;
            }
        }

        public double Crucible3
        {
            get
            {
                return _ashBurnModel != null ? _ashBurnModel.Crucible3 : 0f;
            }
            set
            {
                _ashBurnModel.Crucible3 = value;
                _modified = true;
            }
        }

        public double Paint1
        {
            get
            {
                return _ashBurnModel != null ? _ashBurnModel.Paint1 : 0f;
            }
            set
            {
                _ashBurnModel.Paint1 = value;
                _modified = true;
            }
        }

        public double Paint2
        {
            get
            {
                return _ashBurnModel != null ? _ashBurnModel.Paint2 : 0f;
            }
            set
            {
                _ashBurnModel.Paint2 = value;
                _modified = true;
            }
        }
        
        public double Paint3
        {
            get
            {
                return _ashBurnModel != null ? _ashBurnModel.Paint3 : 0f;
            }
            set
            {
                _ashBurnModel.Paint3 = value;
                _modified = true;
            }
        }

        public double Crucible105_1
        {
            get
            {
                return _ashBurnModel != null ? _ashBurnModel.Crucible105_1 : 0f;
            }
            set
            {
                _ashBurnModel.Crucible105_1 = value;
                _modified = true;
            }
        }

        public double Crucible105_2
        {
            get
            {
                return _ashBurnModel != null ? _ashBurnModel.Crucible105_2 : 0f;
            }
            set
            {
                _ashBurnModel.Crucible105_2 = value;
                _modified = true;
            }
        }

        public double Crucible105_3
        {
            get
            {
                return _ashBurnModel != null ? _ashBurnModel.Crucible105_3 : 0f;
            }
            set
            {
                _ashBurnModel.Crucible105_3 = value;
                _modified = true;
            }
        }

        public double Crucible405_1
        {
            get
            {
                return _ashBurnModel != null ? _ashBurnModel.Crucible405_1 : 0f;
            }
            set
            {
                _ashBurnModel.Crucible405_1 = value;
                _modified = true;
            }
        }

        public double Crucible405_2
        {
            get
            {
                return _ashBurnModel != null ? _ashBurnModel.Crucible405_2 : 0f;
            }
            set
            {
                _ashBurnModel.Crucible405_2 = value;
                _modified = true;
            }
        }

        public double Crucible405_3
        {
            get
            {
                return _ashBurnModel != null ? _ashBurnModel.Crucible405_3 : 0f;
            }
            set
            {
                _ashBurnModel.Crucible405_3 = value;
                _modified = true;
            }
        }

        public double Crucible900_1
        {
            get
            {
                return _ashBurnModel != null ? _ashBurnModel.Crucible900_1 : 0f;
            }
            set
            {
                _ashBurnModel.Crucible900_1 = value;
                _modified = true;
            }
        }

        public double Crucible900_2
        {
            get
            {
                return _ashBurnModel != null ? _ashBurnModel.Crucible900_2 : 0f;
            }
            set
            {
                _ashBurnModel.Crucible900_2 = value;
                _modified = true;
            }
        }

        public double Crucible900_3
        {
            get
            {
                return _ashBurnModel != null ? _ashBurnModel.Crucible900_3 : 0f;
            }
            set
            {
                _ashBurnModel.Crucible900_3 = value;
                _modified = true;
            }
        }

        public DataView VocClassList
        {
            get
            {
                return _vocClassView;
            }
        }

        public void RefreshData(long labBookId)
        {
            Save();
            Model = _service.GetCurrent(labBookId);
        }

        public void Save()
        {
            if (_modified)
                _ = _service.Save(Model);
            _modified = false;
        }

        public bool Modified
        {
            get
            {
                return _modified;
            }
        }

    }
}
