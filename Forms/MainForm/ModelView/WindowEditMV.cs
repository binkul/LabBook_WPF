using System.Windows.Controls;
using LabBook.ADO.Service;
using LabBook.Security;
using LabBook.Forms.MainForm.Model;
using System.ComponentModel;
using System.Data;
using System.Windows;
using System.Windows.Input;
using LabBook.Forms.MainForm.Command;
using System;
using LabBook.Dto;
using LabBook.Forms.Materials;

namespace LabBook.Forms.MainForm.ModelView
{
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
        private ICommand _refreshButton;
        private ICommand _copyFromButton;
        private ICommand _calculateBurn;
        private ICommand _calculateAndSaveBurn;
        private ICommand _materialButton;

        private readonly WindowData _windowData = WindowSetting.Read();
        private ViscosityMV _viscosityMV;
        private GlossMV _glossMV;
        private OpacityMV _opacityMV;
        private SpectroMV _spectroMV;
        private CommonMV _commonMV;
        private AshBurnMV _ashBurnMV;
        private long _index = 0;
        private long _labBookId = 0;
        private int _tabIndex;
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

        public GlossMV SetGlossMV
        {
            set
            {
                _glossMV = value;
            }
        }

        public OpacityMV SetOpacityMV
        {
            set
            {
                _opacityMV = value;
            }
        }

        public SpectroMV SetSpectroMV
        {
            set
            {
                _spectroMV = value;
            }
        }

        public CommonMV SetCommonMV
        {
            set
            {
                _commonMV = value;
            }
        }

        public AshBurnMV SetAshBurnMV
        {
            set
            {
                _ashBurnMV = value;
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

        public double TxtFilerNumberDLeftPosition => _startLeftPosition + ColumnStatus;

        public double TxtFilterTitleLeftPosition => TxtFilerNumberDLeftPosition + ColumnId;

        public double CmbFilterUserLeftPosition => TxtFilterTitleLeftPosition + ColumnTitle;

        public double CmbFilterCycleLeftPosition => CmbFilterUserLeftPosition + ColumnUser;

        public double TxtFilterDensityLeftPosition => CmbFilterCycleLeftPosition + ColumnCycle;

        public double DpFilterDateLeftPosition => TxtFilterDensityLeftPosition + ColumnDensity;

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

        public long LabBookId
        {
            get
            {
                return _labBookId;
            }
            set
            {
                _labBookId = value;

                if (_viscosityMV != null)
                    _viscosityMV.RefreshMainTable(_labBookId);

                if (_glossMV != null)
                    _glossMV.RefreshMainTable(_labBookId);

                if (_opacityMV != null)
                    _opacityMV.RefreshMainTable(_labBookId);

                if (_spectroMV != null)
                    _spectroMV.RefreshMainTable(_labBookId);

                if (_commonMV != null)
                    _commonMV.RefreshData(_labBookId);

                if (_ashBurnMV != null)
                    _ashBurnMV.RefreshData(_labBookId);
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

        public int TabIndex
        {
            get
            {
                return _tabIndex;
            }
            set
            {
                _tabIndex = value;
            }
        }

        public bool Modified
        {
            get
            {
                if (_viscosityMV != null && _glossMV != null && _opacityMV != null && _spectroMV != null && _commonMV != null && _ashBurnMV != null)
                    return _labBookService.Modified || _viscosityMV.Modified || _glossMV.Modified || _opacityMV.Modified || 
                        _spectroMV.Modified || _commonMV.Modified || _ashBurnMV.Modified;
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
            if (index < 0)
            {
                return;
            }

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

        public ICommand Refresh
        {
            get
            {
                if (_refreshButton == null) _refreshButton = new RefreshButton(this);
                return _refreshButton;
            }
        }

        public ICommand CopyFrom
        {
            get
            {
                if (_copyFromButton == null) _copyFromButton = new CopyFromButton(this);
                return _copyFromButton;
            }
        }

        public ICommand CalculateBurn
        {
            get
            {
                if (_calculateBurn == null) _calculateBurn = new CalculateBurn(this);
                return _calculateBurn;
            }
        }

        public ICommand CalculateAndSaveBurn
        {
            get
            {
                if (_calculateAndSaveBurn == null) _calculateAndSaveBurn = new CalculateAndSaveBurn(this);
                return _calculateAndSaveBurn;
            }
        }

        public ICommand MaterialButton
        {
            get
            {
                if (_materialButton == null) _materialButton = new MaterialButtton(this);
                return _materialButton;
            }
        }

        public void SaveAll()
        {
            _ = _labBookService.Update();
            _viscosityMV.Save();
            _glossMV.Save();
            _opacityMV.Save();
            _spectroMV.Save();
            _commonMV.Save();
            _ashBurnMV.Save();
        }

        public void OpenMaterials()
        {
            MaterialForm material = new MaterialForm();
            material.ShowDialog();
            material = null;
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

            if (_labBookService.AddNewSeries(labBook))
            {
                DgRowIndex = _labBookView.Count - 1;
                UpdateRowIndex();
            }
        }

        public void RefreshAll()
        {
            if (Modified)
            {
                MessageBoxResult answer = MessageBox.Show("Czy zapisać zmiany - przy odświżaniu wszelkie zmiany zostana utracone", "Zapis zmian",
                    MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
                if (answer == MessageBoxResult.Yes)
                {
                    SaveAll();
                }
                else if (answer == MessageBoxResult.Cancel)
                    return;
            }
            _labBookService.RefreshAll();
            DgRowIndex = 0;
            UpdateRowIndex();
        }

        public void CopyFromNumberD()
        {
            LabBookDto record = _labBookService.CopyFromNumberD();
            if (record == null)
                return;

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

        public void CalculateAshAndBurn()
        {
            _ashBurnMV.Calculate();
        }

        public void CalculatAshAndBurnAndSave()
        {
            _ashBurnMV.CalculateAndSave();
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
