namespace LabBook.EntityFramework
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("CmbSemiProductType")]
    public partial class CmbSemiProductType
    {
        public int id { get; set; }

        [Required]
        [StringLength(100)]
        public string name { get; set; }
    }
}
