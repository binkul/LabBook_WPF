using GalaSoft.MvvmLight.Command;
using LabBook.ADO.Service;
using LabBook.Commons;
using LabBook.Dto;
using LabBook.Forms.Composition.Model;
using System;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Windows;
using Component = LabBook.Forms.Composition.Model.Component;

namespace LabBook.Forms.Composition.ModelView
{
    public class CompositionFormMV : INotifyPropertyChanged
    {
        private readonly WindowData _windowData = WindowSetting.Read();
        private readonly CompositionService _service = new CompositionService();
        private readonly long _numberD;
        private readonly CompositionData _recipeData;
        private readonly string _title;
        private readonly decimal _density;
        private double _componentPercent;
        private double _componentMass;
        private string _componentName;
        private bool _amountMode = true;
        private bool _massMode = false;
        private readonly DataView _materialView;
        private int _selectedIndex;

        public SortableObservableCollection<Component> Recipe { get; } = new SortableObservableCollection<Component>();
        public RelayCommand<CancelEventArgs> OnClosingCommand { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        public CompositionFormMV() //CompositionEnterDto data)
        {
            _numberD = 12875; // data.NumberD;
            _title =  "Spray granit szary"; // data.Title;
            _density =  1.2M; // data.Density;

            _materialView = _service.GetAllMaterials();
            OnClosingCommand = new RelayCommand<CancelEventArgs>(OnClosingCommandExecuted);

            _recipeData = _service.GetRecipeData(_numberD, _title, _density);
            FillRecipe();
        }

        private void FillRecipe()
        {
            DataTable table = _service.GetRecipe(_numberD);

            foreach (DataRow row in table.Rows)
            {
                int ordering = Convert.ToInt32(row["ordering"]);
                string name = row["component"].ToString();
                bool isSemi = Convert.ToBoolean(row["is_intermediate"]);
                double amount = Convert.ToDouble(row["amount"]);
                double amountKg = amount * _recipeData.Mass / 100;
                int operation = Convert.ToInt32(row["operation"]);
                string operationName = row["name"].ToString();
                string comment = row["comment"].ToString();
                double priceKg = !row["price"].Equals(DBNull.Value) ? Convert.ToDouble(row["price"]) : -1d;
                double rate = Convert.ToDouble(row["rate"]);
                long semiNrD = !row["intermediate_nrD"].Equals(DBNull.Value) ? Convert.ToInt64(row["intermediate_nrD"]) : -2;
                double voc = !row["VOC"].Equals(DBNull.Value) ? Convert.ToDouble(row["VOC"]) : -1d;
                double density = !row["density"].Equals(DBNull.Value) ? Convert.ToDouble(row["density"]) : 0d;

                double price;
                if (priceKg > 0 && rate > 0)
                {
                    priceKg *= rate;
                    price = priceKg * amountKg;
                }
                else
                {
                    price = -1d;
                }

                Component component = new Component(ordering, name, amount, amountKg, priceKg, price, voc, comment, isSemi,
                    semiNrD, operation, operationName, density);
                Recipe.Add(component);
            }
            if (Recipe.Count > 0) SelectedIndex = 0;
        }

        public DataView GetMatrials => _materialView;

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

        public string GetTitle => "D-" + _numberD.ToString() + "  " + _title;

        public string GetDensity => "Gęstość: " + _density.ToString("F4", CultureInfo.CurrentCulture) + " g/cm3";

        public double GetSumPercent => _service.SumOfPercent(Recipe);

        public double GetSumPrice => _service.SumOfPrices(Recipe);

        public double GetSumMass => _service.SumOfMass(Recipe);

        public double GetSumVoc => _service.SumOfVoc(Recipe);

        public double GetSumVocPerLiter
        {
            get
            {
                if (GetSumVoc >= 0 && _recipeData.Density > 0)
                    return GetSumVoc * Convert.ToDouble(_recipeData.Density) * 10;
                else
                    return -1;
            }
        }

        public int SelectedIndex
        {
            get => _selectedIndex;
            set
            {
                _selectedIndex = value;
                _componentPercent = Recipe[_selectedIndex].Amount;
                _componentMass = Recipe[_selectedIndex].Mass;
                _componentName = Recipe[_selectedIndex].Name;
                OnPropertyChanged(nameof(ComponentPercent));
                OnPropertyChanged(nameof(ComponentMass));
                OnPropertyChanged(nameof(ComponentName));
            }
        }

        public double ComponentPercent
        {
            get => _componentPercent;
            set
            {
                _componentPercent = Convert.ToDouble(value);
                Recipe[_selectedIndex].Amount = _componentPercent;
                OnPropertyChanged(nameof(GetSumPercent));
            }
        }

        public double ComponentMass
        {
            get => _componentMass;
            set
            {
                _componentMass = Convert.ToDouble(value);
                Recipe[_selectedIndex].Mass = _componentMass;
                OnPropertyChanged(nameof(GetSumMass));
            }
        }

        public string ComponentName
        {
            get => _componentName;
            set => _componentName = value;
        }

        private void SortRecipe()
        {
            Recipe.Sort(x => x.Ordering, ListSortDirection.Ascending);
        }

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
