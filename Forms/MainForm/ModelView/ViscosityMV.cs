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
    public static class ViscosityType
    {
        public static string Brookfield = "brookfield";
        public static string BrookfieldX = "brookfield_x";
        public static string Krebs = "krebs";
        public static string ICI = "ici";
    }

    public class ViscosityMV : INotifyPropertyChanged
    {
        private ICommand _delBrookViscosity;

        private readonly ExperimentalVisService _service = new ExperimentalVisService();
        private WindowEditMV _windowEditMV;
        private long _brookfieldIndex = 0;
        private long _krebsIndex = 0;
        private long _iciIndex = 0;
        private DataRowView _actualBrookfieldRow;
        private DataRowView _actualKrebsRow;
        private DataRowView _actualIcidRow;
        private long _labBookId;
        private bool _profilStd = true;
        private bool _profilExt = false;
        private bool _profilFull = false;
        public RelayCommand<InitializingNewItemEventArgs> OnInitializingNewBrookfieldCommand { get; set; }
        public RelayCommand<InitializingNewItemEventArgs> OnInitializingNewKrebsCommand { get; set; }
        public RelayCommand<InitializingNewItemEventArgs> OnInitializingNewIciCommand { get; set; }

        public ViscosityMV()
        {
            OnInitializingNewBrookfieldCommand = new RelayCommand<InitializingNewItemEventArgs>(this.OnInitializingNewBrookfieldCommandExecuted);
            OnInitializingNewKrebsCommand = new RelayCommand<InitializingNewItemEventArgs>(this.OnInitializingNewKrebsCommandExecuted);
            OnInitializingNewIciCommand = new RelayCommand<InitializingNewItemEventArgs>(this.OnInitializingNewIciCommandExecuted);
        }

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

        public long DgBrookfieldRowIndex
        {
            get
            {
                return _brookfieldIndex;
            }
            set
            {
                _brookfieldIndex = value;
            }
        }
        
        public long DgKrebsRowIndex
        {
            get
            {
                return _krebsIndex;
            }
            set
            {
                _krebsIndex = value;
            }
        }
        
        public long DgIciRowIndex
        {
            get
            {
                return _iciIndex;
            }
            set
            {
                _iciIndex = value;
            }
        }

        public DataRowView ActualBrookfieldRow
        {
            get
            {
                return _actualBrookfieldRow;
            }
            set
            {
                if (value != null)
                    _actualBrookfieldRow = value;
            }
        }
        
        public DataRowView ActualKrebsRow
        {
            get
            {
                return _actualKrebsRow;
            }
            set
            {
                if (value != null)
                    _actualKrebsRow = value;
            }
        }
        
        public DataRowView ActualIciRow
        {
            get
            {
                return _actualIcidRow;
            }
            set
            {
                if (value != null)
                    _actualIcidRow = value;
            }
        }

        public DataView GetBrookView
        {
            get
            {
                if (_service != null)
                    return _service.GetBrookfield;
                else
                    return null;
            }
        }

        public DataView GetKrebsView
        {
            get
            {
                if (_service != null)
                    return _service.GetKrebs;
                else
                    return null;
            }
        }

        public DataView GetIciView
        {
            get
            {
                if (_service != null)
                    return _service.GetICI;
                else
                    return null;
            }
        }

        public int RowCount
        {
            get
            {
                return GetBrookView.Count;
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
                OnPropertyChanged(nameof(ProfilStandard), nameof(ProfilExtOrFull));
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
                OnPropertyChanged(nameof(ProfilExtend), nameof(ProfilExtOrFull));
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
                OnPropertyChanged(nameof(ProfilFull), nameof(ProfilExtOrFull));
            }
        }

        public bool ProfilExtOrFull
        {
            get
            {
                return ProfilFull || ProfilExtend;
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
                .Select(x => x["id"])
                .DefaultIfEmpty(-1)
                .Max(x => x);

            var view = e.NewItem as DataRowView;
            view.Row["id"] = Convert.ToInt64(maxId) + 1;
            view.Row["labbook_id"] = id;
            view.Row["vis_type"] = ViscosityType.Brookfield;
            view.Row["date_created"] = date;
            view.Row["date_update"] = DateTime.Now;
        }

        public void OnInitializingNewKrebsCommandExecuted(InitializingNewItemEventArgs e)
        {

        }

        public void OnInitializingNewIciCommandExecuted(InitializingNewItemEventArgs e)
        {

        }

        public ICommand DeleteBrookViscosity
        {
            get
            {
                if (_delBrookViscosity == null) _delBrookViscosity = new DelViscosityButton(this);
                return _delBrookViscosity;
            }
        }

        public void Save()
        {
            _ = _service.SaveAndReload(LabBookId);
            OnPropertyChanged(nameof(Modified));
        }

        public void Delete(long id)
        {
            if (ActualBrookfieldRow != null)
                _service.Delete(id);
        }
    }
}