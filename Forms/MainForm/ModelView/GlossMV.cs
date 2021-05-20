using LabBook.ADO.Service;
using LabBook.Forms.MainForm.Command;
using System;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;

namespace LabBook.Forms.MainForm.ModelView
{
    public class GlossMV : INotifyPropertyChanged
    {
        private ICommand _delGloss;

        private readonly ExperimentalGlossService _service = new ExperimentalGlossService();
        private WindowEditMV _windowEditMV;
        private long _dataGridRowIndex;
        private DataRowView _actualDatGridRow;
        public event PropertyChangedEventHandler PropertyChanged;
        public RelayCommand<InitializingNewItemEventArgs> OnInitializingNewBrookfieldCommand { get; set; }

        public GlossMV()
        {
            OnInitializingNewBrookfieldCommand = new RelayCommand<InitializingNewItemEventArgs>(this.OnInitializingNewBrookfieldCommandExecuted);
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

        public bool Modified
        {
            get
            {
                return _service.Modified;
            }
        }

        public long DataGriddRowIndex
        {
            get
            {
                return _dataGridRowIndex;
            }
            set
            {
                _dataGridRowIndex = value;
            }
        }

        public DataRowView ActualDataGridRow
        {
            get
            {
                return _actualDatGridRow;
            }
            set
            {
                if (value != null)
                    _actualDatGridRow = value;
            }
        }

        public DataView GetGlossView
        {
            get
            {
                if (_service != null)
                    return _service.GetGlossView;
                else
                    return null;
            }
        }

        public DataView GetClassView
        {
            get
            {
                return _service.GetClassView;
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

        public void Delete(long id)
        {
            if (ActualDataGridRow != null)
                _service.Delete(id);
        }

        public void OnInitializingNewBrookfieldCommandExecuted(InitializingNewItemEventArgs e)
        {
            var row = _windowEditMV.ActualRow;
            var id = Convert.ToInt64(row["id"]);
            var date = Convert.ToDateTime(row["created"]);

            var maxId = _service.GetGlossTable.AsEnumerable()
                .Select(x => x["id"])
                .DefaultIfEmpty(-1)
                .Max(x => x);


            var view = e.NewItem as DataRowView;
            view.Row["id"] = Convert.ToInt64(maxId) + 1;
            view.Row["labbook_id"] = id;
            view.Row["gloss_class"] = 1;
            view.Row["date_created"] = date;
            view.Row["date_update"] = DateTime.Now;
        }

    }
}
