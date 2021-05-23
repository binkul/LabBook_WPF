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
    public static class OpacityType
    {
        public const string Standard = "standard";
        public const string Extend = "extend";
        public const string Extra = "extra";
    }

    public class OpacityMV : INotifyPropertyChanged
    {
        private ICommand _delOpacity;

        private bool _opacityStd = true;
        private bool _opacityExtend = false;
        private bool _opacityExtra = false;
        private string _opacityColumns = OpacityType.Standard;
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

        public DataView GetClassView
        {
            get
            {
                if (_service != null)
                    return _service.GetClassView;
                else
                    return null;
            }
        }

        public DataView GetYieldView
        {
            get
            {
                return _service.GetYieldView;
            }
        }

        public DataView GetAppTypeView
        {
            get
            {
                return _service.GetAppTypeView;
            }
        }

        public IDictionary<string, bool> GetDgColumns
        {
            get
            {
                return OpacityColumn.GetColumn(_opacityColumns);
            }
        }

        public bool VisibilityStandard
        {
            get
            {
                return _opacityStd;
            }
            set
            {
                _opacityStd = value;
                if (value)
                {
                    _opacityColumns = OpacityType.Standard;
                    OnPropertyChanged(nameof(VisibilityStandard), nameof(GetDgColumns));
                }
            }
        }

        public bool VisibilityExtend
        {
            get
            {
                return _opacityExtend;
            }
            set
            {
                _opacityExtend = value;
                if (value)
                {
                    _opacityColumns = OpacityType.Extend;
                    OnPropertyChanged(nameof(VisibilityExtend), nameof(GetDgColumns));
                }
            }
        }

        public bool VisibilityExtra
        {
            get
            {
                return _opacityExtra;
            }
            set
            {
                _opacityExtra = value;
                if (value)
                {
                    _opacityColumns = OpacityType.Extra;
                    OnPropertyChanged(nameof(VisibilityExtra), nameof(GetDgColumns));
                }
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
            var row = _windowEditMV.ActualRow;
            var id = Convert.ToInt64(row["id"]);
            var date = Convert.ToDateTime(row["created"]);

            var maxId = _service.GetOpacityTable.AsEnumerable()
                .Where(x => x.RowState != DataRowState.Deleted)
                .Select(x => x["id"])
                .DefaultIfEmpty(-1)
                .Max(x => x);

            var view = e.NewItem as DataRowView;
            view.Row["id"] = Convert.ToInt64(maxId) + 1;
            view.Row["labbook_id"] = id;
            view.Row["contrast_class"] = 1;
            view.Row["contrast_yield"] = 1;
            view.Row["other_a_type"] = 1;
            view.Row["other_b_type"] = 1;
            view.Row["date_created"] = date;
            view.Row["date_update"] = DateTime.Now;
        }

    }
}
