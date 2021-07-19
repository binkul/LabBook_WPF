using LabBook.ADO.Common;
using LabBook.Dto;

namespace LabBook.ADO.Repository
{
    public class ClpRepository : RepositoryCommon<ClpDto>
    {
        public static readonly string AllQuery = "Select id, class, clp, description, ordering, is_h, ghs_id, signal_id, date_created From LabBook.dbo.CmbClpHP Order by ordering";
        public static readonly string OnlyHquery = "Select id, class, clp, description, ordering, is_h, ghs_id, signal_id, date_created From LabBook.dbo.CmbClpHP Where is_h='true' Order by ordering";
        public static readonly string OnlyPquery = "Select id, class, clp, description, ordering, is_h, ghs_id, signal_id, date_created From LabBook.dbo.CmbClpHP Where is_h='false' Order by ordering";
        public static readonly string MaterialClpQuery = "Select m.id, m.clp_id, (c.class + ': ' + c.clp + ' - ' + c.description) As clp_full, c.class, c.clp, c.description, c.ordering From " +
            "LabBook.dbo.MaterialCLP m Left join LabBook.dbo.CmbClpHP c On m.clp_id=c.id Where m.material_id=XXXX and c.is_h='true' Union All Select m.id, m.clp_id, (c.clp + ' - ' + c.description) " +
            "As clp_full, '' As class, c.clp, c.description, c.ordering From LabBook.dbo.MaterialCLP m Left join LabBook.dbo.CmbClpHP c On m.clp_id=c.id Where m.material_id=XXXX and " +
            "c.is_h='false' Order By c.ordering";
        public static readonly string MaterialGhsQuery = "Select c.GHS, c.description from LabBook.dbo.MaterialGHS m Left Join LabBook.dbo.CmbClpPictogram c on m.ghs_id=c.id " +
            "Where m.material_id=XXXX Order By c.GHS";
        public static readonly string DeleteMaterialGHSQuery = "Delete From LabBook.dbo.MaterialGHS Where material_id=";
        public static readonly string DeleteMaterialClpQuery = "Delete From LabBook.dbo.MaterialCLP Where material_id=";
        public static readonly string GetClpHPData = "Select id, clp, class, description, ordering From LabBook.dbo.CmbClpHP Order By ordering";
    }
}
