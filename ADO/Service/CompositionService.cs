using LabBook.ADO.Common;
using LabBook.ADO.Repository;
using LabBook.Dto;
using LabBook.Forms.Composition.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace LabBook.ADO.Service
{
    public class CompositionService
    {
        private readonly IRepository<CompositionDto> _repository = new CompositionRepository();
        private DataTable _dataTableMaterial;

        public void GetRecipe(IList<Component> recipe, CompositionData data)
        {
            DataTable table = _repository.GetAll(CompositionRepository.AllRecipeQuery + data.LabBookId.ToString());

            foreach (DataRow row in table.Rows)
            {
                Component component = new Component
                {
                    Ordering = Convert.ToInt32(row["ordering"]),
                    Name = row["component"].ToString(),
                    IsSemiProduct = Convert.ToBoolean(row["is_intermediate"]),
                    Amount = Convert.ToDouble(row["amount"]),
                    Operation = Convert.ToInt32(row["operation"]),
                    OperationName = row["name"].ToString(),
                    Comment = row["comment"].ToString()
                };

                component.Mass = component.Amount * data.Mass / 100;
                component.PriceKg = !row["price"].Equals(DBNull.Value) ? Convert.ToDouble(row["price"]) : 0d;
                component.SemiProductNrD = !row["intermediate_nrD"].Equals(DBNull.Value) ? Convert.ToInt64(row["intermediate_nrD"]) : -2;
                component.VocPercent = !row["VOC"].Equals(DBNull.Value) ? Convert.ToDouble(row["VOC"]) : -1d;
                component.Density = !row["density"].Equals(DBNull.Value) ? Convert.ToDouble(row["density"]) : -1d;

                double rate = Convert.ToDouble(row["rate"]);
                if (component.PriceKg > 0 && rate > 0)
                {
                    component.PriceKg *= rate;
                    component.Price = component.PriceKg * component.Mass;
                }
                component.VOC = CalculateVOC(component);

                recipe.Add(component);
            }
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

        public double SumOfPrices(IList<Component> recipe)
        {
            return recipe.Count(x => x.PriceKg <= 0) > 0 ? -1 : recipe.Where(x => x.Level == 0).Select(x => x.Price).Sum();
        }

        public double PricePerKg(IList<Component> recipe, CompositionData compositionData)
        {
            double price = SumOfPrices(recipe);
            return compositionData.Mass > 0 ? price / compositionData.Mass : 0d;
        }

        public double PricePerL(IList<Component> recipe, CompositionData compositionData)
        {
            double price = PricePerKg(recipe, compositionData);
            return compositionData.Mass > 0 ? price * (double)compositionData.Density : 0d;
        }

        public double SumOfVoc(IList<Component> recipe)
        {
            return recipe.Count(x => x.VOC < 0) > 0 ? -1 : recipe.Where(x => x.Level == 0).Select(x => x.VOC).Sum();
        }

        public void RecalculateByAmount(IList<Component> recipe, CompositionData compositionData)
        {
            foreach(Component component in recipe)
            {
                if (component.Level > 0) continue;

                double amount = component.Amount;
                component.Mass = amount * compositionData.Mass / 100;
                component.Price = CalculatePrice(component);
                component.VOC = CalculateVOC(component);
            }
        }

        public void RecalculateByMass(IList<Component> recipe, CompositionData compositionData)
        {
            compositionData.Mass = SumOfMass(recipe);

            foreach (Component component in recipe)
            {
                if (component.Level > 0) continue;

                component.Amount = component.Mass / compositionData.Mass * 100;
                component.Price = CalculatePrice(component);
                component.VOC = CalculateVOC(component);
            }
        }

        public void UpdateComponent(Component component, CompositionData compositionData)
        {
            MaterialRepository materialRepository = new MaterialRepository();
            MaterialDto material = materialRepository.GetByName(component.Name);

            component.VocPercent = material.VOC;
            component.Density = material.Density;
            component.IsSemiProduct = material.IsIntermediate;
            component.SemiProductNrD = material.IntermediateNrD;
            component.VOC = CalculateVOC(component);

            if (material.Id > 0)
            {
                CurrencyRepository currencyRepository = new CurrencyRepository();
                CurrencyDto currency = currencyRepository.GetById(material.CurrencyId, CurrencyRepository.GetByIdQuery);
                decimal rate = currency.Rate;
                component.PriceKg = (double)(material.Price * rate);
                component.Price = CalculatePrice(component);
            }
        }

        private double CalculatePrice(Component component)
        {
            return component.Price = component.PriceKg > 0 ? component.PriceKg * component.Mass : 0d;
        }

        private double CalculateVOC(Component component)
        {
            return component.VOC = component.VocPercent >= 0 ? component.VocPercent * component.Amount / 100 : -1d;
        }
    }
}
