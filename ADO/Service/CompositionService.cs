using LabBook.ADO.Common;
using LabBook.ADO.Repository;
using LabBook.Dto;
using System;
using System.Data;

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

        public double GetRecipeMass(long numberD)
        {
            DataTable table = _repository.GetAll(CompositionRepository.RecipeDataQuery + numberD.ToString());

            return !table.Rows[0]["mass"].Equals(DBNull.Value) ? Convert.ToDouble(table.Rows[0]["mass"]) : 1000d;
        }

        public DataView GetAllMaterials()
        {
            _dataTableMaterial = _repository.GetAll(CompositionRepository.MaterialListQuery);
            DataView view = new DataView(_dataTableMaterial) { Sort = "name" };
            return view;
        }

    }
}
