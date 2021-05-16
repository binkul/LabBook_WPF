using LabBook.ADO.Common;
using LabBook.ADO.Repository;
using LabBook.Dto;
using LabBook.Security;
using System.Data;

namespace LabBook.ADO.Service
{
    public class ExperimentCycleService
    {
        private readonly IRepository<ExperimentCycleDto> _expCycleRepository = new ExperimentCycleRepository();
        private DataTable _dataTable;

        public DataView GetAll()
        {
            _dataTable = _expCycleRepository.GetAll(ExperimentCycleRepository.AllQuery);
            DataView view = new DataView(_dataTable);
            view.Sort = "name";
            return view;
        }
    }
}
