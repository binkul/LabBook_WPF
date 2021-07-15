using LabBook.ADO.Common;
using LabBook.ADO.Exceptions;
using LabBook.ADO.Repository;
using LabBook.Dto;
using LabBook.Forms.InputBox;
using LabBook.Security;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows;

namespace LabBook.ADO.Service
{
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

        public DataView GetAll()
        {
            _dataTable = _repository.GetAll(MaterialRepository.AllQuery);
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
            MaterialDto material = new MaterialDto("");
            var name = "";

            InputBox inputBox = new InputBox("Podaj nazwę nowego surowca:", "Nazwa");
            if (inputBox.ShowDialog() == true)
                name = inputBox.Answer;

            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Nazwa surowca nie może byc pusta!", "Pusta nazwa", MessageBoxButton.OK, MessageBoxImage.Warning);
                return null;
            }

            if (_repository.ExistByName(name, MaterialRepository.ExistByNameQuery))
            {
                MessageBox.Show("Surowiec o nazwie '" + name + "' istnieje już w bazie danych!", "Pusta nazwa", MessageBoxButton.OK, MessageBoxImage.Warning);
                return null;
            }

            material.Name = name;
            material.LoginId = UserSingleton.Id;

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

                var tmp = _modified;
                _dataTable.Rows.Add(newRow);
                var row = _dataTable.AsEnumerable()
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
            string name = row["name"].ToString();
            long id = Convert.ToInt32(row["id"]);
            bool tmp = _modified;

            if (MessageBox.Show("Czy usunąć surowiec '" + name + "' z bazy danych?", "Usuwanie", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {
                return false;
            }

            result = _repository.Delete(id, MaterialRepository.DeleteQuery);
            if (!result)
            {
                return result;
            }

            DataRow dr = _dataTable.AsEnumerable().SingleOrDefault(r => r.Field<long>("id") == id);
            if (dr != null)
            {
                dr.Delete();
                dr.AcceptChanges();
            }
            _ = _repository.Delete(id, ClpRepository.DeleteMaterialClpQuery);
            _ = _repository.Delete(id, ClpRepository.DeleteMaterialGHSQuery);

            _modified = tmp;
            return result;
        }
    }
}
