namespace LabBook.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CmbClpHP")]
    public partial class CmbClpHP
    {
        public long id { get; set; }

        [Column("class")]
        [Required]
        [StringLength(50)]
        public string _class { get; set; }

        [Required]
        [StringLength(50)]
        public string clp { get; set; }

        [Required]
        [StringLength(500)]
        public string description { get; set; }

        public int ordering { get; set; }

        public bool is_h { get; set; }

        public int ghs_id { get; set; }

        public int signal_id { get; set; }

        [Column(TypeName = "date")]
        public DateTime date_created { get; set; }
    }
}
