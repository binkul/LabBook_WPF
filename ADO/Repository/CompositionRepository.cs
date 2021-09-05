using LabBook.ADO.Common;
using LabBook.Dto;

namespace LabBook.ADO.Repository
{
    public class CompositionRepository : RepositoryCommon<CompositionDto>
    {
        public static readonly string AllRecipeQuery = "Select c.labbook_id, c.ordering, c.component, c.is_intermediate, c.amount, c.operation, " +
            "o.name, c.comment, m.price, m.currency_id, r.rate, m.intermediate_nrD, m.VOC, m.density From LabBook.dbo.ExpComposition c " +
            "Left Join(LabBook.dbo.Material m Left Join LabBook.dbo.CmbCurrency r on m.currency_id= r.id) on c.component=m.name " +
            "Left Join LabBook.dbo.CmbCompOperation o on c.operation= o.id Where c.labbook_id=";
        public static readonly string RecipeDataQuery = "Select Top 1 * From LabBook.dbo.ExpCompositionData Where labbook_id=";
    }
}
