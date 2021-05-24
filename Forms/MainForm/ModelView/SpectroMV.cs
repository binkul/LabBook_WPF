using LabBook.ADO.Service;
using LabBook.Forms.MainForm.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LabBook.Forms.MainForm.ModelView
{
    public static class SpectroType
    {
        public const string Dry = "dry";
        public const string Full = "full";
        public const string XYZ = "xyz";
    }

    public class SpectroMV : INotifyPropertyChanged
    {
        private ICommand _delSpectro;

        private readonly ExperimentalSpectroService _service = new ExperimentalSpectroService();
        private WindowEditMV _windowEditMV;
        private long _dataGridRowIndex;
        private DataRowView _actualDatGridRow;
        private bool _spectroDry = true;
        private bool _spectroFull = false;
        private bool _spectroXYZ = false;
        private string _spectroColumns = SpectroType.Dry;
        public event PropertyChangedEventHandler PropertyChanged;
        public RelayCommand<InitializingNewItemEventArgs> OnInitializingNewSpectroCommand { get; set; }

        public SpectroMV()
        {
            OnInitializingNewSpectroCommand = new RelayCommand<InitializingNewItemEventArgs>(this.OnInitializingNewSpectroCommandExecuted);
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

        public DataView GetSpectroView
        {
            get
            {
                if (_service != null)
                    return _service.GetSpectroView;
                else
                    return null;
            }
        }

        public IDictionary<string, bool> GetDgColumns
        {
            get
            {
                return SpectroColumn.GetColumn(_spectroColumns);
            }
        }

        public bool VisibilityDry
        {
            get
            {
                return _spectroDry;
            }
            set
            {
                _spectroDry = value;
                if (value)
                {
                    _spectroColumns = SpectroType.Dry;
                    OnPropertyChanged(nameof(VisibilityDry), nameof(GetDgColumns));
                }
            }
        }

        public bool VisibilityFull
        {
            get
            {
                return _spectroFull;
            }
            set
            {
                _spectroFull = value;
                if (value)
                {
                    _spectroColumns = SpectroType.Full;
                    OnPropertyChanged(nameof(VisibilityFull), nameof(GetDgColumns));
                }
            }
        }

        public bool VisibilityXYZ
        {
            get
            {
                return _spectroXYZ;
            }
            set
            {
                _spectroXYZ = value;
                if (value)
                {
                    _spectroColumns = SpectroType.XYZ;
                    OnPropertyChanged(nameof(VisibilityXYZ), nameof(GetDgColumns));
                }
            }
        }

        public void RefreshMainTable(long labBookId)
        {
            _service.RefreshMainTable(labBookId);
        }

        public ICommand DeleteSpectro
        {
            get
            {
                if (_delSpectro == null) _delSpectro = new DelSpectroButton(this);
                return _delSpectro;
            }
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
                GetSpectroView.Delete((int)DataGriddRowIndex);
                _service.Delete(id);
            }
        }

        private void OnInitializingNewSpectroCommandExecuted(InitializingNewItemEventArgs e)
        {
            var row = _windowEditMV.ActualRow;
            var id = Convert.ToInt64(row["id"]);
            var date = Convert.ToDateTime(row["created"]);

            var maxId = _service.GetSpectroTable.AsEnumerable()
                .Where(x => x.RowState != DataRowState.Deleted)
                .Select(x => x["id"])
                .DefaultIfEmpty(-1)
                .Max(x => x);

            var view = e.NewItem as DataRowView;
            view.Row["id"] = Convert.ToInt64(maxId) + 1;
            view.Row["labbook_id"] = id;
            view.Row["date_created"] = date;
            view.Row["date_update"] = DateTime.Now;
        }
    }
}
