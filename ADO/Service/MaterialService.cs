using LabBook.ADO.Common;
using LabBook.ADO.Exceptions;
using LabBook.ADO.Repository;
using LabBook.Dto;
using LabBook.Forms.ClpData.Model;
using LabBook.Forms.InputBox;
using LabBook.Forms.SemiProduct.ModelView;
using LabBook.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows;

namespace LabBook.ADO.Service
{
    public enum MaterialType
    {
        Material,
        SemiProduct
    }

    public enum PriceError
    {
        NoRecipe = -1,
        NoMaterialPrice = -2,
        NoCurrency = -3,
        NoSemiproduct = -4
    }

    public class MaterialService
    {
        private readonly IRepository<MaterialDto> _repository;
        private bool _modified = false;
        private DataTable _dataTable;
        private DataTable _dataTableClp;

        public MaterialService()
        {
            _repository = new MaterialRepository();
        }

        public bool Modified => _modified;

        public DataView GetAll(MaterialType type)
        {
            string query;
            switch (type)
            {
                case MaterialType.Material:
                    query = MaterialRepository.AllMaterialQuery;
                    break;
                case MaterialType.SemiProduct:
                    query = MaterialRepository.AllSemiProductQuery;
                    break;
                default:
                    query = "";
                    break;
            }

            _dataTable = _repository.GetAll(query);
            _dataTable.RowChanged += DataTable_RowChanged;
            DataView view = new DataView(_dataTable) { Sort = "name" };
            return view;
        }

        public string GetAllClp(long materialId)
        {
            DataTable table = _repository.GetAll(ClpRepository.MaterialClpQuery.Replace("XXXX", materialId.ToString()));
            StringBuilder clp = new StringBuilder();

            for (int i = 0; i < table.Rows.Count; i++)
            {
                DataRow row = table.Rows[i];
                clp.Append(row["clp_full"].ToString().Replace("-- Brak --: ", ""));
                clp.Append("\r\n");
            }
            return clp.ToString();
        }

        public DataView GetAllClpView(long materialId)
        {
            _dataTableClp = _repository.GetAll(ClpRepository.MaterialClpQuery.Replace("XXXX", materialId.ToString()));
            DataView view = new DataView(_dataTableClp) { Sort = "ordering" };
            return view;
        }

        public void RefreshClpView(long materialId)
        {
            if (_dataTableClp == null) return;

            string query = ClpRepository.MaterialClpQuery.Replace("XXXX", materialId.ToString());
            MaterialRepository repository = (MaterialRepository)_repository;
            _dataTableClp.Rows.Clear();
            repository.RefreshTable(query, _dataTableClp);
        }

        public IDictionary<int, bool> GetAllGhs(long materialId)
        {
            var result = new Dictionary<int, bool>() {
                {1, false },
                {2, false },
                {3, false },
                {4, false },
                {5, false },
                {6, false },
                {7, false },
                {8, false },
                {9, false }
            };

            DataTable table = _repository.GetAll(ClpRepository.MaterialGhsQuery.Replace("XXXX", materialId.ToString()));
            foreach (DataRow row in table.Rows)
            {
                int i = Convert.ToInt32(row["GHS"]);
                result[i] = true;
            }

            return result;
        }

        private void DataTable_RowChanged(object sender, DataRowChangeEventArgs e)
        {
            _modified = true;
        }

        public MaterialDto AddNew()
        {
            string name = "";

            InputBox inputBox = new InputBox("Podaj nazwę nowego surowca:", "Nazwa");
            if (inputBox.ShowDialog() == true)
                name = inputBox.Answer;

            if (string.IsNullOrEmpty(name))
            {
                _ = MessageBox.Show("Nazwa surowca nie może byc pusta!", "Pusta nazwa", MessageBoxButton.OK, MessageBoxImage.Warning);
                return null;
            }

            if (_repository.ExistByName(name, MaterialRepository.ExistByNameQuery))
            {
                _ = MessageBox.Show("Surowiec o nazwie '" + name + "' istnieje już w bazie danych!", "Pusta nazwa", MessageBoxButton.OK, MessageBoxImage.Warning);
                return null;
            }

            MaterialDto material = new MaterialDto(name) { LoginId = UserSingleton.Id };
            return Save(material);
        }

