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

        //Pfad Marvin: optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=MMS-Database;Trusted_Connection=True;");
        //Pfad Carla:
        //Pfad Julian:
        //Pfad Nikolas:


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Aufgabe_Zuweisung>()
            .HasKey(a => a.Eintrag); // Ersetzen Sie SomeUniqueColumn durch eine geeignete eindeutige Spalte

            modelBuilder.Entity<Auftraege>()
            .HasKey(a => a.Auftrags_ID); // Ersetzen Sie SomeUniqueColumn durch eine geeignete eindeutige Spalte

            modelBuilder.Entity<Facharbeiter>()
            .HasKey(a => a.Facharbeiter_ID); // Ersetzen Sie SomeUniqueColumn durch eine geeignete eindeutige Spalte

            modelBuilder.Entity<Lehrgang>()
            .HasKey(a => a.Lehrgang_ID); // Ersetzen Sie SomeUniqueColumn durch eine geeignete eindeutige Spalte

            modelBuilder.Entity<Lehrgang_Zuweisung>()
            .HasKey(a => a.Eintrag); // Ersetzen Sie SomeUniqueColumn durch eine geeignete eindeutige Spalte

            modelBuilder.Entity<Maschinen>()
            .HasKey(a => a.Maschinen_ID); // Ersetzen Sie SomeUniqueColumn durch eine geeignete eindeutige Spalte

            modelBuilder.Entity<Vorgesetzter>()
            .HasKey(a => a.ID_Vorgesetzer); // Ersetzen Sie SomeUniqueColumn durch eine geeignete eindeutige Spalte

        }
    }
}