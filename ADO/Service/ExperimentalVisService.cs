using System;
using LabBook.ADO.Common;
using LabBook.ADO.Repository;
using LabBook.Dto;
using LabBook.Security;
using System.Data;
using LabBook.ADO.Exceptions;

namespace LabBook.ADO.Service
{
    public class ExperimentalVisService
    {
        private readonly User _user;
        private ExperimentalVisRepository _repository;
        private readonly DataTable _dataTable;
        private readonly DataView _brookView;
        private readonly DataView _brookxView;
        private readonly DataView _krebsView;
        private readonly DataView _iciView;

        public ExperimentalVisService(User user)
        {
            _user = user;
            _repository = new ExperimentalVisRepository(_user);
            _dataTable = _repository.CreateTable();
            _brookView = new DataView(_dataTable) { RowFilter = "vis_type = 'brookfield'", Sort = "date_created, date_update" };
            _brookxView = new DataView(_dataTable) { RowFilter = "vis_type = 'brookfield_x'", Sort = "date_created, date_update" };
            _krebsView = new DataView(_dataTable) { RowFilter = "vis_type = 'krebs'", Sort = "date_created, date_update" };
            _iciView = new DataView(_dataTable) { RowFilter = "vis_type = 'ici'", Sort = "date_created, date_update" };
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

        public DataView GetKrebs
        {
            get
            {
                return _krebsView;
            }
        }

        public DataView GetICI
        {
            get
            {
                return _iciView;
            }
        }

        public void Save()
        {
            bool error = false;

            var newRows = _dataTable.GetChanges(DataRowState.Added);
            if (newRows != null)
            {
                foreach (DataRow row in newRows.Rows)
                {
                    if (_repository.Save(row) != ExceptionCode.NoError)
                        error = true;
                }
            }

            var updateRows = _dataTable.GetChanges(DataRowState.Modified);
            if (updateRows != null)
            {
                foreach (DataRow row in updateRows.Rows)
                {
                    if (_repository.Update(row) != ExceptionCode.NoError)
                        error = true;
                }
            }
        }
    }
}
