using GalaSoft.MvvmLight.Command;
using LabBook.ADO.Service;
using LabBook.Forms.Navigation;
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
        private ICommand _addNewButton;
        private ICommand _saveButton;
        private ICommand _deleteButton;
        private ICommand _clpButton;

        private bool _ghs01 = true;
        private bool _ghs02 = true;
        private bool _ghs03 = true;
        private bool _ghs04 = true;
        private bool _ghs05 = true;
        private bool _ghs06 = true;
        private bool _ghs07 = true;
        private bool _ghs08 = true;
        private bool _ghs09 = true;

        public NavigationMV NavigationMV { get; set; }
        private readonly WindowData _windowData = WindowSetting.Read();
        private readonly MaterialService _materialService = new MaterialService();
        private long _index = 0;
        private long _materialId = 0;
        private bool _notScroll = true;
        private DataRowView _actualRow;
        private readonly DataView _semiProductView;


        public RelayCommand<SelectionChangedEventArgs> OnSelectionChangedCommand { get; set; }

        public RelayCommand<CancelEventArgs> OnClosingCommand { get; set; }

        public SemiProductFormMV()
        {
            _semiProductView = _materialService.GetAll();

            OnClosingCommand = new RelayCommand<CancelEventArgs>(OnClosingCommandExecuted);
            OnSelectionChangedCommand = new RelayCommand<SelectionChangedEventArgs>(OnSelectionChangedCommandExecuted);
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
                    nameof(ColumnName)
                    //nameof(CmbFilterFunctionLeftPosition),
                    //nameof(ChbFilterActiveLeftPosition),
                    //nameof(ChbFilterDangerLeftPosition),
                    //nameof(ChbFilterProdLeftPosition)
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
                    nameof(ColumnFunction)
                    //nameof(CmbFilterFunctionLeftPosition),
                    //nameof(ChbFilterActiveLeftPosition),
                    //nameof(ChbFilterDangerLeftPosition),
                    //nameof(ChbFilterProdLeftPosition)
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
                    nameof(ColumnPrice)
                    //nameof(ChbFilterActiveLeftPosition),
                    //nameof(ChbFilterDangerLeftPosition),
                    //nameof(ChbFilterProdLeftPosition)
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
                    nameof(ColumnUnit)
                    //nameof(ChbFilterActiveLeftPosition),
                    //nameof(ChbFilterDangerLeftPosition),
                    //nameof(ChbFilterProdLeftPosition)
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
                    nameof(ColumnDenger)
                    //nameof(ChbFilterDangerLeftPosition),
                    //nameof(ChbFilterProdLeftPosition)
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
                    nameof(ColumnProd)
                    //nameof(ChbFilterProdLeftPosition)
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
                    nameof(ColumnActive)
                    //nameof(ChbFilterActiveLeftPosition),
                    //nameof(ChbFilterDangerLeftPosition),
                    //nameof(ChbFilterProdLeftPosition)
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


        public bool Modified
        {
            get
            {
                return _materialService.Modified;
            }
        }

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

        public DataView GetSemiProductView
        {
            get
            {
                return _semiProductView;
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

        public bool IsDanger => ActualRow != null && Convert.ToBoolean(ActualRow["is_danger"]);

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

        public bool CanDelete => IsPermited && GetRowCount > 0;

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

        public void SaveAll()
        {
            _ = _materialService.Update();
        }

    }
}
