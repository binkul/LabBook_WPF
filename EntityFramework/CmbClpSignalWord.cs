namespace LabBook.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CmbClpSignalWord")]
    public partial class CmbClpSignalWord
    {
        public int id { get; set; }

        [Required]
        [StringLength(50)]
        public string name { get; set; }
    }
}
