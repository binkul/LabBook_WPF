using LabBook.ADO.Common;
using LabBook.Dto;
using System.Data;

namespace LabBook.ADO.Service
{
    public enum ComboType
    {
        MaterialFunction,
        Currency,
        Unit,
        Signal,
        SemiProduct
    }

    public class ComboListService
    {
        private readonly IRepository<ComboListDto> _repository;

        public ComboListService()
        {
            _repository = new ComboListRepository();
        }

        public DataView GetComboView(ComboType type)
        {
            string query = "";
            switch (type)
            {
                case ComboType.MaterialFunction:
                    query = ComboListRepository.MaterialFunctionQuery;
                    break;
                case ComboType.Currency:
                    query = ComboListRepository.CurrencyQuery;
                    break;
                case ComboType.Unit:
                    query = ComboListRepository.UnitQuery;
                    break;
                case ComboType.Signal:
                    query = ComboListRepository.SignalQuery;
                    break;
                case ComboType.SemiProduct:
                    query = ComboListRepository.SemiProductType;
                    break;
                default:
                    break;
            }
            DataTable table = _repository.GetAll(query);
            DataView view = new DataView(table) { Sort = "name" };
            return view;
        }
    }
}
