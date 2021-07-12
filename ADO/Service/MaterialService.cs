using LabBook.ADO.Common;
using LabBook.ADO.Exceptions;
using LabBook.ADO.Repository;
using LabBook.Dto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace LabBook.ADO.Service
{
    public class MaterialService
    {
        private readonly IRepository<MaterialDto> _repository;
        private bool _modified = false;
        private DataTable _dataTable;
        private DataTable _dataTableClp;

        public MaterialService()
        {
            _repository = new MaterialRepository();
        }

        public bool Modified => _modified;

        public DataView GetAll()
        {
            _dataTable = _repository.GetAll(MaterialRepository.AllQuery);
            _dataTable.RowChanged += DataTable_RowChanged;
            DataView view = new DataView(_dataTable) { Sort = "id" };
            return view;
        }

        public string GetAllClp(long materialId)
        {
            DataTable table = _repository.GetAll(ClpRepository.MaterialClpQuery.Replace("XXXX", materialId.ToString()));
            StringBuilder clp = new StringBuilder();

            for (int i = 0; i < table.Rows.Count; i++)
            {
                DataRow row = table.Rows[i];
                clp.Append(row["clp_full"].ToString().Replace("-- Brak --: ", ""));
                clp.Append("\r\n");
            }
            return clp.ToString();
        }

        public DataView GetAllClpView(long materialId)
        {
            _dataTableClp = _repository.GetAll(ClpRepository.MaterialClpQuery.Replace("XXXX", materialId.ToString()));
            DataView view = new DataView(_dataTableClp) { Sort = "ordering" };
            return view;
        }

        public void RefreshClpView(long materialId)
        {
            if (_dataTableClp == null) return;

            string query = ClpRepository.MaterialClpQuery.Replace("XXXX", materialId.ToString());
            MaterialRepository repository = (MaterialRepository)_repository;
            _dataTableClp.Rows.Clear();
            repository.RefreshTable(query, _dataTableClp);
        }

        public IDictionary<int, bool> GetAllGhs(long materialId)
        {
            var result = new Dictionary<int, bool>() {
                {1, false },
                {2, false },
                {3, false },
                {4, false },
                {5, false },
                {6, false },
                {7, false },
                {8, false },
                {9, false }
            };

            DataTable table = _repository.GetAll(ClpRepository.MaterialGhsQuery.Replace("XXXX", materialId.ToString()));
            foreach (DataRow row in table.Rows)
            {
                int i = Convert.ToInt32(row["GHS"]);
                result[i] = true;
            }

            return result;
        }

        private void DataTable_RowChanged(object sender, DataRowChangeEventArgs e)
        {
            _modified = true;
        }

        public bool Update()
        {
            var result = true;
            if (!_modified) return result;

            foreach (DataRow row in _dataTable.Rows)
            {
                if (row.RowState == DataRowState.Modified)
                {
                    if (_repository.Update(row, MaterialRepository.UpdateQuery) == ExceptionCode.NoError)
                        row.AcceptChanges();
                    else
                        result = false;
                }
            }

            if (result) _modified = false;

            return result;
        }

    }
}
