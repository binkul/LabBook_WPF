using LabBook.ADO.Service;
using LabBook.Forms.MainForm.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabBook.Forms.MainForm.ModelView
{
    public class CommonMV : INotifyPropertyChanged
    {
        private readonly ExperimentalCommonService _service = new ExperimentalCommonService();
        private WindowEditMV _windowEditMV;
        private ExpCommon _commonModel;
        private bool _modified = false;
        private DataView _scrubingClassView;
        public event PropertyChangedEventHandler PropertyChanged;

        public CommonMV()
        {
            _scrubingClassView = _service.GetScrubingClass();
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

        public ExpCommon Model
        {
            private get
            {
                return _commonModel;
            }
            set
            {
                _commonModel = value;
                OnPropertyChanged(
                    nameof(ScrubBrush),
                    nameof(ScrubISO11998),
                    nameof(ScrubISO11998Class),
                    nameof(ScratchISO6272),
                    nameof(DryingIdegree),
                    nameof(DryingIIIdegree),
                    nameof(Persoz),
                    nameof(Koenig),
                    nameof(FlasCorrosion),
                    nameof(SaltChamber),
                    nameof(Yellowing),
                    nameof(UvChamber),
                    nameof(Schock),
                    nameof(Adhesion),
                    nameof(WaterResistance),
                    nameof(StainResistance),
                    nameof(Hardness),
                    nameof(FlowLimit),
                    nameof(RunOff),
                    nameof(Yield),
                    nameof(Other)
                    );
            }
        }

        public DataView ScrubingClass
        {
            get
            {
                return _scrubingClassView;
            }
        }

        public string ScrubBrush
        {
            get
            {
                return _commonModel != null ? _commonModel.ScrubBrush : "";
            }
            set
            {
                _commonModel.ScrubBrush = value;
                _modified = true;
            }
        }

        public string ScrubISO11998
        {
            get
            {
                return _commonModel != null ? _commonModel.ScrubISO11998 : "";
            }
            set
            {
                _commonModel.ScrubISO11998 = value;
                _modified = true;
            }
        }

        public long ScrubISO11998Class
        {
            get
            {
                return _commonModel != null ? _commonModel.ScrubISO11998Class : 1;
            }
            set
            {
                _commonModel.ScrubISO11998Class = value;
                _modified = true;
            }
        }

        public string ScratchISO6272
        {
            get
            {
                return _commonModel != null ? _commonModel.ScratchISO6272_1 : "";
            }
            set
            {
                _commonModel.ScratchISO6272_1 = value;
                _modified = true;
            }
        }

        public string DryingIdegree
        {
            get
            {
                return _commonModel != null ? _commonModel.DryingISO9117_1 : "";
            }
            set
            {
                _commonModel.DryingISO9117_1 = value;
                _modified = true;
            }
        }

        public string DryingIIIdegree
        {
            get
            {
                return _commonModel != null ? _commonModel.DryingISO9117_3 : "";
            }
            set
            {
                _commonModel.DryingISO9117_3 = value;
                _modified = true;
            }
        }

        public string Persoz
        {
            get
            {
                return _commonModel != null ? _commonModel.PersozISO2409 : "";
            }
            set
            {
                _commonModel.PersozISO2409 = value;
                _modified = true;
            }
        }

        public string Koenig
        {
            get
            {
                return _commonModel != null ? _commonModel.KoenigISO2409 : "";
            }
            set
            {
                _commonModel.KoenigISO2409 = value;
                _modified = true;
            }
        }

        public string FlasCorrosion
        {
            get
            {
                return _commonModel != null ? _commonModel.FlashRust : "";
            }
            set
            {
                _commonModel.FlashRust = value;
                _modified = true;
            }
        }

        public string SaltChamber
        {
            get
            {
                return _commonModel != null ? _commonModel.SaltSprayISO9227 : "";
            }
            set
            {
                _commonModel.SaltSprayISO9227 = value;
                _modified = true;
            }
        }

        public string Yellowing
        {
            get
            {
                return _commonModel != null ? _commonModel.YellowingISO7724 : "";
            }
            set
            {
                _commonModel.YellowingISO7724 = value;
                _modified = true;
            }
        }

        public string UvChamber
        {
            get
            {
                return _commonModel != null ? _commonModel.UV : "";
            }
            set
            {
                _commonModel.UV = value;
                _modified = true;
            }
        }

        public string Schock
        {
            get
            {
                return _commonModel != null ? _commonModel.SchockISO6272 : "";
            }
            set
            {
                _commonModel.SchockISO6272 = value;
                _modified = true;
            }
        }

        public string Adhesion
        {
            get
            {
                return _commonModel != null ? _commonModel.AdhesionISO2409 : "";
            }
            set
            {
                _commonModel.AdhesionISO2409 = value;
                _modified = true;
            }
        }

        public string WaterResistance
        {
            get
            {
                return _commonModel != null ? _commonModel.WaterISO2812_2 : "";
            }
            set
            {
                _commonModel.WaterISO2812_2 = value;
                _modified = true;
            }
        }

        public string StainResistance
        {
            get
            {
                return _commonModel != null ? _commonModel.StainISO2812_4 : "";
            }
            set
            {
                _commonModel.StainISO2812_4 = value;
                _modified = true;
            }
        }

        public string Hardness
        {
            get
            {
                return _commonModel != null ? _commonModel.Hardness : "";
            }
            set
            {
                _commonModel.Hardness = value;
                _modified = true;
            }
        }

        public string FlowLimit
        {
            get
            {
                return _commonModel != null ? _commonModel.FlowLimit : "";
            }
            set
            {
                _commonModel.FlowLimit = value;
                _modified = true;
            }
        }

        public string RunOff
        {
            get
            {
                return _commonModel != null ? _commonModel.RunOff : "";
            }
            set
            {
                _commonModel.RunOff = value;
                _modified = true;
            }
        }

        public string Yield
        {
            get
            {
                return _commonModel != null ? _commonModel.Yield : "";
            }
            set
            {
                _commonModel.Yield = value;
                _modified = true;
            }
        }

        public string Other
        {
            get
            {
                return _commonModel != null ? _commonModel.Other : "";
            }
            set
            {
                _commonModel.Other = value;
                _modified = true;
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
