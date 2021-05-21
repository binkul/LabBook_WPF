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
    public static class ViscosityType
    {
        public const string Brookfield = "brookfield";
        public const string BrookProfile = "brookfieldProfile";
        public const string BrookFull = "brookfieldFull";
        public const string BrookfieldX = "brookfieldX";
        public const string Krebs = "krebs";
        public const string ICI = "ici";
    }

    public class ViscosityMV : INotifyPropertyChanged
    {
        private ICommand _delBrookViscosity;

        private readonly ExperimentalVisService _service = new ExperimentalVisService();
        private WindowEditMV _windowEditMV;
        private long _dataGridRowIndex = 0;
        private DataRowView _actualDatGridRow;
        private bool _profilStd = true;
        private bool _profilExt = false;
        private bool _profilFull = false;
        private bool _profilX = false;
        private bool _profilKrebs = false;
        private bool _profilIci = false;
        private string _profileType = ViscosityType.Brookfield;
        public event PropertyChangedEventHandler PropertyChanged;
        public RelayCommand<InitializingNewItemEventArgs> OnInitializingNewBrookfieldCommand { get; set; }

        public ViscosityMV()
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
               
        public DataView GetViscosityView
        {
            get
            {
                if (_service != null)
                    return _service.GetView;
                else
                    return null;
            }
        }

        public int RowCount
        {
            get
            {
                return GetViscosityView.Count;
            }
        }

        public bool ProfilStandard
        {
            get
            {
                return _profilStd;
            }
            set
            {
                _profilStd = value;
                if (value)
                {
                    _profileType = ViscosityType.Brookfield;
                    _service.SetBrookfieldVisible();
                    OnPropertyChanged(nameof(ProfilStandard), nameof(GetDgColumns));
                }
            }
        }

        public bool ProfilExtend
        {
            get
            {
                return _profilExt;
            }
            set
            {
                _profilExt = value;
                if (value)
                {
                    _profileType = ViscosityType.BrookProfile;
                    _service.SetBrookfieldVisible();
                    OnPropertyChanged(nameof(ProfilExtend), nameof(GetDgColumns));
                }
            }
        }

        public bool ProfilFull
        {
            get
            {
                return _profilFull;
            }
            set
            {
                _profilFull = value;
                if (value)
                {
                    _profileType = ViscosityType.BrookFull;
                    _service.SetBrookfieldVisible();
                    OnPropertyChanged(nameof(ProfilFull), nameof(GetDgColumns));
                }
            }
        }

        public bool ProfilX
        {
            get
            {
                return _profilX;
            }
            set
            {
                _profilX = value;
                if (value)
                {
                    _profileType = ViscosityType.BrookfieldX;
                    _service.SetBrookfieldXVisible();
                    OnPropertyChanged(nameof(ProfilX), nameof(GetDgColumns));
                }
            }
        }
        
        public bool ProfilKrebs
        {
            get
            {
                return _profilKrebs;
            }
            set
            {
                _profilKrebs = value;
                if (value)
                {
                    _profileType = ViscosityType.Krebs;
                    _service.SetKrebsVisible();
                    OnPropertyChanged(nameof(ProfilKrebs), nameof(GetDgColumns));
                }
            }
        }

        public bool ProfilIci
        {
            get
            {
                return _profilIci;
            }
            set
            {
                _profilIci = value;
                if (value)
                {
                    _profileType = ViscosityType.ICI;
                    _service.SetIciVisible();
                    OnPropertyChanged(nameof(ProfilIci), nameof(GetDgColumns));
                }
            }
        }

        public IDictionary<string, bool> GetDgColumns
        {
            get
            {
                return ViscosityColumn.GetColumn(_profileType);
            }
        }

        public bool Modified
        {
            get
            {
                return _service.Modified;
            }
        }

        public void OnInitializingNewBrookfieldCommandExecuted(InitializingNewItemEventArgs e)
        {
            var row = _windowEditMV.ActualRow;
            var id = Convert.ToInt64(row["id"]);
            var date = Convert.ToDateTime(row["created"]);

            var maxId = _service.GetTable.AsEnumerable()
                .Where(x => x.RowState != DataRowState.Deleted)
                .Select(x => x["id"])
                .DefaultIfEmpty(-1)
                .Max(x => x);

            var visType = ViscosityType.Brookfield;
            
            switch(_profileType)
            {
                case ViscosityType.Brookfield:
                    visType = ViscosityType.Brookfield;
                    break;
                case ViscosityType.BrookProfile:
                    visType = ViscosityType.Brookfield;
                    break;
                case ViscosityType.BrookFull:
                    visType = ViscosityType.Brookfield;
                    break;
                case ViscosityType.BrookfieldX:
                    visType = ViscosityType.BrookfieldX;
                    break;
                case ViscosityType.Krebs:
                    visType = ViscosityType.Krebs;
                    break;
                case ViscosityType.ICI:
                    visType = ViscosityType.ICI;
                    break;
                default:
                    visType = ViscosityType.Brookfield;
                    break;
            }

            var view = e.NewItem as DataRowView;
            view.Row["id"] = Convert.ToInt64(maxId) + 1;
            view.Row["labbook_id"] = id;
            view.Row["vis_type"] = visType;
            view.Row["date_created"] = date;
            view.Row["date_update"] = DateTime.Now;
        }

        public ICommand DeleteBrookViscosity
        {
            get
            {
                if (_delBrookViscosity == null) _delBrookViscosity = new DelViscosityButton(this);
                return _delBrookViscosity;
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
                GetViscosityView.Delete((int)DataGriddRowIndex);
                _service.Delete(id);
            }
        }
    }
}