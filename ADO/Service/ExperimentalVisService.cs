using LabBook.ADO.Repository;
using System.Data;
using LabBook.ADO.Exceptions;
using LabBook.Forms.MainForm.ModelView;

namespace LabBook.ADO.Service
{
    public class ExperimentalVisService
    {
        private ExperimentalVisRepository _repository = new ExperimentalVisRepository();
        private readonly DataTable _dataTable;
        private readonly DataView _dataView;
        private bool _modified = false;

        public ExperimentalVisService()
        {
            _dataTable = _repository.CreateTable();
            _dataTable.RowChanged += _dataTable_RowChanged;
            _dataView = new DataView(_dataTable) { RowFilter = "vis_type = '" + ViscosityType.Brookfield + "'", Sort = "date_created, date_update" };
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

        public void SetBrookfieldVisible()
        {
            _dataView.RowFilter = "vis_type = '" + ViscosityType.Brookfield + "'";
        }

        public void SetBrookfieldXVisible()
        {
            _dataView.RowFilter = "vis_type = '" + ViscosityType.BrookfieldX + "'";
        }

        public void SetKrebsVisible()
        {
            _dataView.RowFilter = "vis_type = '" + ViscosityType.Krebs + "'";
        }

        public void SetIciVisible()
        {
            _dataView.RowFilter = "vis_type = '" + ViscosityType.ICI + "'";
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

        public DataView GetView
        {
            get
            {
                return _dataView;
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
                    if (_repository.Save(row, ExperimentalVisRepository.SaveQuery) != ExceptionCode.NoError)
                        result = false;
                }
            }

            var updateRows = _dataTable.GetChanges(DataRowState.Modified);
            if (updateRows != null)
            {
                foreach (DataRow row in updateRows.Rows)
                {
                    if (_repository.Update(row, ExperimentalVisRepository.SaveQuery) != ExceptionCode.NoError)
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
            bool result = _repository.Delete(id, ExperimentalVisRepository.DelQuery);
            _modified = tmp;
            return result;
        }
    }
}
