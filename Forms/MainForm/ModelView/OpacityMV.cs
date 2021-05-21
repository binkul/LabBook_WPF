using LabBook.ADO.Service;
using LabBook.Forms.MainForm.Command;
using System;
using System.ComponentModel;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LabBook.Forms.MainForm.ModelView
{
    public class OpacityMV : INotifyPropertyChanged
    {
        private ICommand _delOpacity;

        private WindowEditMV _windowEditMV;
        private long _dataGridRowIndex;
        private DataRowView _actualDatGridRow;
        private readonly ExperimentalOpacityService _service = new ExperimentalOpacityService();
        public event PropertyChangedEventHandler PropertyChanged;
        public RelayCommand<InitializingNewItemEventArgs> OnInitializingNewBrookfieldCommand { get; set; }

        public OpacityMV()
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

        public DataView GetOpacityView
        {
            get
            {
                if (_service != null)
                    return _service.GetOpacityView;
                else
                    return null;
            }
        }

        public ICommand DelOpacity
        {
            get
            {
                if (_delOpacity == null) _delOpacity = new DelOpacityButton(this);
                return _delOpacity;
            }
        }

        public void RefreshMainTable(long labBookId)
        {
            _service.RefreshMainTable(labBookId);
        }

        public void Save()
        {
            _ = _service.SaveAndReload(_windowEditMV.LabBookId);
            OnPropertyChanged(nameof(Modified));
        }

        public void Delete()
        {
            if (ActualDataGridRow == null || ActualDataGridRow.IsNew) return;

            if (MessageBox.Show("Czy usunąć zaznaczony rekord?", "Usuwanie", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                var id = Convert.ToInt64(ActualDataGridRow.Row["id"]);
                GetOpacityView.Delete((int)DataGriddRowIndex);
                _service.Delete(id);
            }
        }

        public void OnInitializingNewBrookfieldCommandExecuted(InitializingNewItemEventArgs e)
        {
            //var row = _windowEditMV.ActualRow;
            //var id = Convert.ToInt64(row["id"]);
            //var date = Convert.ToDateTime(row["created"]);

            //var maxId = _service.GetGlossTable.AsEnumerable()
            //    .Where(x => x.RowState != DataRowState.Deleted)
            //    .Select(x => x["id"])
            //    .DefaultIfEmpty(-1)
            //    .Max(x => x);

            //var view = e.NewItem as DataRowView;
            //view.Row["id"] = Convert.ToInt64(maxId) + 1;
            //view.Row["labbook_id"] = id;
            //view.Row["gloss_class"] = 1;
            //view.Row["date_created"] = date;
            //view.Row["date_update"] = DateTime.Now;
        }

    }
}
