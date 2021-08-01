namespace LabBook.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CmbContrastYield")]
    public partial class CmbContrastYield
    {
        public long id { get; set; }

        [StringLength(50)]
        public string name { get; set; }

        [Column(TypeName = "date")]
        public DateTime date { get; set; }
    }
}
