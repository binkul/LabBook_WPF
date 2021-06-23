using LabBook.ADO.Common;
using LabBook.ADO.Repository;
using LabBook.Forms.MainForm.Model;
using System.Data;

namespace LabBook.ADO.Service
{
    public class ExperimentalAshBurnService
    {
        private readonly IRepository<ExpAshBurns> _repository = new ExperimentalAshBurnRepository();

        public ExpAshBurns GetCurrent(long labBookId)
        {
            return _repository.GetById(labBookId, ExperimentalAshBurnRepository.GetByLabbokIdQuery);
        }

        public ExpAshBurns Save(ExpAshBurns expAshBurns)
        {
            if (!_repository.ExistById(expAshBurns.LabBookId, ExperimentalAshBurnRepository.ExistByLabBookIdQuery))
            {
                return _repository.Save(expAshBurns);
            }
            else
            {
                _repository.Update(expAshBurns);
                return expAshBurns;
            }
        }

        public DataView GetVOCClass()
        {
            ExperimentalAshBurnRepository repository = (ExperimentalAshBurnRepository)_repository;
            DataTable table = repository.GetVOCClass();
            return new DataView(table) { Sort = "id" };
        }
    }
}
