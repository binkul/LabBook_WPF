using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace LabBook.EntityFramework
{
    public partial class LabBookContex : DbContext
    {
        public LabBookContex()
            : base("name=LabBookContex")
        {
        }

        public virtual DbSet<CmbClpDataSheets> CmbClpDataSheets { get; set; }
        public virtual DbSet<CmbClpHP> CmbClpHP { get; set; }
        public virtual DbSet<CmbClpPictogram> CmbClpPictogram { get; set; }
        public virtual DbSet<CmbClpSignalWord> CmbClpSignalWord { get; set; }
        public virtual DbSet<CmbContrastClass> CmbContrastClass { get; set; }
        public virtual DbSet<CmbContrastType> CmbContrastType { get; set; }
        public virtual DbSet<CmbContrastYield> CmbContrastYield { get; set; }
        public virtual DbSet<CmbGlosClass> CmbGlosClass { get; set; }
        public virtual DbSet<CmbMaterialFunction> CmbMaterialFunction { get; set; }
        public virtual DbSet<CmbScrubClass> CmbScrubClass { get; set; }
        public virtual DbSet<CmbSemiProductType> CmbSemiProductType { get; set; }
        public virtual DbSet<CmbUnit> CmbUnit { get; set; }
        public virtual DbSet<CmbVOC> CmbVOC { get; set; }
        public virtual DbSet<CmbCurrency> CmbCurrency { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            _ = modelBuilder.Entity<CmbCurrency>()
                .Property(e => e.name)
                .IsFixedLength();

            _ = modelBuilder.Entity<CmbCurrency>()
                .Property(e => e.rate)
                .HasPrecision(7, 4);
        }
    }
}
