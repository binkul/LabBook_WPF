using LabBook.ADO.Common;
using LabBook.ADO.Repository;
using LabBook.Dto;
using System.Data;

namespace LabBook.ADO.Service
{
    public class CompositionService
    {
        private readonly IRepository<CompositionDto> _repository = new CompositionRepository();

        public DataTable GetRecipe(long numberD)
        {
            return _repository.GetAll(CompositionRepository.AllRecipeQuery + numberD.ToString());
        }
    }
}
