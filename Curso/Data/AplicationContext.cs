﻿using Curso.Data.Configurations;
using Curso.Domain;
using Microsoft.EntityFrameworkCore;

namespace Curso.Data
{
    internal class AplicationContext : DbContext
    {
        public DbSet<Pedido> Pedidos { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data source=(localdb)\\mssqllocaldb;Initial Catalog=CursoEFCore;Integrated Security=true");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AplicationContext).Assembly);

        }
    }
}