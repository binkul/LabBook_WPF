using LabBook.ADO.Service;
using LabBook.Forms.MainForm.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        public event PropertyChangedEventHandler PropertyChanged;

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
            }
        }

        public long Id
        {
            get
            {
                return _commonModel.Id;
            }
            set
            {
                _commonModel.Id = value;
            }
        }

        public string ScrubBrush
        {
            get
            {
                return _commonModel.ScrubBrush;
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
                return _commonModel.ScrubISO11998;
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
                return _commonModel.ScrubISO11998Class;
            }
            set
            {
                _commonModel.ScrubISO11998Class = value;
                _modified = true;
            }
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
