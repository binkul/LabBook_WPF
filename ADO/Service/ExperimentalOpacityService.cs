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
    public class ExperimentalOpacityService
    {
        private ExperimentalOpacityRepository _repository = new ExperimentalOpacityRepository();
        private readonly DataTable _opacityTable;
        private readonly DataView _opacityView;
        private bool _modified = false;

        public ExperimentalOpacityService()
        {
            _opacityTable = _repository.CreateTable();
            _opacityTable.RowChanged += _opacityTable_RowChanged;
            _opacityView = new DataView(_opacityTable) { Sort = "date_created, date_update" };
        }

        private void _opacityTable_RowChanged(object sender, DataRowChangeEventArgs e)
        {
            Modified = true;
        }

        public DataTable GetOpacityTable
        {
            get
            {
                return _opacityTable;
            }
        }

        public DataView GetOpacityView
        {
            get
            {
                return _opacityView;
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

            _opacityTable.Rows.Clear();
            _repository.RefreshMainTable(_opacityTable, id);
            _modified = false;
        }

        public bool SaveAndReload(long id)
        {
            var result = true;

            if (Save())
            {
                _opacityTable.Rows.Clear();
                _repository.RefreshMainTable(_opacityTable, id);
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

            foreach (DataRow row in _opacityTable.Rows)
            {
                if (row.RowState == DataRowState.Added)
                {
                    if (_repository.Save(row, ExperimentalOpacityRepository.SaveQuery) == ExceptionCode.NoError)
                        row.AcceptChanges();
                    else
                        result = false;
                }
                else if (row.RowState == DataRowState.Modified)
                {
                    if (_repository.Update(row, ExperimentalOpacityRepository.UpdateQuery) == ExceptionCode.NoError)
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
            bool result = _repository.Delete(id, ExperimentalOpacityRepository.DelQuery);
            _modified = tmp;
            return result;
        }

    }
}
