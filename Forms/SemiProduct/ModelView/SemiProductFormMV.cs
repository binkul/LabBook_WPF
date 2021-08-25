using GalaSoft.MvvmLight.Command;
using LabBook.ADO.Service;
using LabBook.Commons;
using LabBook.Dto;
using LabBook.Forms.ClpData;
using LabBook.Forms.Navigation;
using LabBook.Forms.SemiProduct.Command;
using LabBook.Forms.SemiProduct.Model;
using LabBook.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LabBook.Forms.SemiProduct.ModelView
{
    public class SemiProductFormMV : INotifyPropertyChanged, INavigation
    {
        private readonly double _chbWidthHalf = 6.4d;
        private readonly double _startLeftPosition = 5d;
        private readonly double _columnStatus = 32d;

        private ICommand _addNewButton;
        private ICommand _addFromExistingButton;
        private ICommand _saveButton;
        private ICommand _deleteButton;
        private ICommand _clpButton;
        private ICommand _calcPrice;
        private ICommand _calcVOC;

        private bool _ghs01 = true;
        private bool _ghs02 = true;
        private bool _ghs03 = true;
        private bool _ghs04 = true;
        private bool _ghs05 = true;
        private bool _ghs06 = true;
        private bool _ghs07 = true;
        private bool _ghs08 = true;
        private bool _ghs09 = true;

        private bool _progressVisible = false;
        private int _progressCurrentValue = 0;
        private int _progressMax = 0;
        private readonly BackgroundWorker _worker;
        public bool IsNameFocused { get; set; } = false;
        public bool IsGridFocused { get; set; } = false;

        public NavigationMV NavigationMV { get; set; }
        private readonly LabBookDto _labBookDto;
        private readonly WindowData _windowData = WindowSetting.Read();
        private readonly MaterialService _materialService = new MaterialService();
        private long _index = 0;
        private long _materialId = 0;
        private bool _notScroll = true;
        private DataRowView _actualRow;
        private readonly DataView _semiProductView;


        public RelayCommand<SelectionChangedEventArgs> OnSelectionChangedCommand { get; set; }

        public RelayCommand<CancelEventArgs> OnClosingCommand { get; set; }

        public SemiProductFormMV(LabBookDto labBookDto)
        {
            _labBookDto = labBookDto;
            _semiProductView = _materialService.GetAll(MaterialType.SemiProduct);

            OnClosingCommand = new RelayCommand<CancelEventArgs>(OnClosingCommandExecuted);
            OnSelectionChangedCommand = new RelayCommand<SelectionChangedEventArgs>(OnSelectionChangedCommandExecuted);

            _worker = new BackgroundWorker();
            _worker.WorkerReportsProgress = true;
            _worker.WorkerSupportsCancellation = true;
            _worker.DoWork += ProgressRun;
            _worker.ProgressChanged += ProgressChanged;
            _worker.RunWorkerCompleted += ProgressFinished;
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

        public double ColumnNumberD
        {
            get => _windowData.NumberDWidth;
            set
            {
                _windowData.NumberDWidth = value;
                OnPropertyChanged(
                    nameof(ColumnNumberD),
                    nameof(TxtFilerNumberLeftPosition),
                    nameof(TxtFilerNameLeftPosition),
                    nameof(CmbFilterFunctionLeftPosition)
                    );
            }
        }

        public double ColumnName
        {
            get => _windowData.NameWidth;
            set
            {
                _windowData.NameWidth = value;
                OnPropertyChanged(
                    nameof(ColumnName),
                    nameof(CmbFilterFunctionLeftPosition)
                    );
            }
        }

        public double ColumnFunction
        {
            get => _windowData.FunctionWidth;
            set
            {
                _windowData.FunctionWidth = value;
                OnPropertyChanged(
                    nameof(ColumnFunction)
                    );
            }
        }

        public double ColumnPrice
        {
            get => _windowData.PriceWidth;
            set
            {
                _windowData.PriceWidth = value;
                OnPropertyChanged(
                    nameof(ColumnPrice)
                    );
            }
        }

        public double ColumnDenger
        {
            get => _windowData.DengerWidth;
            set
            {
                _windowData.DengerWidth = value;
                OnPropertyChanged(
                    nameof(ColumnDenger),
                    nameof(ChbFilterDangerLeftPosition),
                    nameof(TxtFilerNumberLeftPosition),
                    nameof(TxtFilerNameLeftPosition),
                    nameof(CmbFilterFunctionLeftPosition)
                    );
            }
        }

        public double ColumnVOC
        {
            get => _windowData.VOCWidth;
            set
            {
                _windowData.VOCWidth = value;
                OnPropertyChanged(nameof(ColumnVOC));
            }
        }

        public double ColumnRemarks
        {
            get => _windowData.RemarksWidth;
            set
            {
                _windowData.RemarksWidth = value;
                OnPropertyChanged(nameof(ColumnRemarks));
            }
        }

        public double ColumnData
        {
            get => _windowData.DataWidth;
            set
            {
                _windowData.DataWidth = value;
                OnPropertyChanged(nameof(ColumnData));
            }
        }

        public bool GHS01
        {
            get => _ghs01;
            private set
            {
                _ghs01 = value;
                OnPropertyChanged(nameof(GHS01));
            }
        }

        public bool GHS02
        {
            get => _ghs02;
            private set
            {
                _ghs02 = value;
                OnPropertyChanged(nameof(GHS02));
            }
        }

        public bool GHS03
        {
            get => _ghs03;
            private set
            {
                _ghs03 = value;
                OnPropertyChanged(nameof(GHS03));
            }
        }

        public bool GHS04
        {
            get => _ghs04;
            private set
            {
                _ghs04 = value;
                OnPropertyChanged(nameof(GHS04));
            }
        }

        public bool GHS05
        {
            get => _ghs05;
            private set
            {
                _ghs05 = value;
                OnPropertyChanged(nameof(GHS05));
            }
        }

        public bool GHS06
        {
            get => _ghs06;
            private set
            {
                _ghs06 = value;
                OnPropertyChanged(nameof(GHS06));
            }
        }

        public bool GHS07
        {
            get => _ghs07;
            private set
            {
                _ghs07 = value;
                OnPropertyChanged(nameof(GHS07));
            }
        }

        public bool GHS08
        {
            get => _ghs08;
            private set
            {
                _ghs08 = value;
                OnPropertyChanged(nameof(GHS08));
            }
        }

        public bool GHS09
        {
            get => _ghs09;
            private set
            {
                _ghs09 = value;
                OnPropertyChanged(nameof(GHS09));
            }
        }

        public bool IsProgressVisible
        {
            get => _progressVisible;
            set
            {
                _progressVisible = value;
                OnPropertyChanged(nameof(IsProgressVisible));
            }
        }

        public int ProgressValue
        {
            get => _progressCurrentValue;
            set
            {
                if (_progressCurrentValue != value)
                {
                    _progressCurrentValue = value;
                    OnPropertyChanged(nameof(ProgressValue));
                }
            }
        }

        public int ProgressMaximum
        {
            get => _progressMax;
            set
            {
                _progressMax = value;
                OnPropertyChanged(nameof(ProgressMaximum));
            }
        }

        public double ChbFilterDangerLeftPosition => _startLeftPosition + _columnStatus + (ColumnDenger / 2) - _chbWidthHalf;

        public double TxtFilerNumberLeftPosition => _startLeftPosition + _columnStatus + ColumnDenger;

        public double TxtFilerNameLeftPosition => TxtFilerNumberLeftPosition + ColumnNumberD;

        public double CmbFilterFunctionLeftPosition => TxtFilerNameLeftPosition + ColumnName;

        public DataView ClpData
        {
            get
            {
                if (_materialService != null)
                    return _materialService.GetAllClpView(_materialId);
                else
                    return null;
            }
        }

        public DataView GetSemiProductView => _semiProductView;

        public DataRowView ActualRow
        {
            get => _actualRow;
            set
            {
                _actualRow = value;
                OnPropertyChanged(
                    nameof(IsPermited),
                    nameof(CanDelete),
                    nameof(IsDanger)
                    );
            }
        }

        public long MaterialId
        {
            get => _materialId;
            set
            {
                _materialId = value;
                RefreshClp();
            }
        }

        public bool Modified => _materialService.Modified && IsBusy;

        public long GetRowCount => GetSemiProductView.Count;

        public long DgRowIndex
        {
            get => _index;
            set
            {
                _index = value;
                _notScroll = false;
                OnPropertyChanged(nameof(DgRowIndex));
                Refresh();
            }
        }

        public void Refresh()
        {
            NavigationMV.Refresh();
        }

        public bool IsEmptyView => _semiProductView.Count > 0;

        public bool IsDanger => ActualRow != null && Convert.ToBoolean(ActualRow["is_danger"]) && IsBusy;

        public bool IsPermited
        {
            get
            {
                if (_actualRow == null)
                    return false;
                else
                {
                    if (UserSingleton.Id == Convert.ToInt64(_actualRow["login_id"]) && IsBusy)
                        return true;
                    else if (UserSingleton.Id != Convert.ToInt64(_actualRow["login_id"]) && UserSingleton.Permission.Equals("admin") & IsBusy)
                        return true;
                    else
                        return false;
                }
            }
        }

        public bool IsBusy => !_worker.IsBusy;

        public bool CanDelete => IsPermited && IsBusy && GetRowCount > 0;

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
            if (_notScroll) return;

            #region scroll to selected row
            var grid = (DataGrid)e.Source;
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
            //grid.Focus();
            _notScroll = true;
            #endregion
        }

        public ICommand AddNewButton
        {
            get
            {
                if (_addNewButton == null) _addNewButton = new AddNewButton(this);
                return _addNewButton;
            }
        }

        public ICommand AddFromExistingButton
        {
            get
            {
                if (_addFromExistingButton == null) _addFromExistingButton = new AddFromExistingButton(this);
                return _addFromExistingButton;
            }
        }

        public ICommand SaveButton
        {
            get
            {
                if (_saveButton == null) _saveButton = new SaveButton(this);
                return _saveButton;
            }
        }

        public ICommand DeleteButton
        {
            get
            {
                if (_deleteButton == null) _deleteButton = new DeleteButton(this);
                return _deleteButton;
            }
        }

        public ICommand ClpButton
        {
            get
            {
                if (_clpButton == null) _clpButton = new ClpButton(this);
                return _clpButton;
            }
        }

        public ICommand CalcPriceButton
        {
            get
            {
                if (_calcPrice == null) _calcPrice = new PriceButton(this);
                return _calcPrice;
            }
        }

        public ICommand CalcVocButton
        {
            get
            {
                if (_calcVOC == null) _calcVOC = new VocButton(this);
                return _calcVOC;
            }
        }

        private void RefreshClp()
        {
            IDictionary<int, bool> ghs = _materialService.GetAllGhs(MaterialId);
            GHS01 = ghs[1];
            GHS02 = ghs[2];
            GHS03 = ghs[3];
            GHS04 = ghs[4];
            GHS05 = ghs[5];
            GHS06 = ghs[6];
            GHS07 = ghs[7];
            GHS08 = ghs[8];
            GHS09 = ghs[9];
            _materialService.RefreshClpView(MaterialId);
            OnPropertyChanged(nameof(ClpData));
        }

        public void SetFiltration(bool filterOn, string filter)
        {
            _semiProductView.RowFilter = filterOn ? filter : "";
            DgRowIndex = 0;
        }

        public void SaveAll()
        {
            _ = _materialService.Update();
        }

        public void AddNewRecord()
        {
            MaterialDto semiProduct = _materialService.AddNewSemiProduct();
            GoToNewIndex(semiProduct);
        }

        public void AddNewFromExisting()
        {
            MaterialDto semiProduct = _materialService.AddNewSemiProduct(_labBookDto.Id, _labBookDto.Title);
            GoToNewIndex(semiProduct);
        }

        public void CalculatePrice()
        {
            if (_worker.IsBusy)
                return;
            _worker.RunWorkerAsync();
        }

        public void CalculateVOC()
        {
            IsProgressVisible = true;
            ProgressMaximum = _semiProductView.Count;
            ProgressValue = 1;
            _materialService.CalculateSemiProductVOC(this);
            IsProgressVisible = false;
        }

        private void GoToNewIndex(MaterialDto semiProduct)
        {
            if (semiProduct != null)
            {
                int index = 0;
                foreach (DataRowView row in _semiProductView)
                {
                    if (Convert.ToInt64(row["intermediate_nrD"]) == semiProduct.IntermediateNrD)
                    {
                        DgRowIndex = index;
                        OnPropertyChanged(nameof(DgRowIndex));
                        break;
                    }
                    index++;
                }
            }
        }

        public void DeleteSemiProduct()
        {
            if (ActualRow != null)
            {
                _ = _materialService.Delete(ActualRow);
                int index = 0;
                foreach(DataRowView view in _semiProductView)
                {
                    if (view["id"].Equals(ActualRow["id"]))
                        break;
                    index++;
                }
                _semiProductView.Delete(index);
                OnPropertyChanged(nameof(GetSemiProductView));
            }
        }

        public void OpenClpForm()
        {
            if (_worker.IsBusy)
                return;
            IDictionary<int, bool> ghs = CollectGHS();
            IList<int> clp = CollectClpId();
            string name = ActualRow["name"].ToString();

            ClpForm clpForm = new ClpForm(ghs, clp, name);
            if (clpForm.ShowDialog() == true)
            {
                _ = _materialService.UpdateGhsAndClp(MaterialId, clpForm.GetResult);
            }
            RefreshClp();
        }

        private IDictionary<int, bool> CollectGHS()
        {
            return new Dictionary<int, bool>() {
                {1, GHS01 },
                {2, GHS02 },
                {3, GHS03 },
                {4, GHS04 },
                {5, GHS05 },
                {6, GHS06 },
                {7, GHS07 },
                {8, GHS08 },
                {9, GHS09 }
            };
        }

        private IList<int> CollectClpId()
        {
            IList<int> clp = new List<int>();
            foreach (DataRowView view in ClpData)
            {
                clp.Add(Convert.ToInt32(view["clp_id"]));
            }
            return clp;
        }

        #region ProgressBar operation

        public void SetFocus()
        {
            // just to refresh form after ProgressBar running
            IsNameFocused = false;
            OnPropertyChanged(nameof(IsNameFocused));
            IsGridFocused = true;
            OnPropertyChanged(nameof(IsGridFocused));
            IsNameFocused = true;
            OnPropertyChanged(nameof(IsNameFocused));
            IsGridFocused = false;
            OnPropertyChanged(nameof(IsGridFocused));
        }

        private void ProgressRun(object sender, DoWorkEventArgs e)
        {
            IsProgressVisible = true;
            ProgressMaximum = _semiProductView.Count;
            int count = 0;

            foreach (DataRowView row in _semiProductView)
            {
                long numberD = Convert.ToInt64(row["intermediate_nrD"]);
                double price = _materialService.CalculatePrice(numberD, 100d);
                row["price"] = price;
                count++;
                (sender as BackgroundWorker).ReportProgress(count);
            }

        }

        private void ProgressFinished(object sender, RunWorkerCompletedEventArgs e)
        {
            IsProgressVisible = false;
            SetFocus();
        }

        private void ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ProgressValue = e.ProgressPercentage;
        }

        #endregion
    }
}
