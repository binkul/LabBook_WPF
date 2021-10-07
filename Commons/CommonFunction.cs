using LabBook.ADO.Repository;
using System;
using System.Data;

namespace LabBook.Commons
{
    public enum PriceError
    {
        NoRecipe = -1,
        NoMaterialPrice = -2,
        NoCurrency = -3,
        NoSemiproduct = -4
    }

    public enum VocError
    {
        NoRecipe = -1,
        NoMaterialVOC = -2,
        NoSemiproduct = -3
    }

    public class CommonFunction
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="numberD"></param>
        /// <returns> price per 1 kg in double </returns>
        public static double CalculatePrice(long numberD)
        {
            double totalPrice = 0d;
            MaterialRepository repository = new MaterialRepository();
            DataTable dataTable = repository.GetAll(MaterialRepository.GetForPrice + numberD);
            if (dataTable.Rows.Count == 0) return (double)PriceError.NoRecipe;

            foreach (DataRow row in dataTable.Rows)
            {
                if (row["is_intermediate"].Equals(DBNull.Value)) return (double)PriceError.NoRecipe;
                bool intermediate = Convert.ToBoolean(row["is_intermediate"]);

                if (row["price"].Equals(DBNull.Value) && !intermediate) return (double)PriceError.NoMaterialPrice;
                if (row["rate"].Equals(DBNull.Value)) return (double)PriceError.NoCurrency;

                double amount = Convert.ToDouble(row["amount"]);
                double price = Convert.ToDouble(row["price"]);
                double rate = Convert.ToDouble(row["rate"]);

                if (intermediate)
                {
                    long nr = Convert.ToInt64(row["intermediate_nrD"]);
                    price = CalculatePrice(nr);
                    rate = 1;
                }
                if (price < 0) return price;

                totalPrice += amount * price * rate;
            }
            return totalPrice > 0 ? totalPrice / 100 : totalPrice;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="numberD"></param>
        /// <returns> VOC % in double </returns>
        public static double CalculateVOC(long numberD)
        {
            double totalVOC = 0d;
            MaterialRepository repository = new MaterialRepository();
            DataTable dataTable = repository.GetAll(MaterialRepository.GetForVOC + numberD);
            if (dataTable.Rows.Count == 0) return (double)PriceError.NoRecipe;

            foreach (DataRow row in dataTable.Rows)
            {

                if (row["is_intermediate"].Equals(DBNull.Value)) return (double)VocError.NoRecipe;
                bool intermediate = Convert.ToBoolean(row["is_intermediate"]);
                if (row["VOC"].Equals(DBNull.Value) && !intermediate) return (double)VocError.NoMaterialVOC;

                double amount = Convert.ToDouble(row["amount"]);
                double voc = 0;
                if (!intermediate)
                {
                    voc = Convert.ToDouble(row["VOC"]);
                }
                else
                {
                    long nr = Convert.ToInt64(row["intermediate_nrD"]);
                    voc = CalculateVOC(nr);
                }
                if (voc < 0) return voc;

                totalVOC += amount * voc;
            }

            return totalVOC > 0 ? Math.Round((totalVOC / 100), 2) : totalVOC;
        }

    }
}
