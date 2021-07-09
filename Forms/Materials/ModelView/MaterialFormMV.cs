using GalaSoft.MvvmLight.Command;
using LabBook.ADO.Service;
using LabBook.Forms.Materials.Command;
using LabBook.Forms.Materials.Model;
using LabBook.Security;
using System;
using System.ComponentModel;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LabBook.Forms.Materials.ModelView
{
    public class MaterialFormMV : INotifyPropertyChanged
    {
        private readonly double _startLeftPosition = 5d;
        private readonly double _columnStatus = 32d;
        private readonly double _chbWidthHalf = 6.4d;

        private ICommand _addNewButton;
        private ICommand _saveButton;
        private ICommand _moveRight;
        private ICommand _moveLeft;
        private ICommand _moveLast;
        private ICommand _moveFirst;

        private readonly WindowData _windowData = WindowSetting.Read();
        private readonly MaterialService _materialService = new MaterialService();
        private long _index = 0;
        private long _materialId = 0;
        private bool _notScroll = true;
        private DataRowView _actualRow;
        private readonly DataView _materialView;

        public RelayCommand<SelectionChangedEventArgs> OnSelectionChangedCommand { get; set; }

        public RelayCommand<CancelEventArgs> OnClosingCommand { get; set; }

        public MaterialFormMV()
        {
            _materialView = _materialService.GetAll();

            OnClosingCommand = new RelayCommand<CancelEventArgs>(this.OnClosingCommandExecuted);
            OnSelectionChangedCommand = new RelayCommand<SelectionChangedEventArgs>(this.OnSelectionChangedCommandExecuted);
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

        public double ColumnName
        {
            get 
            {
                return _windowData.NameWidth;
            }
            set
            {
                _windowData.NameWidth = value;
                OnPropertyChanged(
                    nameof(ColumnName), 
                    nameof(CmbFilterFunctionLeftPosition), 
                    nameof(ChbFilterActiveLeftPosition),
                    nameof(ChbFilterDangerLeftPosition),
                    nameof(ChbFilterProdLeftPosition)
                    );
            }
        }
        
        public double ColumnFunction
        {
            get
            {
                return _windowData.FunctionWidth;
            }
            set
            {
                _windowData.FunctionWidth = value;
                OnPropertyChanged(
                    nameof(ColumnFunction), 
                    nameof(CmbFilterFunctionLeftPosition), 
                    nameof(ChbFilterActiveLeftPosition),
                    nameof(ChbFilterDangerLeftPosition),
                    nameof(ChbFilterProdLeftPosition)
                    );
            }
        }

        public double ColumnPrice
        {
            get
            {
                return _windowData.PriceWidth;
            }
            set
            {
                _windowData.PriceWidth = value;
                OnPropertyChanged(
                    nameof(ColumnPrice), 
                    nameof(ChbFilterActiveLeftPosition),
                    nameof(ChbFilterDangerLeftPosition),
                    nameof(ChbFilterProdLeftPosition)
                    );
            }
        }
        
        public double ColumnCurrency
        {
            get
            {
                return _windowData.CurrencyWidth;
            }
            set
            {
                _windowData.CurrencyWidth = value;
                OnPropertyChanged(
                    nameof(ColumnCurrency), 
                    nameof(ChbFilterActiveLeftPosition),
                    nameof(ChbFilterDangerLeftPosition),
                    nameof(ChbFilterProdLeftPosition)
                    );
            }
        }

        public double ColumnUnit
        {
            get
            {
                return _windowData.UnitWidth;
            }
            set
            {
                _windowData.UnitWidth = value;
                OnPropertyChanged(
                    nameof(ColumnUnit), 
                    nameof(ChbFilterActiveLeftPosition),
                    nameof(ChbFilterDangerLeftPosition),
                    nameof(ChbFilterProdLeftPosition)
                    );
            }
        }

        public double ColumnDenger
        {
            get
            {
                return _windowData.DengerWidth;
            }
            set
            {
                _windowData.DengerWidth = value;
                OnPropertyChanged(
                    nameof(ColumnDenger),
                    nameof(ChbFilterDangerLeftPosition),
                    nameof(ChbFilterProdLeftPosition)
                    );
            }
        }

        public double ColumnProd
        {
            get
            {
                return _windowData.ProdWidth;
            }
            set
            {
                _windowData.ProdWidth = value;
                OnPropertyChanged(
                    nameof(ColumnProd),
                    nameof(ChbFilterProdLeftPosition)
                    );
            }
        }

        public double ColumnActive
        {
            get
            {
                return _windowData.ActivWidth;
            }
            set
            {
                _windowData.ActivWidth = value;
                OnPropertyChanged(
                    nameof(ColumnActive), 
                    nameof(ChbFilterActiveLeftPosition),
                    nameof(ChbFilterDangerLeftPosition),
                    nameof(ChbFilterProdLeftPosition)
                    );
            }
        }

        public double ColumnVOC
        {
            get
            {
                return _windowData.VOCWidth;
            }
            set
            {
                _windowData.VOCWidth = value;
                OnPropertyChanged(nameof(ColumnVOC));
            }
        }

        public double ColumnData
        {
            get
            {
                return _windowData.DataWidth;
            }
            set
            {
                _windowData.DataWidth = value;
                OnPropertyChanged(nameof(ColumnData));
            }
        }

        public double TxtFilerNameLeftPosition => _startLeftPosition + _columnStatus;

        public double CmbFilterFunctionLeftPosition => TxtFilerNameLeftPosition + ColumnName;

        private double StartComboPosition => CmbFilterFunctionLeftPosition + ColumnFunction + ColumnPrice + ColumnCurrency + ColumnUnit;

        public double ChbFilterActiveLeftPosition => StartComboPosition + (ColumnActive / 2) - _chbWidthHalf;

        public double ChbFilterDangerLeftPosition => StartComboPosition + ColumnActive + (ColumnDenger / 2) - _chbWidthHalf;

        public double ChbFilterProdLeftPosition => StartComboPosition + ColumnActive + ColumnDenger + (ColumnProd / 2) - _chbWidthHalf;

        public bool Modified
        {
            get
            {
                return _materialService.Modified;
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
                _notScroll = false;
                OnPropertyChanged(nameof(DgRowIndex));
            }
        }

        public long MaterialId
        {
            get
            {
                return _materialId;
            }
            set
            {
                _materialId = value;
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
                    if (UserSingleton.Id == Convert.ToInt64(_actualRow["login_id"]))
                        return true;
                    else if (UserSingleton.Id != Convert.ToInt64(_actualRow["login_id"]) && UserSingleton.Permission.Equals("admin"))
                        return true;
                    else
                        return false;
                }
            }
        }

        public DataView GetMaterialView
        {
            get
            {
                return _materialView;
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

        public ICommand SaveButton
        {
            get
            {
                if (_saveButton == null) _saveButton = new SaveButton(this);
                return _saveButton;
            }
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

        public void SetFiltration(bool filterOn, string filter)
        {
            if (filterOn)
            {
                _materialView.RowFilter = filter;
            }
            else
            {
                _materialView.RowFilter = "";
            }
            DgRowIndex = 0;
        }

        public void AddNewRecord()
        {

        }

        public void SaveAll()
        {
            _ = _materialService.Update();
        }

    }
}
