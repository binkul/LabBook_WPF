namespace LabBook.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CmbScrubClass")]
    public partial class CmbScrubClass
    {
        public long id { get; set; }

        [Required]
        [StringLength(15)]
        public string name { get; set; }

        [Column(TypeName = "date")]
        public DateTime? date { get; set; }
    }
}
