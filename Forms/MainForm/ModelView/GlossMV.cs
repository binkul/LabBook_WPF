using LabBook.ADO.Service;
using LabBook.Forms.MainForm.Command;
using System.ComponentModel;
using System.Windows.Input;

namespace LabBook.Forms.MainForm.ModelView
{
    public class GlossMV : INotifyPropertyChanged
    {
        private ICommand _delGloss;

        private readonly ExperimentalGlossService _service = new ExperimentalGlossService();
        private WindowEditMV _windowEditMV;

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

        public bool Modified
        {
            get
            {
                return _service.Modified;
            }
        }


        public ICommand DeleteGloss
        {
            get
            {
                if (_delGloss == null) _delGloss = new DelGlossButton(this);
                return _delGloss;
            }
        }


    }
}
