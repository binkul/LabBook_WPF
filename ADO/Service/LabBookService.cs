using LabBook.ADO.Common;
using LabBook.ADO.Exceptions;
using LabBook.ADO.Repository;
using LabBook.Dto;
using LabBook.Security;
using System.Data;

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
            _dataTable = _repository.GetAll();
            _dataTable.RowChanged += DataTable_RowChanged;
            DataView view = new DataView(_dataTable);
            view.Sort = "id";
            return view;
        }

        private void DataTable_RowChanged(object sender, DataRowChangeEventArgs e)
        {
            _modified = true;
        }

        public bool Update()
        {
            var result = true;

            var updateRows = _dataTable.GetChanges(DataRowState.Modified);
            if (updateRows != null)
            {
                foreach (DataRow row in updateRows.Rows)
                {

                    if (_repository.Update(row) != ExceptionCode.NoError)
                        result = false;
                }
            }

            if (result) _modified = false;

            return result;
        }
    }
}
