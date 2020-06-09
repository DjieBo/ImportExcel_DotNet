namespace DataImport.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class BuildingContext : DbContext
    {
        public BuildingContext()
            : base("name=BuildingContext")
        {
        }
        public virtual DbSet<Lokasi> Lokasi { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
