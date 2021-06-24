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

        public ExpAshBurns Calculate(ExpAshBurns expAshBurns)
        {

            double crucible = (expAshBurns.Crucible1 + expAshBurns.Crucible2 + expAshBurns.Crucible3) / 3;
            double paint = (expAshBurns.Paint1 + expAshBurns.Paint2 + expAshBurns.Paint2) / 3;
            double crucible105 = (expAshBurns.Crucible105_1 + expAshBurns.Crucible105_2 + expAshBurns.Crucible105_3) / 3;
            double crucible450 = (expAshBurns.Crucible405_1 + expAshBurns.Crucible405_2 + expAshBurns.Crucible405_3) / 3;
            double crucible900 = (expAshBurns.Crucible900_1 + expAshBurns.Crucible900_2 + expAshBurns.Crucible900_3) / 3;


            return expAshBurns;
        }
    }
}
