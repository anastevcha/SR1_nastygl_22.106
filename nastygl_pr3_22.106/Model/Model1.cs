using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace nastygl_pr3_22._106.Model
{
    public partial class Model11 : DbContext
    {
        private static Model11 _instance;
        public Model11()
            : base("name=Model11")
        {
        }
        public static Model11 GetContext()
        {
            if (_instance == null)
            {
                _instance = new Model11();
            }
            return _instance;
        }

        public virtual DbSet<Avtorizacia> Avtorizacia { get; set; }
        public virtual DbSet<Cvet_Materiala> Cvet_Materiala { get; set; }
        public virtual DbSet<Doljnost> Doljnost { get; set; }
        public virtual DbSet<Izdeliya> Izdeliya { get; set; }
        public virtual DbSet<Klienty> Klienty { get; set; }
        public virtual DbSet<Materialy> Materialy { get; set; }
        public virtual DbSet<Modeli> Modeli { get; set; }
        public virtual DbSet<Otchety> Otchety { get; set; }
        public virtual DbSet<Otzyvy> Otzyvy { get; set; }
        public virtual DbSet<Postavshiki> Postavshiki { get; set; }
        public virtual DbSet<Razmer_Odejdy> Razmer_Odejdy { get; set; }
        public virtual DbSet<Skidki> Skidki { get; set; }
        public virtual DbSet<Sklad> Sklad { get; set; }
        public virtual DbSet<Sotrudniki> Sotrudniki { get; set; }
        public virtual DbSet<Status_Zakaza> Status_Zakaza { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Tip_Materiala> Tip_Materiala { get; set; }
        public virtual DbSet<Zakazy> Zakazy { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Avtorizacia>()
                .HasMany(e => e.Sotrudniki)
                .WithRequired(e => e.Avtorizacia)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Cvet_Materiala>()
                .HasMany(e => e.Materialy)
                .WithRequired(e => e.Cvet_Materiala1)
                .HasForeignKey(e => e.Cvet_Materiala)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Doljnost>()
                .HasMany(e => e.Sotrudniki)
                .WithRequired(e => e.Doljnost1)
                .HasForeignKey(e => e.Doljnost)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Izdeliya>()
                .Property(e => e.Stoimost_Izdelia)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Izdeliya>()
                .HasMany(e => e.Zakazy)
                .WithRequired(e => e.Izdeliya)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Klienty>()
                .Property(e => e.Nomer_telefona)
                .HasPrecision(16, 0);

            modelBuilder.Entity<Klienty>()
                .HasMany(e => e.Otzyvy)
                .WithRequired(e => e.Klienty)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Klienty>()
                .HasMany(e => e.Zakazy)
                .WithRequired(e => e.Klienty)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Materialy>()
                .Property(e => e.Stoimost_Za_Metr)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Materialy>()
                .HasMany(e => e.Zakazy)
                .WithRequired(e => e.Materialy)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Modeli>()
                .Property(e => e.Cena)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Modeli>()
                .HasMany(e => e.Sklad)
                .WithRequired(e => e.Modeli)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Modeli>()
                .HasMany(e => e.Zakazy)
                .WithRequired(e => e.Modeli)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Postavshiki>()
                .HasMany(e => e.Materialy)
                .WithRequired(e => e.Postavshiki)
                .HasForeignKey(e => e.Postavshik)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Razmer_Odejdy>()
                .HasMany(e => e.Zakazy)
                .WithRequired(e => e.Razmer_Odejdy)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Skidki>()
                .HasMany(e => e.Zakazy)
                .WithRequired(e => e.Skidki1)
                .HasForeignKey(e => e.Skidki)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Sotrudniki>()
                .Property(e => e.Zarplata)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Sotrudniki>()
                .HasMany(e => e.Otchety)
                .WithRequired(e => e.Sotrudniki)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Sotrudniki>()
                .HasMany(e => e.Zakazy)
                .WithRequired(e => e.Sotrudniki)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Status_Zakaza>()
                .HasMany(e => e.Zakazy)
                .WithRequired(e => e.Status_Zakaza1)
                .HasForeignKey(e => e.Status_Zakaza)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Tip_Materiala>()
                .HasMany(e => e.Materialy)
                .WithRequired(e => e.Tip_Materiala1)
                .HasForeignKey(e => e.Tip_Materiala)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Zakazy>()
                .Property(e => e.Summa_Zakaza)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Zakazy>()
                .HasMany(e => e.Otzyvy)
                .WithRequired(e => e.Zakazy)
                .WillCascadeOnDelete(false);
        }
    }
}
