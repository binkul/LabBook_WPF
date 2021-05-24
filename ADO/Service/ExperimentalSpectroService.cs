using LabBook.ADO.Exceptions;
using LabBook.ADO.Repository;
using System.Data;

namespace LabBook.ADO.Service
{
    public class ExperimentalSpectroService
    {
        private ExperimentalSpectroRepository _repository = new ExperimentalSpectroRepository();
        private readonly DataTable _spectroTable;
        private readonly DataView _spectroView;
        private bool _modified = false;

        public ExperimentalSpectroService()
        {
            _spectroTable = _repository.CreateTable();
            _spectroTable.RowChanged += _opacityTable_RowChanged;
            _spectroView = new DataView(_spectroTable) { Sort = "date_created, date_update" };
        }

        private void _opacityTable_RowChanged(object sender, DataRowChangeEventArgs e)
        {
            _modified = true;
        }

        public DataTable GetSpectroTable
        {
            get
            {
                return _spectroTable;
            }
        }

        public DataView GetSpectroView
        {
            get
            {
                return _spectroView;
            }
        }

        public bool Modified
        {
            get
            {
                return _modified;
            }
            private set
            {
                _modified = value;
            }
        }

        public void RefreshMainTable(long id)
        {
            _ = Save();

            _spectroTable.Rows.Clear();
            _repository.RefreshMainTable(_spectroTable, id);
            _modified = false;
        }

        public bool SaveAndReload(long id)
        {
            var result = true;

            if (Save())
            {
                _spectroTable.Rows.Clear();
                _repository.RefreshMainTable(_spectroTable, id);
                _modified = false;
            }
            else
                result = false;

            return result;
        }

        public bool Save()
        {
            var result = true;
            if (!_modified) return result;

            foreach (DataRow row in _spectroTable.Rows)
            {
                if (row.RowState == DataRowState.Added)
                {
                    if (_repository.Save(row, ExperimentalSpectroRepository.SaveQuery) == ExceptionCode.NoError)
                        row.AcceptChanges();
                    else
                        result = false;
                }
                else if (row.RowState == DataRowState.Modified)
                {
                    if (_repository.Update(row, ExperimentalSpectroRepository.UpdateQuery) == ExceptionCode.NoError)
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
            bool tmp = _modified;
            bool result = _repository.Delete(id, ExperimentalSpectroRepository.DelQuery);
            _modified = tmp;
            return result;
        }

    }
}
