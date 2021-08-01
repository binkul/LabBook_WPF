namespace LabBook.EntityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CmbClpDataSheets
    {
        public long id { get; set; }

        [Required]
        [StringLength(500)]
        public string name { get; set; }

        [StringLength(500)]
        public string remarks { get; set; }
    }
}
