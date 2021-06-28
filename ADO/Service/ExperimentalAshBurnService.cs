using LabBook.ADO.Common;
using LabBook.ADO.Repository;
using LabBook.Forms.MainForm.Model;
using System;
using System.Data;
using System.Linq;

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

        public ExpAshBurnCalculation Calculate(ExpAshBurns expAshBurns)
        {
            ExpAshBurnCalculation result = new ExpAshBurnCalculation();

            if (expAshBurns.Row_1 && expAshBurns.Paint1 > 0f)
            {
                result.Solid_1 = expAshBurns.Crucible105_1 - expAshBurns.Crucible1;
                result.Ash450_1 = expAshBurns.Crucible405_1 - expAshBurns.Crucible1;
                result.Ash900_1 = expAshBurns.Crucible900_1 - expAshBurns.Crucible1;
                result.Organic_1 = result.Solid_1 - result.Ash450_1;
                result.Chalk_1 = ((result.Ash450_1 - result.Ash900_1) * 100f) / 44f;
                result.Chalk_1 = result.Ash450_1 - result.Chalk_1;
                result.Titanium_1 = result.Ash450_1 - result.Chalk_1;
                result.Solid_1 = (result.Solid_1 * 100f) / expAshBurns.Paint1;
                result.Ash450_1 = (result.Ash450_1 * 100) / expAshBurns.Paint1;
                result.Ash900_1 = (result.Ash900_1 * 100) / expAshBurns.Paint1;
                result.Organic_1 = (result.Organic_1 * 100) / expAshBurns.Paint1;
                result.Titanium_1 = (result.Titanium_1 * 100) / expAshBurns.Paint1;
                result.Chalk_1 = (result.Chalk_1 * 100f) / expAshBurns.Paint1;
            }

            if (expAshBurns.Row_2 && expAshBurns.Paint2 > 0f)
            {
                result.Solid_2 = expAshBurns.Crucible105_2 - expAshBurns.Crucible2;
                result.Ash450_2 = expAshBurns.Crucible405_2 - expAshBurns.Crucible2;
                result.Ash900_2 = expAshBurns.Crucible900_2 - expAshBurns.Crucible2;
                result.Organic_2 = result.Solid_2 - result.Ash450_2;
                result.Chalk_2 = ((result.Ash450_2 - result.Ash900_2) * 100f) / 44f;
                result.Chalk_2 = result.Ash450_2 - result.Chalk_2;
                result.Titanium_2 = result.Ash450_2 - result.Chalk_2;
                result.Solid_2 = (result.Solid_2 * 100f) / expAshBurns.Paint2;
                result.Ash450_2 = (result.Ash450_2 * 100) / expAshBurns.Paint2;
                result.Ash900_2 = (result.Ash900_2 * 100) / expAshBurns.Paint2;
                result.Organic_2 = (result.Organic_2 * 100) / expAshBurns.Paint2;
                result.Titanium_2 = (result.Titanium_2 * 100) / expAshBurns.Paint2;
                result.Chalk_2 = (result.Chalk_2 * 100f) / expAshBurns.Paint2;
            }

            if (expAshBurns.Row_3 && expAshBurns.Paint3 > 0f)
            {
                result.Solid_3 = expAshBurns.Crucible105_3 - expAshBurns.Crucible3;
                result.Ash450_3 = expAshBurns.Crucible405_3 - expAshBurns.Crucible3;
                result.Ash900_3 = expAshBurns.Crucible900_3 - expAshBurns.Crucible3;
                result.Organic_3 = result.Solid_3 - result.Ash450_3;
                result.Chalk_3 = ((result.Ash450_3 - result.Ash900_3) * 100f) / 44f;
                result.Chalk_3 = result.Ash450_3 - result.Chalk_3;
                result.Titanium_3 = result.Ash450_3 - result.Chalk_3;
                result.Solid_3 = (result.Solid_3 * 100f) / expAshBurns.Paint3;
                result.Ash450_3 = (result.Ash450_3 * 100) / expAshBurns.Paint3;
                result.Ash900_3 = (result.Ash900_3 * 100) / expAshBurns.Paint3;
                result.Organic_3 = (result.Organic_3 * 100) / expAshBurns.Paint3;
                result.Titanium_3 = (result.Titanium_3 * 100) / expAshBurns.Paint3;
                result.Chalk_3 = (result.Chalk_3 * 100f) / expAshBurns.Paint3;
            }

            return result;
        }
    
        public ExpAshBurnCalculation CalculateAverage(ExpAshBurns expAshBurns)
        {
            ExpAshBurnCalculation result = Calculate(expAshBurns);

            result.SolidAvr = Math.Round(CalcAverage(result.Solid_1, result.Solid_2, result.Solid_3), 2);
            result.Ash450Avr = Math.Round(CalcAverage(result.Ash450_1, result.Ash450_2, result.Ash450_3), 2);
            result.Ash900Avr = Math.Round(CalcAverage(result.Ash900_1, result.Ash900_2, result.Ash900_3), 2);
            result.OrganicAvr = Math.Round(CalcAverage(result.Organic_1, result.Organic_2, result.Organic_3), 2);
            result.ChalkAvr = Math.Round(CalcAverage(result.Chalk_1, result.Chalk_2, result.Chalk_3), 2);
            result.TitaniumAvr = Math.Round(CalcAverage(result.Titanium_1, result.Titanium_2, result.Titanium_3), 2);

            return result;
        }

        private double CalcAverage(params double[] nums)
        {
            var result = (from num in nums
                          where num > 0
                          select num).ToArray();

            return  result.Length > 0 ? result.Average() : -1;   
        }
    }
}
