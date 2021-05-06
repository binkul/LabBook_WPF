using LabBook.ADO.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabBook.Forms.MainForm.ModelView
{
    public class ViscosityMV : INotifyPropertyChanged
    {
        private ExperimentalVisService _service;
        private long _labBookId;

        public event PropertyChangedEventHandler PropertyChanged;

        public ExperimentalVisService SetService
        {
            get
            {
                return _service;
            }
            set
            {
                _service = value;
            }
        }

        public long LabBookId
        {
            get
            {
                return _labBookId;
            }
            set
            {
                _labBookId = value;
                if (_service != null)
                    _service.RefreshMainTable(_labBookId);
            }
        }

        public DataView GetBrookView
        {
            get
            {
                return _service.GetBrookfield;
            }
        }

    }
}