﻿using LabBook.ADO.Common;
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
        private int _id = 0;
        private readonly IRepository<CompositionDto> _repository = new CompositionRepository();
        private DataTable _dataTableMaterial;

        public void GetRecipe(IList<Component> recipe, CompositionData data)
        {
            DataTable table = _repository.GetAll(CompositionRepository.AllRecipeQuery + data.LabBookId.ToString());

            foreach (DataRow row in table.Rows)
            {
                Component component = new Component();

                component.Id = _id++;
                component.Name = row["component"].ToString();
                component.Ordering = Convert.ToInt32(row["ordering"]);
                component.IsSemiProduct = Convert.ToBoolean(row["is_intermediate"]);
                component.SemiProductNrD = !row["intermediate_nrD"].Equals(DBNull.Value) ? Convert.ToInt64(row["intermediate_nrD"]) : -2;
                component.AmountOriginal = Convert.ToDouble(row["amount"]);
                component.Amount = Convert.ToDouble(row["amount"]);
                component.Mass = component.Amount * data.Mass / 100d;
                component.Operation = Convert.ToInt32(row["operation"]);
                component.OperationName = row["name"].ToString();
                component.Comment = row["comment"].ToString();
                component.PriceKg = !row["price"].Equals(DBNull.Value) ? Convert.ToDouble(row["price"]) : 0d;
                component.Rate = !row["rate"].Equals(DBNull.Value) ? Convert.ToDouble(row["rate"]) : 0d;
                component.Density = !row["density"].Equals(DBNull.Value) ? Convert.ToDouble(row["density"]) : -1d;

                UpdatePriceAndVoc(component, row);
                CompositionSubRecipeDto recipeDto = new CompositionSubRecipeDto(component.Id, component.Level, component.SemiProductNrD, component.Operation, component.Amount, component.Mass, component.ParentsId);
                component.SemiRecipe = component.IsSemiProduct ? GetSemiRecipe(recipeDto) : new List<Component>();

                recipe.Add(component);
            }
        }

        private IList<Component> GetSemiRecipe(CompositionSubRecipeDto recipeDto)
        {
            IList<Component> recipe = new List<Component>();
            DataTable table = _repository.GetAll(CompositionRepository.AllRecipeQuery + recipeDto.NrD.ToString());

            foreach (DataRow row in table.Rows)
            {
                Component component = new Component();

                component.Id = _id++;
                component.AddParent(recipeDto.ParentsId, recipeDto.Id); 
                component.Level = recipeDto.Level + 1;
                component.Name = row["component"].ToString();
                component.Ordering = Convert.ToInt32(row["ordering"]);
                component.IsSemiProduct = Convert.ToBoolean(row["is_intermediate"]);
                component.SemiProductNrD = !row["intermediate_nrD"].Equals(DBNull.Value) ? Convert.ToInt64(row["intermediate_nrD"]) : -2;
                component.AmountOriginal = Convert.ToDouble(row["amount"]);
                double amount = Convert.ToDouble(row["amount"]);
                component.Amount = amount * recipeDto.Amount / 100d;
                component.Mass = amount * recipeDto.Mass / 100d;
                component.Operation = recipeDto.Operation > 1 ? 3 : 1;
                component.PriceKg = !row["price"].Equals(DBNull.Value) ? Convert.ToDouble(row["price"]) : 0d;
                component.Rate = !row["rate"].Equals(DBNull.Value) ? Convert.ToDouble(row["rate"]) : 0d;
                component.Density = !row["density"].Equals(DBNull.Value) ? Convert.ToDouble(row["density"]) : -1d;

                UpdatePriceAndVoc(component, row);
                CompositionSubRecipeDto subRecipeDto = new CompositionSubRecipeDto(component.Id, component.Level, component.SemiProductNrD, component.Operation, component.Amount, component.Mass, component.ParentsId);
                component.SemiRecipe = component.IsSemiProduct ? GetSemiRecipe(subRecipeDto) : new List<Component>();

                recipe.Add(component);
            }

            UpdateSemiOperation(recipeDto, recipe);
            return recipe;
        }

        private void UpdatePriceAndVoc(Component component, DataRow row)
        {
            if (!component.IsSemiProduct)
            {
                component.PriceKg = component.PriceKg > 0 && component.Rate > 0 ? component.PriceKg * component.Rate : 0d;
                component.Price = component.PriceKg > 0 && component.Rate > 0 ? CalculatePrice(component) : 0d;
                component.VocPercent = !row["VOC"].Equals(DBNull.Value) ? Convert.ToDouble(row["VOC"]) : -1d;
            }
            else
            {
                component.PriceKg = CommonFunction.CalculatePrice(component.SemiProductNrD);
                component.Price = component.PriceKg > 0 ? CalculatePrice(component) : 0d;
                component.VocPercent = CommonFunction.CalculateVOC(component.SemiProductNrD);
                component.SemiStatus = "[+]";
            }
            component.VOC = CalculateVOC(component);
        }

        private void UpdateSemiOperation(CompositionSubRecipeDto recipeDto, IList<Component> recipe)
        {
            if (recipeDto.Operation == ((int)RecipeOperation.End) && recipe.Count > 0)
                recipe[recipe.Count - 1].Operation = recipeDto.Operation;

            if (recipe.Count == 1)
            {
                recipe[0].SubOrdering = SubRecipeOrdering.onlyOne;
            }
            else if (recipe.Count > 1)
            {
                recipe[0].SubOrdering = SubRecipeOrdering.top;
                recipe[recipe.Count - 1].SubOrdering = SubRecipeOrdering.bottom;
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

        public void RecalculateByAmount(IList<Component> recipe, double mass, int level, double parentAmount)
        {
            foreach(Component component in recipe)
            {
                if (component.Level > level) continue;

                double amount = level == 0 ? component.Amount : component.AmountOriginal * parentAmount / 100d;
                component.Amount = amount;
                component.Mass = amount * mass / 100d;
                component.Price = CalculatePrice(component);
                component.VOC = CalculateVOC(component);
                if (component.IsSemiProduct)
                {
                    RecalculateByAmount(component.SemiRecipe, mass, level + 1, component.Amount);
                }
            }
        }

        public void RecalculateByMass(IList<Component> recipe, double mass, int level, double parentAmount)
        {
            foreach (Component component in recipe)
            {
                if (component.Level > level) continue;

                component.Amount = level == 0 ? component.Mass / mass * 100d : parentAmount * component.AmountOriginal / 100d;
                component.Mass = level == 0 ? component.Mass : mass * component.AmountOriginal / 100d;
                component.Price = CalculatePrice(component);
                component.VOC = CalculateVOC(component);
                if (component.IsSemiProduct)
                {
                    RecalculateByMass(component.SemiRecipe, component.Mass, level + 1, component.Amount);
                }
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

        public void ExpandOrHideSemiRecipe(IList<Component> recipe, Component component, int index)
        {
            if (component.SemiStatus == "[+]")
            {
                ExpandSemiRecipe(recipe, component, index + 1);
            }
            else
            {
                HideSemiRecipe(recipe, component);
            }
        }

        public void ExpandSemiRecipe(IList<Component> recipe, Component component, int index)
        {
            if (component == null) return;

            foreach (Component child in component.SemiRecipe)
            {
                if (index < recipe.Count)
                    recipe.Insert(index, child);
                else
                    recipe.Add(child);
                index++;
            }
            component.SemiStatus = "[-]";
        }

        public void HideSemiRecipe(IList<Component> recipe, Component component)
        {
            if (component == null) return;

            int parentId = component.Id;
            IList<Component> childs = new List<Component>();
            foreach (Component child in recipe)
            {
                if (child.ParentsId.Contains(parentId))
                    childs.Add(child);
            }

            foreach (Component child in childs)
            {
                if (child.IsSemiProduct)
                    child.SemiStatus = "[+]";
                recipe.Remove(child);
            }

            component.SemiStatus = "[+]";
        }
    }
}
