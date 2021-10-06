using GalaSoft.MvvmLight.Command;
using LabBook.ADO.Service;
using LabBook.Commons;
using LabBook.Forms.Composition.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
        private bool _mouseDown = false;

        public SortableObservableCollection<Component> Recipe { get; } = new SortableObservableCollection<Component>();
        public RelayCommand<CancelEventArgs> OnClosingCommand { get; set; }
        public RelayCommand<SelectedCellsChangedEventArgs> OnSelectedCellsCommnd { get; set; }
        public RelayCommand<MouseButtonEventArgs> OnMouseDownCommand { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        public CompositionFormMV() //CompositionEnterDto data)
        {
            _numberD = 12875; // data.NumberD;
            _title =  "Spray granit szary"; // data.Title;
            _density =  1.2M; // data.Density;

            _materialView = _service.GetAllMaterials();
            OnClosingCommand = new RelayCommand<CancelEventArgs>(OnClosingCommandExecuted);
            OnSelectedCellsCommnd = new RelayCommand<SelectedCellsChangedEventArgs>(OnSelectedCellsCommandExecuted);
            OnMouseDownCommand = new RelayCommand<MouseButtonEventArgs>(OnMouseDownCommandExecuted);

            _recipeData = _service.GetRecipeData(_numberD, _title, _density);
            _service.GetRecipe(Recipe, _recipeData);
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

        public double GetTotalMass
        {
            get => _recipeData.Mass;
            set
            {
                _recipeData.Mass = value;
                _service.RecalculateByAmount(Recipe, _recipeData);
                OnPropertyChanged(nameof(GetSumPercent));
                OnPropertyChanged(nameof(GetPricePerKg));
                OnPropertyChanged(nameof(GetPricePerL));
                OnPropertyChanged(nameof(GetSumMass));
                OnPropertyChanged(nameof(GetSumVoc));
                OnPropertyChanged(nameof(GetSumVocPerLiter));
            }
        }

        public string GetTitle => "D-" + _numberD.ToString() + "  " + _title;

        public string GetDensity
        {
            get
            {
                if (_density > 0)
                    return "Gęstość: " + _density.ToString("F4", CultureInfo.CurrentCulture) + " g/cm3";
                else
                    return "Gęstość: -- Brak --";
            }
        }

        public double GetSumPercent => _service.SumOfPercent(Recipe);

        public double GetPricePerKg => _service.PricePerKg(Recipe, _recipeData);

        public double GetPricePerL => _service.PricePerL(Recipe, _recipeData);

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
                _service.RecalculateByAmount(Recipe, _recipeData);
                OnPropertyChanged(nameof(GetSumPercent));
                OnPropertyChanged(nameof(GetPricePerKg));
                OnPropertyChanged(nameof(GetPricePerL));
                OnPropertyChanged(nameof(GetSumMass));
                OnPropertyChanged(nameof(GetSumVoc));
                OnPropertyChanged(nameof(GetSumVocPerLiter));
            }
        }

        public double ComponentMass
        {
            get => _componentMass;
            set
            {
                _componentMass = Convert.ToDouble(value);
                Recipe[_selectedIndex].Mass = _componentMass;
                _service.RecalculateByMass(Recipe, _recipeData);
                OnPropertyChanged(nameof(GetSumPercent));
                OnPropertyChanged(nameof(GetPricePerKg));
                OnPropertyChanged(nameof(GetPricePerL));
                OnPropertyChanged(nameof(GetSumMass));
                OnPropertyChanged(nameof(GetSumVoc));
                OnPropertyChanged(nameof(GetSumVocPerLiter));
                OnPropertyChanged(nameof(GetTotalMass));
            }
        }

        public string ComponentName
        {
            get => _componentName;
            set
            {
                _componentName = value;
                Recipe[_selectedIndex].Name = _componentName;
                _service.UpdateComponent(Recipe[_selectedIndex], _recipeData);
                OnPropertyChanged(nameof(GetSumPercent));
                OnPropertyChanged(nameof(GetPricePerKg));
                OnPropertyChanged(nameof(GetPricePerL));
                OnPropertyChanged(nameof(GetSumMass));
                OnPropertyChanged(nameof(GetSumVoc));
                OnPropertyChanged(nameof(GetSumVocPerLiter));
                OnPropertyChanged(nameof(GetTotalMass));
            }
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

        public void OnSelectedCellsCommandExecuted(SelectedCellsChangedEventArgs e)
        {
            if (!_mouseDown) return;
            if (Recipe.Count == 0) return;

            IList<DataGridCellInfo> info = e.AddedCells;
            Component component = info[0].Item as Component;

            if (!component.IsSemiProduct) return;

            int index = Recipe.IndexOf(component);
            index++;

            if (component.SemiStatus == "close")
            {
                foreach (Component subComponent in component.SemiRecipe)
                {
                    if (index < Recipe.Count)
                        Recipe.Insert(index, subComponent);
                    else
                        Recipe.Add(subComponent);
                }
                component.SemiStatus = "open";
                component.Comment = "Zmiana";
            }
            else
            {

            }

            _mouseDown = false;
        }

        public void OnMouseDownCommandExecuted(MouseButtonEventArgs e)
        {
            Point position = e.GetPosition((UIElement)e.Source);
            if (position.X > 26 && position.X < 38)
            {
                _mouseDown = true;
            }
        }
    }
}
