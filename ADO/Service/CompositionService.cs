using LabBook.ADO.Common;
using LabBook.ADO.Repository;
using LabBook.Dto;
using LabBook.Forms.Composition.Model;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace LabBook.ADO.Service
{
    public class CompositionService
    {
        private readonly IRepository<CompositionDto> _repository = new CompositionRepository();
        private DataTable _dataTableMaterial;

        public DataTable GetRecipe(long numberD)
        {
            return _repository.GetAll(CompositionRepository.AllRecipeQuery + numberD.ToString());
        }

        public CompositionData GetRecipeData(long numberD, string title, decimal density)
        {
            CompositionRepository repository = (CompositionRepository)_repository;
            return repository.GetRecipeData(numberD, title, density);
        }

        public DataView GetAllMaterials()
        {
            _dataTableMaterial = _repository.GetAll(CompositionRepository.MaterialListQuery);
            DataView view = new DataView(_dataTableMaterial) { Sort = "name" };
            return view;
        }

        public double SumOfPercent(IList<Component> recipe)
        {
            return recipe
                .Where(x => x.Level == 0)
                .Select(x => x.Amount)
                .Sum();
        }

        public double SumOfMass(IList<Component> recipe)
        {
            return recipe
                .Where(x => x.Level == 0)
                .Select(x => x.Mass)
                .Sum();
        }
    }
}
