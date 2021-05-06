using LabBook.ADO.Common;
using LabBook.ADO.Repository;
using LabBook.Dto;
using LabBook.Security;
using System.Data;

namespace LabBook.ADO.Service
{
    public class ExperimentalVisService
    {
        private readonly User _user;
        private ExperimentalVisRepository _repository;
        private readonly DataTable _dataTable;
        private readonly DataView _brookView;
        private readonly DataView _brookxView;

        public ExperimentalVisService(User user)
        {
            _user = user;
            _repository = new ExperimentalVisRepository(_user);
            _dataTable = _repository.CreateTable();
            _brookView = new DataView(_dataTable) { RowFilter = "vis_type = 'brookfield'" };
            _brookxView = new DataView(_dataTable) { RowFilter = "vis_type = 'brookfield_x'" };
        }

        public void RefreshMainTable(long id)
        {
            _dataTable.Rows.Clear();
            _repository.RefreshMainTable(_dataTable, id);
        }

        public DataView GetBrookfield
        {
            get
            {
                return _brookView;
            }
        }

        public DataView GetBrookfieldx
        {
            get
            {
                return _brookxView;
            }
        }

    }
}
