using GalaSoft.MvvmLight.Command;
using LabBook.Forms.Composition.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LabBook.Forms.Composition.ModelView
{
    public class CompositionFormMV : INotifyPropertyChanged
    {

        private readonly WindowData _windowData = WindowSetting.Read();
        private long _numberD;
        private string _title;
        private decimal _density;
        public RelayCommand<CancelEventArgs> OnClosingCommand { get; set; }

        public CompositionFormMV(Int64 numberD, string title, decimal density)
        {
            _numberD = numberD;
            _title = title;
            _density = density;

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
            get => _windowData.FormXpos;
            set
            {
                _windowData.FormXpos = value;
                OnPropertyChanged(nameof(FormXpos));
            }
        }

        public double FormYpos
        {
            get => _windowData.FormYpos;
            set
            {
                _windowData.FormYpos = value;
                OnPropertyChanged(nameof(FormYpos));
            }
        }

        public double FormWidth
        {
            get => _windowData.FormWidth;
            set
            {
                _windowData.FormWidth = value;
                OnPropertyChanged(nameof(FormWidth));
            }
        }

        public double FormHeight
        {
            get => _windowData.FormHeight;
            set
            {
                _windowData.FormHeight = value;
                OnPropertyChanged(nameof(FormHeight));
            }
        }

        public string GetTitle => "D-" + _numberD.ToString() + "  " + _title;

        public decimal GetDensity => _density;



        public void OnClosingCommandExecuted(CancelEventArgs e)
        {
            var ansver = MessageBoxResult.No;

            //if (Modified)
            //{
            //    ansver = MessageBox.Show("Dokonano zmian w rekordach. Czy zapisać zmiany?", "Zamis zmian", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
            //}

            if (ansver == MessageBoxResult.Yes)
            {
                //SaveAll();
                WindowSetting.Save(_windowData);
            }
            else if (ansver == MessageBoxResult.Cancel)
            {
                e.Cancel = true;
            }
            
            WindowSetting.Save(_windowData);
        }

    }
}
