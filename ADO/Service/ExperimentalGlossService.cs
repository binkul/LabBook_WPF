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

        public DataView GetOpacityView
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
    }
}
