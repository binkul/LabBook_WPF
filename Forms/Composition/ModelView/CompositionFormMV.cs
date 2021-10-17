using GalaSoft.MvvmLight.Command;
using LabBook.ADO.Service;
using LabBook.Commons;
using LabBook.Forms.Composition.Command;
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
        private ICommand _saveButton;
        private ICommand _deleteButton;
        private ICommand _printButton;
        private ICommand _loadRecipeButton;
        private ICommand _insertButton;
        private ICommand _addFirstButton;
        private ICommand _addMiddleButton;
        private ICommand _addLastButton;
        private ICommand _moveUpButton;
        private ICommand _moveDownButton;
        private ICommand _frameUpButton;
        private ICommand _frameCutButton;
        private ICommand _frameDownButton;

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
        private bool _modified = false;

        public SortableObservableCollection<Component> Recipe { get; } = new SortableObservableCollection<Component>();
        public RelayCommand<CancelEventArgs> OnClosingCommand { get; set; }
        public RelayCommand<MouseButtonEventArgs> OnMouseUpCommand { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        public CompositionFormMV() //CompositionEnterDto data)
        {
            _numberD = 12875; // data.NumberD;
            _title =  "Spray granit szary"; // data.Title;
            _density =  1.2M; // data.Density;

            _materialView = _service.GetAllMaterials();
            OnClosingCommand = new RelayCommand<CancelEventArgs>(OnClosingCommandExecuted);
            OnMouseUpCommand = new RelayCommand<MouseButtonEventArgs>(OnMouseUpCommandExecuted);

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
                _service.RecalculateByAmount(Recipe, _recipeData.Mass, 0, 0);
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

        public bool Modified
        {
            get => _modified;
            set => _modified = value;
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

        public ICommand PrintButton
        {
            get
            {
                if (_printButton == null) _printButton = new PrinterButton(this);
                return _printButton;
            }
        }

        public ICommand LoadRecipeButton
        {
            get
            {
                if (_loadRecipeButton == null) _loadRecipeButton = new ExchangeButton(this);
                return _loadRecipeButton;
            }
        }

        public ICommand InsertRecipeButton
        {
            get
            {
                if (_insertButton == null) _insertButton = new InsertButton(this);
                return _insertButton;
            }
        }

        public ICommand AddFirstButton
        {
            get
            {
                if (_addFirstButton == null) _addFirstButton = new AddFirstButton(this);
                return _addFirstButton;
            }
        }

        public ICommand AddMiddleButton
        {
            get
            {
                if (_addMiddleButton == null) _addMiddleButton = new AddMiddleButton(this);
                return _addMiddleButton;
            }
        }

        public ICommand AddLastButton
        {
            get
            {
                if (_addLastButton == null) _addLastButton = new AddLastButton(this);
                return _addLastButton;
            }
        }

        public ICommand MoveUpButton
        {
            get
            {
                if (_moveUpButton == null) _moveUpButton = new MoveUpButton(this);
                return _moveUpButton;
            }
        }

        public ICommand MoveDownButton
        {
            get
            {
                if (_moveDownButton == null) _moveDownButton = new MoveDownButton(this);
                return _moveDownButton;
            }
        }

        public ICommand FrameUpButton
        {
            get
            {
                if (_frameUpButton == null) _frameUpButton = new FrameUpButton(this);
                return _frameUpButton;
            }
        }

        public ICommand FrameCutButton
        {
            get
            {
                if (_frameCutButton == null) _frameCutButton = new FrameCutButton(this);
                return _frameCutButton;
            }
        }

        public ICommand FrameDownButton
        {
            get
            {
                if (_frameDownButton == null) _frameDownButton = new FrameDownButton(this);
                return _frameDownButton;
            }
        }

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
                if (_selectedIndex < 0) return;

                _selectedIndex = value;
                _componentPercent = Recipe[_selectedIndex].Amount;
                _componentMass = Recipe[_selectedIndex].Mass;
                _componentName = Recipe[_selectedIndex].Name;
                OnPropertyChanged(nameof(ComponentPercent));
                OnPropertyChanged(nameof(ComponentMass));
                OnPropertyChanged(nameof(ComponentName));
                OnPropertyChanged(nameof(IsSemiComponent));
            }
        }

        public bool IsSemiComponent
        {
            get
            {
                if (_selectedIndex < 0) return false;
                return Recipe[_selectedIndex].Level == 0;
            }
        }

        public double ComponentPercent
        {
            get => _componentPercent;
            set
            {
                _componentPercent = Convert.ToDouble(value);
                Recipe[_selectedIndex].Amount = _componentPercent;
                _service.RecalculateByAmount(Recipe, _recipeData.Mass, 0, 0);
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
                _recipeData.Mass = _service.SumOfMass(Recipe);
                _service.RecalculateByMass(Recipe, _recipeData.Mass, 0, 0);
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
                _service.HideSemiRecipe(Recipe, Recipe[_selectedIndex]);
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

        public string GetSemiStatus
        {
            get => Recipe[_selectedIndex].SemiStatus;
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

        public void OnMouseUpCommandExecuted(MouseButtonEventArgs e)
        {
            if (_selectedIndex < 0) return;
            if (Recipe.Count == 0) return;
            Component component = Recipe[_selectedIndex];
            if (!component.IsSemiProduct) return;

            Point position = e.GetPosition((UIElement)e.Source);
            if (position.X > 26 && position.X < 38)
            {
                _service.ExpandOrHideSemiRecipe(Recipe, component, _selectedIndex);
            }
        }

        public void SaveAll()
        {

        }

        public void Delete()
        {

        }

        public void Print()
        {

        }

        public void LoadRecipe()
        {

        }

        public void InserRecipe()
        {

        }

        public void AddFirst()
        {

        }

        public void AddMiddle()
        {

        }

        public void AddLast()
        {

        }

        public void MoveUp()
        {

        }

        public void MoveDown()
        {

        }

        public void FrameUp()
        {

        }

        public void FrameCut()
        {

        }

        public void FrameDown()
        {

        }
    }
}
