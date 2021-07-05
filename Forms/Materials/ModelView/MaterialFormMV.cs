using GalaSoft.MvvmLight.Command;
using LabBook.ADO.Service;
using LabBook.Forms.Materials.Command;
using LabBook.Forms.Materials.Model;
using LabBook.Security;
using System;
using System.ComponentModel;
using System.Data;
using System.Windows;
using System.Windows.Input;

namespace LabBook.Forms.Materials.ModelView
{
    public class MaterialFormMV : INotifyPropertyChanged
    {
        private ICommand _addNewButton;

        private readonly WindowData _windowData = WindowSetting.Read();
        private readonly MaterialService _materialService = new MaterialService();
        private DataRowView _actualRow;
        private readonly DataView _materialView;

        public RelayCommand<CancelEventArgs> OnClosingCommand { get; set; }

        public MaterialFormMV()
        {
            _materialView = _materialService.GetAll();
            OnClosingCommand = new RelayCommand<CancelEventArgs>(this.OnClosingCommandExecuted);
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
                OnPropertyChanged(nameof(ColumnName));
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
                OnPropertyChanged(nameof(ColumnFunction));
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
                OnPropertyChanged(nameof(ColumnPrice));
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
                OnPropertyChanged(nameof(ColumnCurrency));
            }
        }

        public bool Modified
        {
            get
            {
                return _materialService.Modified;
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

        public DataView GetFunctionView
        {
            get
            {
                return _materialService.GetAllFunction();
            }
        }
        
        public DataView GetCurrencyView
        {
            get
            {
                return _materialService.GetAllCurrency();
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

        public ICommand AddNewButton
        {
            get
            {
                if (_addNewButton == null) _addNewButton = new AddNewButton(this);
                return _addNewButton;
            }
        }

        public void AddNewRecord()
        {

        }

        public void SaveAll()
        {

        }

    }
}
