using LabBook.ADO.Repository;
using System.Data;
using LabBook.ADO.Exceptions;

namespace LabBook.ADO.Service
{
    public class ExperimentalVisService
    {
        private ExperimentalVisRepository _repository = new ExperimentalVisRepository();
        private readonly DataTable _dataTable;
        private readonly DataView _brookView;
        private readonly DataView _brookxView;
        private readonly DataView _krebsView;
        private readonly DataView _iciView;
        private bool _modified = false;

        public ExperimentalVisService()
        {
            _dataTable = _repository.CreateTable();
            _dataTable.RowChanged += _dataTable_RowChanged;
            _brookView = new DataView(_dataTable) { RowFilter = "vis_type = 'brookfield'", Sort = "date_created, date_update" };
            _brookxView = new DataView(_dataTable) { RowFilter = "vis_type = 'brookfield_x'", Sort = "date_created, date_update" };
            _krebsView = new DataView(_dataTable) { RowFilter = "vis_type = 'krebs'", Sort = "date_created, date_update" };
            _iciView = new DataView(_dataTable) { RowFilter = "vis_type = 'ici'", Sort = "date_created, date_update" };
        }

        private void _dataTable_RowChanged(object sender, DataRowChangeEventArgs e)
        {
            _modified = true;
        }

        public DataTable GetTable
        {
            get
            {
                return _dataTable;
            }
        }

        public bool Modified
        {
            get
            {
                return _modified;
            }
        }

        public void RefreshMainTable(long id)
        {
            _ = Save();

            _dataTable.Rows.Clear();
            _repository.RefreshMainTable(_dataTable, id);
            _modified = false;
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

        public bool Save()
        {
            var result = true;

            var newRows = _dataTable.GetChanges(DataRowState.Added);
            if (newRows != null)
            {
                foreach (DataRow row in newRows.Rows)
                {
                    if (_repository.Save(row) != ExceptionCode.NoError)
                        result = false;
                }
            }

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

        public bool SaveAndReload(long id)
        {
            var result = true;

            if (Save())
            {
                _dataTable.Rows.Clear();
                _repository.RefreshMainTable(_dataTable, id);
                _modified = false;
            }
            else
                result = false;

            return result;
        }

        public bool Delete(long id)
        {
            bool tmp = _modified;
            bool result = _repository.Delete(id);
            _modified = tmp;
            return result;
        }
    }
}
