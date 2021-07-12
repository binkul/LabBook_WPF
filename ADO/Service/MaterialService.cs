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

        public MaterialService()
        {
            _repository = new MaterialRepository();
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
            _dataTable = _repository.GetAll(MaterialRepository.AllQuery);
            _dataTable.RowChanged += DataTable_RowChanged;
            DataView view = new DataView(_dataTable) { Sort = "id" };
            return view;
        }

        public string GetAllClp()
        {
            DataTable table = _repository.GetAll(ClpRepository.MaterialClpQuery);
            StringBuilder clp = new StringBuilder();

            for (int i = 0; i < table.Rows.Count; i++)
            {
                DataRow row = table.Rows[i];
                clp.Append(row["clp"].ToString());
                clp.Append("\n");
            }
            return clp.ToString();
        }

        public IList<int> GetAllGhs()
        {
            DataTable table = _repository.GetAll(ClpRepository.MaterialGhsQuery);

            var result = table.AsEnumerable()
                .Select(row => Convert.ToInt32(row["GHS"]))
                .ToList();

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
