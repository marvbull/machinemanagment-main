using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore; // Vergewissern Sie sich, dass der richtige Namespace verwendet wird

namespace MMS.db_models
{
    public class db_connect : DbContext // Ändern Sie die Basisklasse zu Microsoft.EntityFrameworkCore.DbContext
    {
        public DbSet<Facharbeiter> Facharbeiter { get; set; }
        public DbSet<Vorgesetzter> Vorgesetzter { get; set; }
        public DbSet<Lehrgang_Zuweisung> LehrgangZuweisungen { get; set; }
        public DbSet<Lehrgang> Lehrgang { get; set; }
        public DbSet<Auftraege> Auftraege { get; set; }
        public DbSet<Aufgabe_Zuweisung> AufgabeZuweisungen { get; set; }
        public DbSet<Maschinen> Maschinen { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Stellen Sie sicher, dass die Verbindungszeichenfolge korrekt ist
                optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=MMS-Database;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Hier können Sie weitere Konfigurationen für Ihre Modelle definieren.
        }
    }
}