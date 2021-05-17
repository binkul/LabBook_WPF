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
    using System;
    using LabBook.Dto;

    public class WindowEditMV : INotifyPropertyChanged
    {
        private readonly double _startLeftPosition = 5d;

        private ICommand _moveRight;
        private ICommand _moveLeft;
        private ICommand _moveLast;
        private ICommand _moveFirst;
        private ICommand _saveButton;
        private ICommand _deleteButton;
        private ICommand _addNewButton;
        private ICommand _newSerieButton;

        private readonly WindowData _windowData = WindowSetting.Read();
        private ViscosityMV _viscosityMV;
        private long _index = 0;
        private DataRowView _actualRow;
        private readonly LabBookService _labBookService = new LabBookService();
        private readonly ExperimentCycleService _expCycleService = new ExperimentCycleService();
        private readonly UserService _userService = new UserService();
        private DataView _labBookView;
        private DataView _expCycleView;
        private DataView _userView;
        public RelayCommand<SelectionChangedEventArgs> OnSelectionChangedCommand { get; set; }

        public RelayCommand<CancelEventArgs> OnClosingCommand { get; set; }

        public WindowEditMV()
        {
            _labBookView = _labBookService.GetAll();
            _expCycleView = _expCycleService.GetAll();
            _userView = _userService.GetAll();

            OnClosingCommand = new RelayCommand<CancelEventArgs>(this.OnClosingCommandExecuted);
            OnSelectionChangedCommand = new RelayCommand<SelectionChangedEventArgs>(this.OnSelectionChangedCommandExecuted);

            PrepareUsersView();
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

        public ViscosityMV SetViscosityMV
        {
            set
            {
                _viscosityMV = value;
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

        public double ColumnStatus
        {
            get
            {
                return _windowData.ColStatus;
            }
            set
            {
                _windowData.ColStatus = value;
                OnPropertyChanged(
                    nameof(ColumnStatus),
                    nameof(TxtFilerNumberDLeftPosition),
                    nameof(TxtFilterTitleLeftPosition),
                    nameof(CmbFilterUserLeftPosition),
                    nameof(CmbFilterCycleLeftPosition),
                    nameof(TxtFilterDensityLeftPosition),
                    nameof(DpFilterDateLeftPosition));
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
                OnPropertyChanged(
                    nameof(TxtFilerNumberDLeftPosition), 
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
                OnPropertyChanged(
                    nameof(ColumnTitle), 
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
                OnPropertyChanged(
                    nameof(ColumnUser), 
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
                OnPropertyChanged(
                    nameof(ColumnCycle),
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
                OnPropertyChanged(
                    nameof(ColumnDensity),
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
                OnPropertyChanged(
                    nameof(ColumnDate),
                    nameof(DpFilterDateLeftPosition));
            }
        }

        public double TxtFilerNumberDLeftPosition
        {
            get
            {
                return _startLeftPosition +  ColumnStatus;
            }
        }

        public double TxtFilterTitleLeftPosition
        {
            get
            {
                return TxtFilerNumberDLeftPosition + ColumnId;
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

        public bool Modified
        {
            get
            {
                if (_viscosityMV != null)
                    return _labBookService.Modified || _viscosityMV.Modified;
                else
                    return false;
            }
        }

        public DataRowView ActualRow
        {
            get
            {
                return _actualRow;
            }
            set
            {
                _actualRow = value;
                OnPropertyChanged(nameof(IsPermited));
            }
        }

        public bool IsPermited
        {
            get
            {
                if (_actualRow == null)
                    return false;
                else
                {
                    if (Convert.ToBoolean(_actualRow["deleted"]))
                        return false;
                    else if (UserSingleton.Id == Convert.ToInt64(_actualRow["user_id"]))
                        return true;
                    else if (UserSingleton.Id != Convert.ToInt64(_actualRow["user_id"]) && UserSingleton.Permission.Equals("admin"))
                        return true;
                    else
                        return false;
                }
            }
        }

        public bool IsDeleted
        {
            get
            {
                return Convert.ToBoolean(_actualRow["deleted"]);
            }
        }

        public void OnClosingCommandExecuted(CancelEventArgs e)
        {
            var ansver = MessageBoxResult.No;

            if (Modified)
            {
                ansver = MessageBox.Show("Dokonano zmian w rekordach. Czy zapisać zmiany?", "Zamis zmian", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
            }

            if (ansver == MessageBoxResult.Yes)
            {
                SaveAll();
                WindowSetting.Save(_windowData);
            }
            else if (ansver == MessageBoxResult.Cancel)
            {
                e.Cancel = true;
            }
            else
            {
                WindowSetting.Save(_windowData);
            }
        }

        public void OnSelectionChangedCommandExecuted(SelectionChangedEventArgs e)
        {
            var grid = (DataGrid)e.Source;

            #region scroll to selected row
            var index = grid.SelectedIndex;
            if (index < 0) return;

            var item = grid.Items[index];
            DataGridRow row = grid.ItemContainerGenerator.ContainerFromIndex(index) as DataGridRow;
            if (row == null)
            {
                grid.ScrollIntoView(item);
            }
            grid.Focus();
            #endregion
        }

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

        public ICommand Save
        {
            get
            {
                if (_saveButton == null) _saveButton = new SaveButton(this);
                return _saveButton;
            }
        }

        public ICommand AddNew
        {
            get
            {
                if (_addNewButton == null) _addNewButton = new NewButton(this);
                return _addNewButton;
            }
        }

        public ICommand Delete
        {
            get
            {
                if (_deleteButton == null) _deleteButton = new DeleteButton(this);
                return _deleteButton;
            }
        }

        public ICommand AddNewSerie
        {
            get
            {
                if (_newSerieButton == null) _newSerieButton = new NewSerieButton(this);
                return _newSerieButton;
            }
        }

        public void SaveAll()
        {
            _ = _labBookService.Update();
            _viscosityMV.Save();
        }

        public void DeleteExperiment()
        {
            if (_actualRow == null) return;

            var id = Convert.ToInt64(_actualRow["id"]);
            _labBookService.Delete(id);
        }

        public void AddNewRecord()
        {
            var cycleView = _expCycleView[0];
            var cycleId = Convert.ToInt64(cycleView["id"]);
            var userId = UserSingleton.Id;
            var title = "Pusty";
            var labBook = new LabBookDto(title, userId, cycleId);

            LabBookDto record = _labBookService.AddNew(labBook);

            var index = 0;
            foreach (DataRowView row in _labBookView)
            {
                if (Convert.ToInt64(row["id"]) == record.Id)
                {
                    DgRowIndex = index;
                    UpdateRowIndex();
                    break;
                }
                index++;
            }
        }

        public void AddNewSeriesRecords()
        {
            var cycleView = _expCycleView[0];
            var cycleId = Convert.ToInt64(cycleView["id"]);
            var userId = UserSingleton.Id;
            var title = "Pusty";
            var labBook = new LabBookDto(title, userId, cycleId);

            _labBookService.AddNewSeries(labBook);

            DgRowIndex = _labBookView.Count - 1;
            UpdateRowIndex();
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

        private void PrepareUsersView()
        {
            DataTable usersFilter = _userView.ToTable();
            DataRow row = usersFilter.NewRow();
            row["id"] = -1;
            row["name"] = "-- Wszyscy --";
            row["identifier"] = "Brak";
            usersFilter.Rows.Add(row);
        }
    }
}
