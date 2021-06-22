using LabBook.ADO.Service;
using System.ComponentModel;

namespace LabBook.Forms.MainForm.ModelView
{
    public class AshBurnMV : INotifyPropertyChanged
    {
        private readonly ExperimentalAshBurnService _service = new ExperimentalAshBurnService();
        public event PropertyChangedEventHandler PropertyChanged;

        public AshBurnMV()
        {

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
