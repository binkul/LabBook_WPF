using GalaSoft.MvvmLight.Command;
using LabBook.ADO.Common;
using LabBook.ADO.Repository;
using LabBook.Dto;
using LabBook.Forms.ClpData.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Windows.Controls;
using System.Windows.Input;

namespace LabBook.Forms.ClpData.ModelView
{
    public class ClpListMV
    {
        private int _clpDataSelectedCount = 0;
        private readonly IRepository<ClpDto> _repository = new ClpRepository();
        private readonly IList<ClpMV> _selectedRowsList = new List<ClpMV>();
        private ClpMV _selectedClpMV = null;

        private ICommand _addButton;
        private ICommand _removeAllButton;
        private ICommand _removeSelectedButton;

        public RelayCommand<SelectionChangedEventArgs> OnSelectionChangedCommand { get; set; }
        public RelayCommand<SelectionChangedEventArgs> OnSelectionClpChangedCommand { get; set; }
        public ObservableCollection<ClpMV> ClpDataList { get; } = new ObservableCollection<ClpMV>();
        public ObservableCollection<ClpMV> ClpSelectedList { get; } = new ObservableCollection<ClpMV>();

        public ClpListMV()
        {
            OnSelectionChangedCommand = new RelayCommand<SelectionChangedEventArgs>(OnSelectionChangedCommandExecuted);
            OnSelectionClpChangedCommand = new RelayCommand<SelectionChangedEventArgs>(OnSelectionClpChangedCommandExecuted);
            ClpSelectedList.Clear();
            ClpDataList.Clear();
            FillClpDataList();
        }
        
        private void FillClpDataList()
        {
            int maxLenght = 30;
            DataTable table = _repository.GetAll(ClpRepository.GetClpHPData);

            foreach (DataRow row in table.Rows)
            {
                int id = Convert.ToInt32(row["id"]);
                int ordering = Convert.ToInt32(row["ordering"]);
                string clpHP = row["clp"].ToString();

                string clpClass = "";
                if (clpHP.Substring(0, 1).ToUpper() == "H")
                {
                    clpClass = row["class"].ToString();
                }

                string clpDescription = row["description"].ToString();
                if (clpDescription.Length > maxLenght)
                    clpDescription = clpDescription.Substring(0, maxLenght) + " ...";

                ClpDataList.Add(new ClpMV(id, clpHP, clpClass, clpDescription, ordering));
            }
        }

        public int ClpDataSelectedCount
        {
            get => _clpDataSelectedCount;
            set => _clpDataSelectedCount = value;
        }

        public int ClpSelectedCount => ClpSelectedList.Count;

        public bool IsClpSelected => _selectedClpMV != null;

        public ICommand AddButton
        {
            get
            {
                if (_addButton == null) _addButton = new AddButton(this);
                return _addButton;
            }
        }

        public ICommand RemoveAllButton
        {
            get
            {
                if (_removeAllButton == null) _removeAllButton = new RemoveAllButton(this);
                return _removeAllButton;
            }
        }

        public ICommand RemoveSelectedButton
        {
            get
            {
                if (_removeSelectedButton == null) _removeSelectedButton = new RemoveSelectedButton(this);
                return _removeSelectedButton;
            }
        }

        public void OnSelectionChangedCommandExecuted(SelectionChangedEventArgs e)
        {
            DataGrid grid = (DataGrid)e.Source;
            ClpDataSelectedCount = grid.SelectedItems.Count;

            _selectedRowsList.Clear();
            for (var i = 0; i < grid.SelectedItems.Count; i++)
            {
                ClpMV row = (ClpMV)grid.SelectedItems[i];
                _selectedRowsList.Add(row);
            }
        }

        public void OnSelectionClpChangedCommandExecuted(SelectionChangedEventArgs e)
        {
            DataGrid grid = (DataGrid)e.Source;
            if (grid.SelectedItems.Count > 0)
            {
                _selectedClpMV = (ClpMV)grid.SelectedItem;
            }
            else
            {
                _selectedClpMV = null;
            }
        }

        public void AddClpToList()
        {
            List<ClpMV> sortList = new List<ClpMV>(ClpSelectedList);
            foreach (ClpMV row in _selectedRowsList)
            {
                if (!sortList.Contains(row))
                    sortList.Add(row);
            }
            sortList.Sort();
            ClpSelectedList.Clear();
            foreach (ClpMV row in sortList)
            {
                ClpSelectedList.Add(row);
            }
        }

        public void RemoveSelected()
        {
            if (_selectedClpMV != null)
                ClpSelectedList.Remove(_selectedClpMV);
        }

        public void RemoveAllSelected()
        {
            ClpSelectedList.Clear();
        }
    }
}
