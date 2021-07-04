using GalaSoft.MvvmLight.Command;
using LabBook.Forms.Materials.Command;
using LabBook.Forms.Materials.Model;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace LabBook.Forms.Materials.ModelView
{
    public class MaterialFormMV : INotifyPropertyChanged
    {
        private ICommand _addNewButton;

        private readonly WindowData _windowData = WindowSetting.Read();
        public RelayCommand<CancelEventArgs> OnClosingCommand { get; set; }

        public MaterialFormMV()
        {
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

        public bool Modified
        {
            get
            {
                //if (_viscosityMV != null && _glossMV != null && _opacityMV != null && _spectroMV != null && _commonMV != null && _ashBurnMV != null)
                //    return _labBookService.Modified || _viscosityMV.Modified || _glossMV.Modified || _opacityMV.Modified ||
                //        _spectroMV.Modified || _commonMV.Modified || _ashBurnMV.Modified;
                //else
                    return false;
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
