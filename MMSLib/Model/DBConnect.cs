using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore; 

namespace MMSLib.Model
{
    public class DBConnect : DbContext 
    {
        public DbSet<Facharbeiter> Facharbeiter { get; set; }
        public DbSet<Vorgesetzter> Vorgesetzter { get; set; }
        public DbSet<LehrgangZuweisung> LehrgangZuweisung { get; set; }
        public DbSet<Lehrgang> Lehrgang { get; set; }
        public DbSet<Auftraege> Auftraege { get; set; }
        public DbSet<AufgabenZuweisen> AufgabenZuweisen { get; set; }
        public DbSet<Maschine> Maschine { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Stellen Sie sicher, dass die Verbindungszeichenfolge korrekt ist
                optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=MMS-Database;Trusted_Connection=True;");
            }
        }

        //Pfad Marvin: optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=MMS-Database;Trusted_Connection=True;");
        //Pfad Carla:optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=MMS-Database;Trusted_Connection=True;");
    //Pfad Julian:
    //Pfad Nikolas:


    protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AufgabenZuweisen>()
            .HasKey(a => a.Eintrag); // Ersetzen Sie SomeUniqueColumn durch eine geeignete eindeutige Spalte

            modelBuilder.Entity<Auftraege>()
            .HasKey(a => a.AuftragsID); // Ersetzen Sie SomeUniqueColumn durch eine geeignete eindeutige Spalte

            modelBuilder.Entity<Facharbeiter>()
            .HasKey(a => a.FacharbeiterID); // Ersetzen Sie SomeUniqueColumn durch eine geeignete eindeutige Spalte

            modelBuilder.Entity<Lehrgang>()
            .HasKey(a => a.LehrgangID); // Ersetzen Sie SomeUniqueColumn durch eine geeignete eindeutige Spalte

            modelBuilder.Entity<LehrgangZuweisung>()
            .HasKey(a => a.EintragLehrgang); // Ersetzen Sie SomeUniqueColumn durch eine geeignete eindeutige Spalte

            modelBuilder.Entity<Maschine>()
            .HasKey(a => a.MaschinenID); // Ersetzen Sie SomeUniqueColumn durch eine geeignete eindeutige Spalte

            modelBuilder.Entity<Vorgesetzter>()
            .HasKey(a => a.VorgesetzerID); // Ersetzen Sie SomeUniqueColumn durch eine geeignete eindeutige Spalte

        }
    }
}