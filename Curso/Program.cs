using System;
using Microsoft.EntityFrameworkCore;

namespace Curso
{
    class Program
    {
        static void Main(string[] args)
        {
            using var db = new Data.AplicationContext();
            // db.Database.Migrate(); // não recomendado

            var existe = db.Database.GetPendingMigrations().Any();
            if(existe)
            {
                Console.WriteLine("Existe migrações pendentes");
            }
        }
    }
}