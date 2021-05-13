using LabBook.ADO.Common;
using LabBook.ADO.Repository;
using LabBook.Dto;
using LabBook.Security;
using System.Data;

namespace LabBook.ADO.Service
{
    public class ExperimentCycleService
    {
        private readonly IRepository<ExperimentCycleDto> _expCycleRepository;
        private DataTable _dataTable;

        public ExperimentCycleService() 
        {
            _expCycleRepository = new ExperimentCycleRepository();
        }

        public DataView GetAll()
        {
            _dataTable = _expCycleRepository.GetAll();
            DataView view = new DataView(_dataTable);
            view.Sort = "name";
            return view;
        }
    }
}
