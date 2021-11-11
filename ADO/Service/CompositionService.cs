using LabBook.ADO.Common;
using LabBook.ADO.Repository;
using LabBook.Commons;
using LabBook.Dto;
using LabBook.Forms.Composition.Model;
using LabBook.Forms.InputBox;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;

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

    public enum RecipeOperation
    {
        None = 1,
        Start = 2,
        Middle = 3,
        End = 4
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
                Component component = GetNewComponent();

                component.Name = row["component"].ToString();
                component.Ordering = Convert.ToInt32(row["ordering"]);
                component.IsSemiProduct = Convert.ToBoolean(row["is_intermediate"]);
                component.SemiProductNrD = !row["intermediate_nrD"].Equals(DBNull.Value) ? Convert.ToInt64(row["intermediate_nrD"]) : -2;
                component.AmountOriginal = Math.Round(Convert.ToDouble(row["amount"]) * data.Amount / 100d, 4);
                component.Amount = component.AmountOriginal; // Convert.ToDouble(row["amount"]) * data.Amount / 100d;
                component.Mass = component.Amount * data.Mass / 100d;
                component.Operation = (RecipeOperation)Convert.ToInt32(row["operation"]);
                component.OperationName = row["name"].ToString();
                component.Comment = row["comment"].ToString();
                component.PriceKg = !row["price"].Equals(DBNull.Value) ? Convert.ToDouble(row["price"]) : 0d;
                component.Rate = !row["rate"].Equals(DBNull.Value) ? Convert.ToDouble(row["rate"]) : 0d;
                component.Density = !row["density"].Equals(DBNull.Value) ? Convert.ToDouble(row["density"]) : -1d;

                double voc = !row["VOC"].Equals(DBNull.Value) ? Convert.ToDouble(row["VOC"]) : -1d;
                UpdatePriceAndVoc(component, voc);
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
                Component component = GetNewComponent();

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
                component.Operation = recipeDto.Operation != RecipeOperation.None ? RecipeOperation.Middle : RecipeOperation.None;
                component.PriceKg = !row["price"].Equals(DBNull.Value) ? Convert.ToDouble(row["price"]) : 0d;
                component.Rate = !row["rate"].Equals(DBNull.Value) ? Convert.ToDouble(row["rate"]) : 0d;
                component.Density = !row["density"].Equals(DBNull.Value) ? Convert.ToDouble(row["density"]) : -1d;

                double voc = !row["VOC"].Equals(DBNull.Value) ? Convert.ToDouble(row["VOC"]) : -1d;
                UpdatePriceAndVoc(component, voc);
                CompositionSubRecipeDto subRecipeDto = new CompositionSubRecipeDto(component.Id, component.Level, component.SemiProductNrD, component.Operation, component.Amount, component.Mass, component.ParentsId);
                component.SemiRecipe = component.IsSemiProduct ? GetSemiRecipe(subRecipeDto) : new List<Component>();

                recipe.Add(component);
            }

            UpdateSemiOperation(recipeDto, recipe);
            return recipe;
        }

        public IList<Component> GetSemiRecipe(long numberD, double mass, double amount)
        {
            IList<Component> recipe = new List<Component>();
            CompositionData data = new CompositionData { LabBookId = numberD, Mass = mass, Amount = amount };
            GetRecipe(recipe, data);
            return recipe;
        }

        private void UpdatePriceAndVoc(Component component, double voc)
        {
            if (!component.IsSemiProduct)
            {
                component.PriceKg = component.PriceKg > 0 && component.Rate > 0 ? component.PriceKg * component.Rate : 0d;
                component.Price = component.PriceKg > 0 && component.Rate > 0 ? CalculatePrice(component) : 0d;
                component.VocPercent = voc;
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
            if (recipeDto.Operation == RecipeOperation.End && recipe.Count > 0)
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

        public Component GetNewComponent()
        {
            Component component = new Component
            {
                Id = _id++,
                Name = "-- Pusty --"
            };
            return component;
        }

        public CompositionData GetRecipeData(long numberD, string title, decimal density)
        {
            CompositionRepository repository = (CompositionRepository)_repository;
            return repository.GetRecipeData(numberD, title, density);
        }

        public decimal GetDensity(long numberD)
        {
            CompositionRepository repository = (CompositionRepository)_repository;
            return repository.GetDensityById(numberD);
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

                component.Amount = level == 0 ? Math.Round(component.Mass / mass * 100d, 4) : Math.Round(parentAmount * component.AmountOriginal / 100d, 4);
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

            component.Density = material.Density;
            component.IsSemiProduct = material.IsIntermediate;
            component.SemiProductNrD = material.IntermediateNrD;
            component.PriceKg = (double)material.Price;
            component.VocPercent = material.VOC;
            component.SemiStatus = "";

            if (material.Id > 0)
            {
                CurrencyRepository currencyRepository = new CurrencyRepository();
                CurrencyDto currency = currencyRepository.GetById(material.CurrencyId, CurrencyRepository.GetByIdQuery);

                component.Rate = (double)currency.Rate;
                UpdatePriceAndVoc(component, material.VOC);

            }

            CompositionSubRecipeDto recipeDto = new CompositionSubRecipeDto(component.Id, component.Level, component.SemiProductNrD, component.Operation, component.Amount, component.Mass, component.ParentsId);
            component.SemiRecipe = component.IsSemiProduct ? GetSemiRecipe(recipeDto) : new List<Component>();
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
            if (!component.IsSemiProduct) return;

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

        public void Reordering(IList<Component> recipe)
        {
            if (recipe == null) return;
            if (recipe.Count == 0) return;

            for (int i = 0; i < recipe.Count; i++)
            {
                if (recipe[i].Level > 0) continue;
                recipe[i].Ordering = i + 1;
            }

        }

        public void SetOperation(Component component, RecipeOperation operation)
        {
            component.Operation = operation;

            if (component.IsSemiProduct)
            {
                foreach (Component subComponent in component.SemiRecipe)
                {
                    RecipeOperation subOperation = operation == RecipeOperation.None ? RecipeOperation.None : RecipeOperation.Middle;
                    SetOperation(subComponent, subOperation);
                }
            }
        }

        public void BuildFrame(IList<Component> recipe)
        {
            bool start = false;
            foreach (Component component in recipe)
            {
                if (component.Operation == RecipeOperation.Start)
                    start = true;

                if (start && component.Operation != RecipeOperation.Start && component.Operation != RecipeOperation.End)
                    SetOperation(component, RecipeOperation.Middle);
                else if (!start)
                    SetOperation(component, RecipeOperation.None);

                if (component.Operation == RecipeOperation.End)
                    start = false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="recipe"></param>
        /// <param name="upDown"></param>
        /// <param name="component"></param>
        /// upDown=true => up; upDown=false => down
        /// <returns></returns>
        public Component FindFirstAround(IList<Component> recipe, Component component, bool upDown)
        {
            int pos = recipe.IndexOf(component);
            int loopStep = upDown ? -1 : 1;
            bool finish;
            do
            {
                pos += loopStep;
                if (recipe[pos].Level == 0)
                {
                    return recipe[pos];
                }

                if (upDown)
                {
                    finish = pos >= 0;
                }
                else
                {
                    finish = pos < recipe.Count;
                }

            }
            while (finish);
            return null;
        }

        public long GetRecipeNumber()
        {
            long numberD = 0;

            InputBox inputBox = new InputBox("Podaj numer D receptury do wgrania:", "Numer D");
            string tmp;
            if (inputBox.ShowDialog() == true)
                tmp = inputBox.Answer;
            else
                return numberD;

            if (!long.TryParse(tmp, out numberD))
            {
                MessageBox.Show("Wprowadzona wartość nie jest liczbą całkowitą.", "Zła wartość", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return numberD;
        }
    
        /// <summary>
        /// 
        /// </summary>
        /// <param name="recipe">original recipe</param>
        /// <param name="subRecipe">recipe to replace component</param>
        /// <param name="component">component to be replaced</param>
        public void InsertSubrecipe(IList<Component> recipe, IList<Component> subRecipe, Component component)
        {
            if (subRecipe.Count == 0)
            {
                MessageBox.Show("Brak składników dla wybranej receptury", "Brak składników", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            RecipeOperation startOperation;
            RecipeOperation midOperation;
            RecipeOperation endOperation;
            switch (component.Operation)
            {
                case RecipeOperation.Start:
                    startOperation = RecipeOperation.Start;
                    midOperation = RecipeOperation.Middle;
                    endOperation = RecipeOperation.Middle;
                    break;
                case RecipeOperation.Middle:
                    startOperation = RecipeOperation.Middle;
                    midOperation = RecipeOperation.Middle;
                    endOperation = RecipeOperation.Middle;
                    break;
                case RecipeOperation.End:
                    startOperation = RecipeOperation.Middle;
                    midOperation = RecipeOperation.Middle;
                    endOperation = RecipeOperation.End;
                    break;
                default:
                    startOperation = RecipeOperation.None;
                    midOperation = RecipeOperation.None;
                    endOperation = RecipeOperation.None;
                    break;
            }

            var i = recipe.IndexOf(component);
            HideSemiRecipe(recipe, component);
            recipe.Remove(component);
            foreach (Component comp in subRecipe)
            {
                comp.Operation = midOperation;
                recipe.Insert(i, comp);
                i++;
            }
            subRecipe[0].Operation = startOperation;
            subRecipe[subRecipe.Count - 1].Operation = endOperation;
        }

        public double SetNewTotalMassFromCompound(IList<Component> recipe, int index)
        {
            InputBox inputBox = new InputBox("Podaj ilość surowca do przeliczenia:", "Masa");
            string tmp;
            if (inputBox.ShowDialog() == true)
                tmp = inputBox.Answer;
            else
                return 0d;

            if (!double.TryParse(tmp, out double mass))
            {
                MessageBox.Show("Wprowadzona wartość nie jest liczbą.", "Zła wartość", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            Component component = recipe[index];
            return Math.Round(100d * mass / component.Amount, 4);
        }
    }
}
