using LabBook.ADO.Common;
using LabBook.Dto;

namespace LabBook.ADO.Repository
{
    public class CompositionRepository : RepositoryCommon<CompositionDto>
    {
        public static readonly string AllRecipeQuery = "Select c.labbook_id, c.ordering, c.component, c.is_intermediate, c.amount, c.operation, c.comment, " +
            "m.price, m.currency_id, r.rate, m.intermediate_nrD, m.VOC, m.density From LabBook.dbo.ExpComposition c Left join(LabBook.dbo.Material m " +
            "Left join LabBook.dbo.CmbCurrency r on m.currency_id= r.id) on c.component=m.name Where c.labbook_id= ";
    }
}
