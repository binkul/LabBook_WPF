namespace LabBook.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CmbGlosClass")]
    public partial class CmbGlosClass
    {
        public long id { get; set; }

        [Required]
        [StringLength(50)]
        public string name { get; set; }

        [Column(TypeName = "date")]
        public DateTime date { get; set; }
    }
}
