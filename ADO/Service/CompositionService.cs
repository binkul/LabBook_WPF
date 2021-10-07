using LabBook.ADO.Common;
using LabBook.ADO.Repository;
using LabBook.Commons;
using LabBook.Dto;
using LabBook.Forms.Composition.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace LabBook.ADO.Service
{
    public enum RecipeOperation
    {
        none = 1,
        start = 2,
        middle = 3,
        end = 4
    }

    public enum RecipeLevelType
    {
        mainLevel = 0,
        firstLevel = 1,
        secondLevel = 2,
        thirdLevel = 3,
        fourthLevel = 4
    }

    public enum SubRecipeOrdering
    {
        none = 0,
        onlyOne = 1,
        top = 2,
        middle = 3,
        bottom = 4
    }

    public class CompositionService
    {
        private readonly IRepository<CompositionDto> _repository = new CompositionRepository();
        private DataTable _dataTableMaterial;

        public void GetRecipe(IList<Component> recipe, CompositionData data)
        {
            DataTable table = _repository.GetAll(CompositionRepository.AllRecipeQuery + data.LabBookId.ToString());

            foreach (DataRow row in table.Rows)
            {
                Component component = new Component();

                component.Ordering = Convert.ToInt32(row["ordering"]);
                component.Name = row["component"].ToString();
                component.IsSemiProduct = Convert.ToBoolean(row["is_intermediate"]);
                component.Amount = Convert.ToDouble(row["amount"]);
                component.Operation = Convert.ToInt32(row["operation"]);
                component.OperationName = row["name"].ToString();
                component.Comment = row["comment"].ToString();
                component.Mass = component.Amount * data.Mass / 100;
                component.PriceKg = !row["price"].Equals(DBNull.Value) ? Convert.ToDouble(row["price"]) : 0d;
                component.SemiProductNrD = !row["intermediate_nrD"].Equals(DBNull.Value) ? Convert.ToInt64(row["intermediate_nrD"]) : -2;
                component.Density = !row["density"].Equals(DBNull.Value) ? Convert.ToDouble(row["density"]) : -1d;

                if (!component.IsSemiProduct)
                {
                    double rate = Convert.ToDouble(row["rate"]);
                    component.PriceKg = component.PriceKg > 0 && rate > 0 ? component.PriceKg * rate : 0d;
                    component.Price = component.PriceKg > 0 && rate > 0 ? CalculatePrice(component) : 0d;
                    component.VocPercent = !row["VOC"].Equals(DBNull.Value) ? Convert.ToDouble(row["VOC"]) : -1d;
                }
                else
                {
                    component.PriceKg = CommonFunction.CalculatePrice(component.SemiProductNrD);
                    component.Price = component.PriceKg > 0 ? CalculatePrice(component) : 0d;
                    component.VocPercent = CommonFunction.CalculateVOC(component.SemiProductNrD);
                }

                component.VOC = CalculateVOC(component);
                component.SemiStatus = component.IsSemiProduct ? "[+]" : "";
                component.SemiRecipe = component.IsSemiProduct ? GetSemiRecipe(component.Level, component.SemiProductNrD, component.Operation, component.Amount, component.Mass) : new List<Component>();

                recipe.Add(component);
            }
        }

        public CompositionData GetRecipeData(long numberD, string title, decimal density)
        {
            CompositionRepository repository = (CompositionRepository)_repository;
            return repository.GetRecipeData(numberD, title, density);
        }

        private IList<Component> GetSemiRecipe(int level, long nrD, int operation, double percent, double mass)
        {
            IList<Component> recipe = new List<Component>();
            DataTable table = _repository.GetAll(CompositionRepository.AllRecipeQuery + nrD.ToString());

            foreach (DataRow row in table.Rows)
            {
                Component component = new Component();

                double amount = Convert.ToDouble(row["amount"]);
                component.Name = row["component"].ToString();
                component.Amount = amount * percent / 100d;
                component.Mass = amount * mass / 100;
                component.IsSemiProduct = Convert.ToBoolean(row["is_intermediate"]);
                component.Ordering = Convert.ToInt32(row["ordering"]);
                component.Level = level + 1;
                component.PriceKg = !row["price"].Equals(DBNull.Value) ? Convert.ToDouble(row["price"]) : 0d;
                component.Operation = operation > 1 ? 3 : 1;
                component.VocPercent = !row["VOC"].Equals(DBNull.Value) ? Convert.ToDouble(row["VOC"]) : -1d;
                component.Density = !row["density"].Equals(DBNull.Value) ? Convert.ToDouble(row["density"]) : -1d;
                component.SemiProductNrD = !row["intermediate_nrD"].Equals(DBNull.Value) ? Convert.ToInt64(row["intermediate_nrD"]) : -2;

                double rate = Convert.ToDouble(row["rate"]);
                component.PriceKg = component.PriceKg > 0 && rate > 0 ? component.PriceKg * rate : 0d;
                component.Price = component.PriceKg > 0 && rate > 0 ? CalculatePrice(component) : 0d;
                component.VOC = CalculateVOC(component);

                component.SemiStatus = component.IsSemiProduct ? "[+]" : "";
                component.SemiRecipe = component.IsSemiProduct ? GetSemiRecipe(component.Level, component.SemiProductNrD, component.Operation, component.Amount, component.Mass) : new List<Component>();

                recipe.Add(component);
            }

            if (operation == ((int)RecipeOperation.end) && recipe.Count > 0)
                recipe[recipe.Count - 1].Operation = operation;

            if (recipe.Count == 1)
            {
                recipe[0].SubOrdering = SubRecipeOrdering.onlyOne;
            }
            else if (recipe.Count > 1)
            {
                recipe[0].SubOrdering = SubRecipeOrdering.top;
                recipe[recipe.Count - 1].SubOrdering = SubRecipeOrdering.bottom;
            }

            return recipe;
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
