namespace LabBook.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CmbCurrency")]
    public partial class CmbCurrency
    {
        [Key]
        [Column(Order = 0)]
        public int id { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string name { get; set; }

        [Key]
        [Column(Order = 2)]
        public decimal rate { get; set; }

        [Key]
        [Column(Order = 3, TypeName = "date")]
        public DateTime date_crated { get; set; }
    }
}
