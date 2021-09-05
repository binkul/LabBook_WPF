using GalaSoft.MvvmLight.Command;
using LabBook.ADO.Service;
using LabBook.Forms.Composition.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Component = LabBook.Forms.Composition.Model.Component;

namespace LabBook.Forms.Composition.ModelView
{
    public class CompositionFormMV : INotifyPropertyChanged
    {
        private readonly double _startLeftPosition = 34d;

        private readonly WindowData _windowData = WindowSetting.Read();
        private readonly CompositionService _service = new CompositionService();
        private readonly long _numberD;
        private readonly string _title;
        private readonly decimal _density;
        private bool _amountMode = true;
        private bool _massMode = false;
        private readonly DataView _materialList;

        public ObservableCollection<Component> Recipe { get; } = new ObservableCollection<Component>();
        public RelayCommand<CancelEventArgs> OnClosingCommand { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        public CompositionFormMV() //Int64 numberD, string title, decimal density)
        {
            _numberD = 12875; // numberD;
            _title = "Spray granit szary"; // title;
            _density = 1.2M; // density;

            OnClosingCommand = new RelayCommand<CancelEventArgs>(OnClosingCommandExecuted);

            FillRecipe();
        }

        private void FillRecipe()
        {
            DataTable table = _service.GetRecipe(_numberD);
            double recMass = _service.GetRecipeMass(_numberD);
            double price = -1d;

            foreach(DataRow row in table.Rows)
            {
                int ordering = Convert.ToInt32(row["ordering"]);
                string name = row["component"].ToString();
                bool isSemi = Convert.ToBoolean(row["is_intermediate"]);
                double amount = Convert.ToDouble(row["amount"]);
                double amountKg = (amount * recMass) / 100;
                int operation = Convert.ToInt32(row["operation"]);
                string operationName = row["name"].ToString();
                string comment = row["comment"].ToString();
                double priceKg = !row["price"].Equals(DBNull.Value) ? Convert.ToDouble(row["price"]) : -1d;
                double rate = Convert.ToDouble(row["rate"]);
                long semiNrD = !row["intermediate_nrD"].Equals(DBNull.Value) ? Convert.ToInt64(row["intermediate_nrD"]) : -2;
                decimal voc = !row["VOC"].Equals(DBNull.Value) ? Convert.ToDecimal(row["VOC"]) : -1M;
                double density = !row["density"].Equals(DBNull.Value) ? Convert.ToDouble(row["density"]) : 0d;

                if (priceKg > 0 && rate > 0)
                {
                    priceKg *= rate;
                    price = priceKg * amountKg;
                }

                Component component = new Component(ordering, name, amount, amountKg, priceKg, price, voc, comment, isSemi, 
                    semiNrD, operation, operationName, density);
                Recipe.Add(component);
            }
        }

        public DataView GetMatrials => _materialList;

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

        public double ColumnLP
        {
            get => _windowData.ColumnLP;
            set
            {
                _windowData.ColumnLP = value;
                OnPropertyChanged(
                    nameof(CmbMatrialLeftPosition),
                    nameof(TxtAmountLeftPosition),
                    nameof(TxtMassLeftPosition),
                    nameof(RdAmountLeftPosition),
                    nameof(RdAmountKgLeftPosition),
                    nameof(TxtCommentLeftPosition));
            }
        }

        public double ColumnComponent
        {
            get => _windowData.ColumnComponent;
            set
            {
                _windowData.ColumnComponent = value;
                OnPropertyChanged(
                    nameof(ColumnComponent),
                    nameof(CmbMatrialLeftPosition),
                    nameof(TxtAmountLeftPosition),
                    nameof(TxtMassLeftPosition),
                    nameof(RdAmountLeftPosition),
                    nameof(RdAmountKgLeftPosition),
                    nameof(TxtCommentLeftPosition));
            }
        }

        public double ColumnAmount
        {
            get => _windowData.ColumnAmount;
            set
            {
                _windowData.ColumnAmount = value;
                OnPropertyChanged(
                   nameof(TxtAmountLeftPosition),
                   nameof(TxtMassLeftPosition),
                   nameof(RdAmountLeftPosition),
                   nameof(RdAmountKgLeftPosition),
                   nameof(TxtCommentLeftPosition));
            }
        }

        public double ColumnMass
        {
            get => _windowData.ColumnMass;
            set
            {
                _windowData.ColumnMass = value;
                OnPropertyChanged(
                  nameof(TxtMassLeftPosition),
                  nameof(RdAmountLeftPosition),
                  nameof(RdAmountKgLeftPosition),
                  nameof(TxtCommentLeftPosition));
            }
        }

        public double ColumnPriceKg
        {
            get => _windowData.ColumnPriceKg;
            set
            {
                _windowData.ColumnPriceKg = value;
                OnPropertyChanged(
                 nameof(RdAmountLeftPosition),
                 nameof(RdAmountKgLeftPosition),
                 nameof(TxtCommentLeftPosition));
            }
        }

        public double ColumnPrice
        {
            get => _windowData.ColumnPrice;
            set
            {
                _windowData.ColumnPrice = value;
                OnPropertyChanged(
                   nameof(RdAmountKgLeftPosition),
                   nameof(TxtCommentLeftPosition));
            }
        }

        public double ColumnVoc
        {
            get => _windowData.ColumnVOC;
            set
            {
                _windowData.ColumnVOC = value;
                OnPropertyChanged(nameof(TxtCommentLeftPosition));
            }
        }

        public double ColumnComment
        {
            get => _windowData.ColumnComment;
            set
            {
                _windowData.ColumnComment = value;
                OnPropertyChanged(nameof(TxtCommentLeftPosition));
            }
        }

        public bool AmountMode
        {
            get => _amountMode;
            set
            {
                _amountMode = value;
                OnPropertyChanged(nameof(AmountMode));
            }
        }

        public bool MassMode
        {
            get => _massMode;
            set
            {
                _massMode = value;
                OnPropertyChanged(nameof(MassMode));
            }
        }

        public double CmbMatrialLeftPosition => _startLeftPosition + ColumnLP;

        public double TxtAmountLeftPosition => CmbMatrialLeftPosition + ColumnComponent + 1;

        public double TxtMassLeftPosition => TxtAmountLeftPosition + ColumnAmount;

        public double RdAmountLeftPosition => TxtAmountLeftPosition + ColumnAmount + ColumnMass + (ColumnPrice / 2);

        public double RdAmountKgLeftPosition => RdAmountLeftPosition + (ColumnPrice / 2) + (ColumnPriceKg / 2);

        public double TxtCommentLeftPosition => RdAmountKgLeftPosition + (ColumnPriceKg / 2) + ColumnVoc;

        public string GetTitle => "D-" + _numberD.ToString() + "  " + _title;

        public decimal GetDensity => _density;

        protected void OnPropertyChanged(params string[] names)
        {
            if (PropertyChanged != null)
            {
                foreach (string name in names)
                    PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

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
