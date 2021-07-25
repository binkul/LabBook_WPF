using LabBook.Dto;

namespace LabBook.ADO.Common
{
    public class ComboListRepository : RepositoryCommon<ComboListDto>
    {
        public static readonly string MaterialFunctionQuery = "Select id, name From LabBook.dbo.CmbMaterialFunction Order By name";
        public static readonly string CurrencyQuery = "Select id, name from LabBook.dbo.CmbCurrency Order by name";
        public static readonly string UnitQuery = "Select id, name from LabBook.dbo.CmbUnit Order by name";
        public static readonly string SignalQuery = "Select id, name From LabBook.dbo.CmbClpSignalWord Order By name";
        public static readonly string SemiProductType = "Select id, name From LabBook.dbo.CmbSemiProductType Order By name";
    }
}
