using LabBook.ADO.Common;
using LabBook.ADO.Exceptions;
using LabBook.ADO.Repository;
using LabBook.Dto;
using System.Data;

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