        public MaterialDto AddNewSemiProduct()
        {
            string input = "";
            InputBox inputBox = new InputBox("Podaj numer D nowego półproduktu:", "Numer D");
            if (inputBox.ShowDialog() == true) input = inputBox.Answer;
            if (string.IsNullOrEmpty(input) || !long.TryParse(input, out long nrD))
            {
                _ = MessageBox.Show("Wprowadzony numer nie jest liczbą całkowitą!", "Zła wartość", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }

            LabBookRepository repository = new LabBookRepository();
            LabBookDto labBookDto = repository.GetById(nrD, LabBookRepository.GetByIdQuery);
            if (labBookDto == null)
            {
                _ = MessageBox.Show("Podany numer D" + nrD + " nie istnieje w bazie danych!", "Brak numeru", MessageBoxButton.OK, MessageBoxImage.Information);
                return null;
            }
            if (_repository.ExistById(nrD, MaterialRepository.ExistByIntDQuery))
            {
                _ = MessageBox.Show("Półprodukt o numerze D" + nrD + " istnieje już w tabeli półprodukty.", "Brak numeru", MessageBoxButton.OK, MessageBoxImage.Information);
                return new MaterialDto("", nrD);
            }

            MaterialDto material = new MaterialDto(labBookDto.Title, nrD) { LoginId = UserSingleton.Id };

            return Save(material);
        }

        public MaterialDto AddNewSemiProduct(long nrD, string name)
        {
            InputBox inputBox = new InputBox("Podaj nazwę nowego półproduktu:", name);
            if (inputBox.ShowDialog() == true) name = inputBox.Answer;
            if (string.IsNullOrEmpty(name))
            {
                _ = MessageBox.Show("Nazwa nie może być pusta", "Zła wartość", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
            if (_repository.ExistById(nrD, MaterialRepository.ExistByIntDQuery))
            {
                _ = MessageBox.Show("Półprodukt o numerze D" + nrD + " istnieje już w tabeli półprodukty.", "Brak numeru", MessageBoxButton.OK, MessageBoxImage.Information);
                return new MaterialDto("", nrD);
            }

            MaterialDto material = new MaterialDto(name, nrD) { LoginId = UserSingleton.Id };

            return Save(material);
        }

        private MaterialDto Save(MaterialDto material)
        {
            material = _repository.Save(material);
            if (material != null)
            {
                DataRow newRow = _dataTable.NewRow();
                newRow["id"] = material.Id;
                newRow["name"] = material.Name;
                newRow["is_intermediate"] = material.IsIntermediate;
                newRow["is_danger"] = material.IsDanger;
                newRow["is_production"] = material.IsProduction;
                newRow["is_active"] = material.IsActive;
                newRow["intermediate_nrD"] = material.IntermediateNrD;
                newRow["clp_signal_word_id"] = material.ClpSignalWordId;
                newRow["clp_msds_id"] = material.ClpMsdsId;
                newRow["function_id"] = material.FunctionId;
                newRow["price"] = material.Price;
                newRow["currency_id"] = material.CurrencyId;
                newRow["unit_id"] = material.UnitId;
                newRow["density"] = material.Density;
                newRow["solids"] = material.Solids;
                newRow["ash_450"] = material.Ash450;
                newRow["VOC"] = material.VOC;
                newRow["remarks"] = material.Remarks;
                newRow["login_id"] = material.LoginId;
                newRow["date_created"] = material.DateCreated;
                newRow["date_update"] = material.DateUpdated;

                bool tmp = _modified;
                _dataTable.Rows.Add(newRow);
                DataRow row = _dataTable.AsEnumerable()
                    .SingleOrDefault(r => r.Field<long>("id") == material.Id);
                row.AcceptChanges();
                _modified = tmp;
            }
            return material;
        }

        public bool Update()
        {
            var result = true;
            if (!_modified) return result;

            foreach (DataRow row in _dataTable.Rows)
            {
                if (row.RowState == DataRowState.Modified)
                {
                    if (_repository.Update(row, MaterialRepository.UpdateQuery) == ExceptionCode.NoError)
                        row.AcceptChanges();
                    else
                        result = false;
                }
            }

            if (result) _modified = false;

            return result;
        }

        public bool Delete(DataRowView row)
        {
            bool result = true;
            bool semi = Convert.ToBoolean(row["is_intermediate"]);
            string name = row["name"].ToString();
            long id = Convert.ToInt32(row["id"]);
            bool tmp = _modified;
            string type = "surowiec";

            if (semi)
            {
                type = "półprodukt";
            }
            if (MessageBox.Show("Czy usunąć " + type + " '" + name + "' z bazy danych?", "Usuwanie", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {
                return false;
            }

            result = _repository.Delete(id, MaterialRepository.DeleteQuery);
            if (!result)
            {
                return result;
            }

            _ = _repository.Delete(id, ClpRepository.DeleteMaterialClpQuery);
            _ = _repository.Delete(id, ClpRepository.DeleteMaterialGHSQuery);

            _modified = tmp;
            return result;
        }

        public bool UpdateGhsAndClp(long materialId, SelectedClpData data)
        {
            MaterialRepository repository = (MaterialRepository)_repository;

            bool delGhs = _repository.Delete(materialId, ClpRepository.DeleteMaterialGHSQuery);
            bool delClp = _repository.Delete(materialId, ClpRepository.DeleteMaterialClpQuery);

            data.GHS
                .Where(x => x.Value)
                .Select(x => x.Key)
                .ToList()
                .ForEach(x => repository.SaveGhs(materialId, x));

            data.CLP
                .Select(x => x)
                .ToList()
                .ForEach(x => repository.SaveClp(materialId, x));

            return delClp & delGhs;
        }

        public void CalculateSemiProductPrice(object sender, DoWorkEventArgs e)
        {
            int count = 0;
            //_dataTable.RowChanged -= DataTable_RowChanged;

            foreach (DataRow row in _dataTable.Rows)
            {
                long numberD = Convert.ToInt64(row["intermediate_nrD"]);
                double price = CalculatePrice(numberD, 100d);
                row["price"] = price;
                count++;
                (sender as BackgroundWorker).ReportProgress(count);
            }

            //_dataTable.RowChanged += DataTable_RowChanged;
            //_modified = true;
        }

        private double CalculatePrice(long numberD, double percent)
        {
            double totalPrice = 0d;
            _dataTable = _repository.GetAll(MaterialRepository.GetForPrice + numberD);
            if (_dataTable.Rows.Count == 0) 
            {
                return (double)PriceError.NoRecipe;
            }

            foreach(DataRow row in _dataTable.Rows)
            {
                if (row["is_intermediate"].Equals(DBNull.Value)) return (double)PriceError.NoSemiproduct;

                bool intermediate = Convert.ToBoolean(row["is_intermediate"]);
                double amount = Convert.ToDouble(row["amount"]);
                if (intermediate)
                {
                    long nr = Convert.ToInt64(row["intermediate_nrD"]);
                    double semiPrice = CalculatePrice(nr, amount);
                    if (semiPrice < 0) return semiPrice;
                    totalPrice += semiPrice;
                    continue;
                }

                double price = Convert.ToDouble(row["price"]);
                double rate = Convert.ToDouble(row["rate"]);
                if (price < 0)
                {
                    return (double)PriceError.NoMaterialPrice;
                }
                else if (rate < 0)
                {
                    return (double)PriceError.NoCurrency;
                }
                else
                {
                    totalPrice += amount * price * rate;
                }
            }

            return Math.Round(percent / 100 * (totalPrice / 100), 4);
        }

        public void CalculateSemiProductVOC(SemiProductFormMV mv)
        {

        }

    }
}
