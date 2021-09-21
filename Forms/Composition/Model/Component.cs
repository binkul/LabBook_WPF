using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace LabBook.Forms.Composition.Model
{
    public class Component : INotifyPropertyChanged, IComparable<Component>
    {
        private int _level = 0;
        private int _ordering;
        private string _name;
        private double _amount;
        private double _mass;
        private double _priceKg;
        private double _price;
        private double _voc;
        private string _comment;
        private bool _isSemi;
        private long _semiNrD;
        private int _semiLevel = 0;
        private int _operation;
        private string _operationName;
        private double _density;
        public ObservableCollection<Component> SemiProduct { get; } = new ObservableCollection<Component>();

        public Component(int level, int ordering, string name, double amount, double mass, double priceKg, double price, double voc, 
            string comment, bool isSemi, long semiNrD, int semiLevel, int operation, string operationName, double density)
        {
            _level = level;
            _ordering = ordering;
            _name = name;
            _amount = amount;
            _mass = mass;
            _priceKg = priceKg;
            _price = price;
            _voc = voc;
            _comment = comment;
            _isSemi = isSemi;
            _semiNrD = semiNrD;
            _semiLevel = semiLevel;
            _operation = operation;
            _operationName = operationName;
            _density = density;
        }

        public Component(int ordering, string name, double amount, double mass, double priceKg, double price, double voc,
            string comment, bool isSemi, long semiNrD, int operation, string operationName, double density)
        {
            _ordering = ordering;
            _name = name;
            _amount = amount;
            _mass = mass;
            _priceKg = priceKg;
            _price = price;
            _voc = voc;
            _comment = comment;
            _isSemi = isSemi;
            _semiNrD = semiNrD;
            _operation = operation;
            _operationName = operationName;
            _density = density;
        }

        public int Level
        {
            get => _level;
            set
            {
                _level = value;
                OnPropertyChanged(nameof(Level));
            }
        }

        public int Ordering
        {
            get => _ordering;
            set
            {
                _ordering = value;
                OnPropertyChanged(nameof(Ordering));
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public double Amount
        {
            get => _amount;
            set
            {
                _amount = value;
                OnPropertyChanged(nameof(Amount));
            }
        }

        public double Mass
        {
            get => _mass;
            set
            {
                _mass = value;
                OnPropertyChanged(nameof(Mass));
            }
        }

        public double Price
        {
            get => _price;
            set
            {
                _price = value;
                OnPropertyChanged(nameof(Price));
            }
        }

        public double PriceKg
        {
            get => _priceKg;
            set
            {
                _priceKg = value;
                OnPropertyChanged(nameof(PriceKg));
            }
        }

        public double VOC
        {
            get => _voc;
            set
            {
                _voc = value;
                OnPropertyChanged(nameof(VOC));
            }
        }

        public string Comment
        {
            get => _comment;
            set
            {
                _comment = value;
                OnPropertyChanged(nameof(Comment));
            }
        }

        public bool IsSemiProduct
        {
            get => _isSemi;
            set
            {
                _isSemi = value;
                OnPropertyChanged(nameof(IsSemiProduct));
            }
        }

        public long IsSemiProductNrD
        {
            get => _semiNrD;
            set => _semiNrD = value;
        }

        public int SemiLevel
        {
            get => _semiLevel;
            set => _semiLevel = value;
        }

        public bool IsSemiproductPresent => SemiProduct.Count > 0;

        public int Operation
        {
            get => _operation;
            set
            {
                _operation = value;
                OnPropertyChanged(nameof(Operation));
            }
        }

        public string OperationName
        {
            get => _operationName;
            set
            {
                _operationName = value;
                OnPropertyChanged(nameof(OperationName));
            }
        }

        public double Density
        {
            get => _density;
            set => _density = value;
        }

        public void AddSemiProduct(Component component)
        {
            SemiProduct.Add(component);
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
        
        public int CompareTo(Component other)
        {
            return Ordering - other.Ordering;
        }
    }
}
