namespace LabBook.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CmbContrastClass")]
    public partial class CmbContrastClass
    {
        public long id { get; set; }

        [StringLength(50)]
        public string name { get; set; }

        [Column(TypeName = "date")]
        public DateTime date { get; set; }
    }
}
