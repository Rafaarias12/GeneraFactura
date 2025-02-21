using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Entidades;

namespace Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options): base(options)
        {
            
        }

        public DbSet<Factura> Facturas { get; set; }
        public DbSet<DetalleFactura> DetalleFacturas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Factura>().ToTable("Facturas");
            modelBuilder.Entity<DetalleFactura>().ToTable("DetalleFacturas");

            modelBuilder.Entity<Factura>()
                .HasMany(f => f.DetalleFacturas) 
                .WithOne(d => d.Facturas) 
                .HasForeignKey(d => d.FacturaId); 
        }
    }
}
