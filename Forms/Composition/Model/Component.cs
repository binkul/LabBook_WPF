﻿using LabBook.ADO.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace LabBook.Forms.Composition.Model
{
    public class Component : INotifyPropertyChanged, IComparable<Component>
    {
        private int _level = (int)RecipeLevelType.mainLevel;
        private int _ordering = 1;
        private string _name;
        private double _amount = 0;
        private double _mass = 0;
        private double _priceKg = -1;
        private double _price = -1;
        private double _rate = -1;
        private double _vocPercent = -1;
        private double _voc = -1;
        private string _comment;
        private bool _isSemi = false;
        private long _semiNrD = -1;
        private SubRecipeOrdering _subOrdering = SubRecipeOrdering.none;
        private string _subRecipeStatus = "";
        private int _operation = 1;
        private string _operationName;
        private double _density = -1;
        public IList<Component> SemiRecipe { get; set; } = new List<Component>();

        public Component()
        { }

        public Component(int level, int ordering, string name, double amount, double mass, double priceKg, double price, double rate,
            double vocPercent, double voc, string comment, bool isSemi, long semiNrD, SubRecipeOrdering subOrder, int operation, string operationName, double density)
        {
            _level = level;
            _ordering = ordering;
            _name = name;
            _amount = amount;
            _mass = mass;
            _priceKg = priceKg;
            _price = price;
            _rate = rate;
            _vocPercent = vocPercent;
            _voc = voc;
            _comment = comment;
            _isSemi = isSemi;
            _semiNrD = semiNrD;
            _subOrdering = subOrder;
            _operation = operation;
            _operationName = operationName;
            _density = density;
        }

        public Component(int ordering, string name, double amount, double mass, double priceKg, double price, double rate,
            double vocPercent, double voc, string comment, bool isSemi, long semiNrD, int operation, string operationName, double density)
        {
            _ordering = ordering;
            _name = name;
            _amount = amount;
            _mass = mass;
            _priceKg = priceKg;
            _price = price;
            _rate = rate;
            _vocPercent = vocPercent;
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

        public double Rate
        {
            get => _rate;
            set
            {
                _rate = value;
            }
        }

        public double VocPercent
        {
            get => _vocPercent;
            set
            {
                _vocPercent = value;
                OnPropertyChanged(nameof(VocPercent));
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

        public long SemiProductNrD
        {
            get => _semiNrD;
            set => _semiNrD = value;
        }

        public SubRecipeOrdering SubOrdering
        {
            get => _subOrdering;
            set => _subOrdering = value;
        }

        public string SemiStatus
        {
            get => _subRecipeStatus;
            set
            {
                _subRecipeStatus = value;
                OnPropertyChanged(nameof(SemiStatus));
            }
        }

        public bool IsSemiproductPresent => SemiRecipe.Count > 0;

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
