namespace LabBook.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CmbClpPictogram")]
    public partial class CmbClpPictogram
    {
        public int id { get; set; }

        [Required]
        [StringLength(50)]
        public string description { get; set; }

        public int GHS { get; set; }

        [Required]
        [StringLength(50)]
        public string png_file { get; set; }
    }
}
