namespace LabBook.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CmbUnit")]
    public partial class CmbUnit
    {
        public int id { get; set; }

        [Required]
        [StringLength(50)]
        public string name { get; set; }
    }
}
