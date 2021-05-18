using LabBook.ADO.Common;
using LabBook.ADO.Exceptions;
using LabBook.ADO.Repository;
using LabBook.Dto;
using LabBook.Forms.InputBox;
using System;
using System.Data;
using System.Linq;
using System.Windows;

namespace LabBook.ADO.Service
{
    public class LabBookService
    {
        private readonly IRepository<LabBookDto> _repository;
        private bool _modified = false;
        private DataTable _dataTable;

        public LabBookService()
        {
            _repository = new LabBookRepository();
        }

        public bool Modified
        {
            get
            {
                return _modified;
            }
        }

        public DataView GetAll()
        {
            _dataTable = _repository.GetAll(LabBookRepository.AllQuery);
            _dataTable.RowChanged += DataTable_RowChanged;
            DataView view = new DataView(_dataTable) { Sort = "id" };
            return view;
        }

        private void DataTable_RowChanged(object sender, DataRowChangeEventArgs e)
        {
            _modified = true;
        }

        public bool Update()
        {
            var result = true;

            foreach (DataRow row in _dataTable.Rows)
            {
                if (row.RowState == DataRowState.Modified)
                {
                    if (_repository.Update(row, LabBookRepository.UpdateQuery) == ExceptionCode.NoError)
                        row.AcceptChanges();
                    else
                        result = false;
                }
            }

            if (result) _modified = false;

            return result;
        }

        public bool Delete(long id)
        {
            bool result = true;

            try
            {
                DataRow row = _dataTable.AsEnumerable()
                    .SingleOrDefault(r => r.Field<long>("id") == id);

                row["deleted"] = true;
            }
            catch (Exception)
            {
                result = false;
            }

            return result;
        }

        public LabBookDto AddNew(LabBookDto labBook)
        {
            var result = _repository.Save(labBook, LabBookRepository.SaveQuery);

            if (result != null)
            {
                DataRow newRow = _dataTable.NewRow();
                newRow["id"] = result.Id;
                newRow["title"] = result.Title;
                newRow["density"] = result.Density;
                newRow["observation"] = result.Observation;
                newRow["remarks"] = result.Remarks;
                newRow["user_id"] = result.UserId;
                newRow["cycle_id"] = result.CycleId;
                newRow["created"] = result.Created;
                newRow["modified"] = result.Modified;
                newRow["deleted"] = result.Deleted;

                var tmp = _modified;
                _dataTable.Rows.Add(newRow);
                var row = _dataTable.AsEnumerable()
                    .SingleOrDefault(r => r.Field<long>("id") == result.Id);
                row.AcceptChanges();
                _modified = tmp;
            }

            return result;
        }

        public bool AddNewSeries(LabBookDto labBook)
        {
            var tmp = "";
            var count = 0;
            InputBox inputBox = new InputBox("Podaj ilość pustych rekordów:", "Ilość");
            if (inputBox.ShowDialog() == true)
                tmp = inputBox.Answer;

            if (!int.TryParse(tmp, out count))
            {
                MessageBox.Show("Wprowadzona wartość nie jest liczbą całkowitą.", "Zła wartość", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (count == 0) return false;

            for (var i = 0; i < count; i++)
            {
                _ = AddNew(labBook);
            }

            return true;
        }

        public void RefreshAll()
        {
            LabBookRepository repoTmp = (LabBookRepository)_repository;
            repoTmp.RefreshMainTable(_dataTable);
            _dataTable.AcceptChanges();
            _modified = false;
        }
    }
}
