namespace LabBook.Forms.MainForm.ModelView
{
    using System.Windows.Controls;
    using LabBook.ADO.Service;
    using LabBook.Security;
    using Model;
    using System.ComponentModel;
    using System.Data;
    using System.Windows;
    using System.Windows.Input;
    using LabBook.Forms.MainForm.Command;

    public class WindowEditMV : INotifyPropertyChanged
    {
        private readonly string _allUser = "-- Wszyscy --";
        private readonly double _startLeftPosition = 28d;

        private ICommand _moveRight;
        private ICommand _moveLeft;
        private ICommand _moveLast;
        private ICommand _moveFirst;

        private readonly WindowData _windowData = WindowSetting.Read();
        private long _index = 0;
        private readonly User _user;
        private readonly LabBookService _labBookService;
        private readonly ExperimentCycleService _expCycleService;
        private readonly ExperimentalVisService _expVisService;
        private readonly UserService _userService;
        private DataView _labBookView;
        private DataView _expCycleView;
        private DataView _userView;
        public RelayCommand<CancelEventArgs> OnClosingCommand { get; set; }
        public RelayCommand<RoutedEventArgs> OnClickNavigationCommand { get; set; }

        public WindowEditMV(User user)
        {
            _user = user;
            _labBookService = new LabBookService(_user);
            _expCycleService = new ExperimentCycleService(_user);
            _expVisService = new ExperimentalVisService(_user);
            _userService = new UserService(_user);
            _labBookView = _labBookService.GetAll();
            _expCycleView = _expCycleService.GetAll();
            _userView = _userService.GetAll();

            OnClosingCommand = new RelayCommand<CancelEventArgs>(this.OnClosingCommandExecuted);
            //OnClickNavigationCommand = new RelayCommand<RoutedEventArgs>(this.OnClickNavigationCommandExecuted);

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

        public ExperimentalVisService GetVisService
        {
            get
            {
                return _expVisService;
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

        public long DgRowIndex
        {
            get
            {
                return _index;
            }
            set
            {
                _index = value;
            }
        }

        public void OnClosingCommandExecuted(CancelEventArgs cancelEventArgs)
        {
            if (MessageBox.Show("Czy zamknąć?", "", MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
                cancelEventArgs.Cancel = true;
            WindowSetting.Save(_windowData);
        }

        //public void OnClickNavigationCommandExecuted(RoutedEventArgs routedEventArgs)
        //{
        //    Button button = (Button)routedEventArgs.Source;
        //    var count = GetLabBookView.Count;
        //    var name = button.Tag;
        //    var index = DgRowIndex;

        //    switch (name)
        //    {
        //        case "first":
        //            index = 0;
        //            break;
        //        case "left":
        //            _ = index > 0 ? index-- : index = 0;
        //            break;
        //        case "right":
        //            _ = index < count - 1 ? index++ : index = count - 1;
        //            break;
        //        case "last":
        //            index = count - 1;
        //            break;
        //    }
        //    DgRowIndex = index;
        //    OnPropertyChanged(nameof(DgRowIndex));
        //}

        public ICommand MoveRight
        {
            get
            {
                if (_moveRight == null) _moveRight = new NaviButtonRight(this);
                return _moveRight;
            }
        }

        public ICommand MoveLast
        {
            get
            {
                if (_moveLast == null) _moveLast = new NaviButtonLast(this);
                return _moveLast;
            }
        }
        
        public ICommand MoveFirst
        {
            get
            {
                if (_moveFirst == null) _moveFirst = new NaviButtonFirst(this);
                return _moveFirst;
            }
        }

        public ICommand MoveLeft
        {
            get
            {
                if (_moveLeft == null) _moveLeft = new NaviButtonLeft(this);
                return _moveLeft;
            }
        }

        public void UpdateRowIndex()
        {
            OnPropertyChanged(nameof(DgRowIndex));
        }

        public void SetFiltration(bool filterOn, string filter)
        {
            if (filterOn)
            {
                _labBookView.RowFilter = filter;
            }
            else
            {
                _labBookView.RowFilter = "";
            }
            DgRowIndex = 0;
            OnPropertyChanged(nameof(DgRowIndex));
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
