using LabBook.ADO.Exceptions;
using LabBook.ADO.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabBook.ADO.Service
{
    public class ExperimentalGlossService
    {
        private ExperimentalGlossRepository _repository = new ExperimentalGlossRepository();
        private readonly DataTable _glossTable;
        private readonly DataTable _classTable;
        private readonly DataView _glossView;
        private readonly DataView _classView;
        private bool _modified = false;

        public ExperimentalGlossService()
        {
            _glossTable = _repository.CreateTable();
            _classTable = _repository.GetAll(ExperimentalGlossRepository.ClassQuery);
            _glossTable.RowChanged += OpacityTable_RowChanged;
            _glossView = new DataView(_glossTable) { Sort = "date_created, date_update" };
            _classView = new DataView(_classTable) { Sort = "name" };
        }

        private void OpacityTable_RowChanged(object sender, DataRowChangeEventArgs e)
        {
            _modified = true;
        }

        public DataTable GetGlossTable
        {
            get
            {
                return _glossTable;
            }
        }

        public DataView GetGlossView
        {
            get
            {
                return _glossView;
            }
        }

        public DataView GetClassView
        {
            get
            {
                return _classView;
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

            _glossTable.Rows.Clear();
            _repository.RefreshMainTable(_glossTable, id);
            _modified = false;
        }

        public bool SaveAndReload(long id)
        {
            var result = true;

            if (Save())
            {
                _glossTable.Rows.Clear();
                _repository.RefreshMainTable(_glossTable, id);
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

            foreach (DataRow row in _glossTable.Rows)
            {
                if (row.RowState == DataRowState.Added)
                {
                    if (_repository.Save(row, ExperimentalGlossRepository.SaveQuery) == ExceptionCode.NoError)
                        row.AcceptChanges();
                    else
                        result = false;
                }
                else if (row.RowState == DataRowState.Modified)
                {
                    if (_repository.Update(row, ExperimentalGlossRepository.UpdateQuery) == ExceptionCode.NoError)
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
            bool result = _repository.Delete(id, ExperimentalGlossRepository.DelQuery);
            _modified = tmp;
            return result;
        }

    }
}
