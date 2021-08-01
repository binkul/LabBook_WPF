namespace LabBook.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CmbVOC")]
    public partial class CmbVOC
    {
        public long id { get; set; }

        [Required]
        [StringLength(50)]
        public string VOC { get; set; }

        [Column(TypeName = "date")]
        public DateTime date { get; set; }
    }
}
