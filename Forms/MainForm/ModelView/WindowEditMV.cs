namespace LabBook.Forms.MainForm.ModelView
{
    using LabBook.ADO.Service;
    using LabBook.Security;
    using Model;
    using System.ComponentModel;
    using System.Data;
    using System.Windows;

    public class WindowEditMV : INotifyPropertyChanged
    {
        private readonly string _allUser = "-- Wszyscy --";
        private readonly double _startLeftPosition = 28d;

        private readonly WindowData _windowData = WindowSetting.Read();
        private readonly LabBookForm _windowForm;
        private readonly User _user;
        private readonly LabBookService _labBookService;
        private readonly ExperimentCycleService _expCycleService;
        private readonly UserService _userService;
        private DataView _labBookView;
        private DataView _expCycleView;
        private DataView _userView;
        public RelayCommand<CancelEventArgs> OnClosingCommand { get; set; }

        public WindowEditMV(User user, LabBookForm labBookForm)
        {
            _user = user;
            _windowForm = labBookForm;
            _labBookService = new LabBookService(_user);
            _expCycleService = new ExperimentCycleService(_user);
            _userService = new UserService(_user);
            _labBookView = _labBookService.GetAll();
            _expCycleView = _expCycleService.GetAll();
            _userView = _userService.GetAll();

            OnClosingCommand = new RelayCommand<CancelEventArgs>(this.OnClosingCommandExecuted);

            PrepareModelView();
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

        public double FormXpos
        {
            get
            {
                return _windowData.FormXpos;
            }
            set
            {
                _windowData.FormXpos = value;
                OnPropertyChanged(nameof(FormXpos));
            }
        }

        public double FormYpos
        {
            get
            {
                return _windowData.FormYpos;
            }
            set
            {
                _windowData.FormYpos = value;
                OnPropertyChanged(nameof(FormYpos));
            }
        }

        public double FormWidth
        {
            get
            {
                return _windowData.FormWidth;
            }
            set
            {
                _windowData.FormWidth = value;
                OnPropertyChanged(nameof(FormWidth));
            }
        }

        public double FormHeight
        {
            get
            {
                return _windowData.FormHeight;
            }
            set
            {
                _windowData.FormHeight = value;
                OnPropertyChanged(nameof(FormHeight));
            }
        }

        public double ColumnId
        {
            get
            {
                return _windowData.ColId;
            }
            set
            {
                _windowData.ColId = value;
                OnPropertyChanged(nameof(ColumnId), 
                    nameof(TxtFilterTitleLeftPosition), 
                    nameof(CmbFilterUserLeftPosition),
                    nameof(CmbFilterCycleLeftPosition),
                    nameof(TxtFilterDensityLeftPosition),
                    nameof(DpFilterDateLeftPosition));
            }
        }

        public double ColumnTitle
        {
            get
            {
                return _windowData.ColTitle;
            }
            set
            {
                _windowData.ColTitle = value;
                OnPropertyChanged(nameof(ColumnTitle), 
                    nameof(CmbFilterUserLeftPosition),
                    nameof(CmbFilterCycleLeftPosition),
                    nameof(TxtFilterDensityLeftPosition),
                    nameof(DpFilterDateLeftPosition));
            }
        }

        public double ColumnUser
        {
            get
            {
                return _windowData.ColUser;
            }
            set
            {
                _windowData.ColUser = value;
                OnPropertyChanged(nameof(ColumnUser), 
                    nameof(CmbFilterUserLeftPosition),
                    nameof(CmbFilterCycleLeftPosition),
                    nameof(TxtFilterDensityLeftPosition),
                    nameof(DpFilterDateLeftPosition));
            }
        }

        public double ColumnCycle
        {
            get
            {
                return _windowData.ColCycle;
            }
            set
            {
                _windowData.ColCycle = value;
                OnPropertyChanged(nameof(ColumnCycle),
                    nameof(CmbFilterCycleLeftPosition),
                    nameof(TxtFilterDensityLeftPosition),
                    nameof(DpFilterDateLeftPosition));
            }
        }

        public double ColumnDensity
        {
            get
            {
                return _windowData.ColDensity;
            }
            set
            {
                _windowData.ColDensity = value;
                OnPropertyChanged(nameof(ColumnDensity),
                    nameof(TxtFilterDensityLeftPosition),
                    nameof(DpFilterDateLeftPosition));
            }
        }

        public double ColumnDate
        {
            get
            {
                return _windowData.ColDate;
            }
            set
            {
                _windowData.ColDate = value;
                OnPropertyChanged(nameof(ColumnDate),
                    nameof(DpFilterDateLeftPosition));
            }
        }

        public double TxtFilterTitleLeftPosition
        {
            get
            {
                return _startLeftPosition + ColumnId;
            }
        }

        public double CmbFilterUserLeftPosition
        {
            get
            {
                return TxtFilterTitleLeftPosition + ColumnTitle;
            }
        }

        public double CmbFilterCycleLeftPosition
        {
            get
            {
                return CmbFilterUserLeftPosition + ColumnUser;
            }
        }

        public double TxtFilterDensityLeftPosition
        {
            get
            {
                return CmbFilterCycleLeftPosition + ColumnCycle;
            }
        }

        public double DpFilterDateLeftPosition
        {
            get
            {
                return TxtFilterDensityLeftPosition + ColumnDensity;
            }
        }

        public DataView GetLabBookView
        {
            get
            {
                return _labBookView;
            }
        }

        public DataView GetExpCycleView
        {
            get
            {
                return _expCycleView;
            }
        }

        public DataView GetAllUser
        {
            get
            {
                return _userView;
            }
        }

        public void OnClosingCommandExecuted(CancelEventArgs cancelEventArgs)
        {
            if (MessageBox.Show("Czy zamknąć?", "", MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
                cancelEventArgs.Cancel = true;
            WindowSetting.Save(_windowData);
        }

        public void SetFiltration(bool filterOn, string filter)
        {
            if (filterOn)
            {
                _labBookView.RowFilter = filter;
                _windowForm.GoToFirstRecord();
            }
            else
            {
                _labBookView.RowFilter = "";
                _windowForm.GoToFirstRecord();
            }
        }

        private void PrepareModelView()
        {
            DataTable usersFilter = _userView.ToTable();
            DataRow row = usersFilter.NewRow();
            row["id"] = -1;
            row["name"] = _allUser;
            row["identifier"] = "Brak";
            usersFilter.Rows.Add(row);
            DataView viewFilter = new DataView(usersFilter) { Sort = "name" };
        }
    }
}
