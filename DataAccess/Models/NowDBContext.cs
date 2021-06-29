using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace DataAccess.Models
{
    public partial class NowDBContext : DbContext
    {
        public NowDBContext()
        {
        }

        public NowDBContext(DbContextOptions<NowDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Currency> Currencies { get; set; }
        public virtual DbSet<Instrument> Instruments { get; set; }
        public virtual DbSet<Market> Markets { get; set; }
        public virtual DbSet<PriceHistory> PriceHistories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=Dawood-PC;Database=NowDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Currency>(entity =>
            {
                entity.Property(e => e.Isocode)
                    .IsRequired()
                    .HasMaxLength(3)
                    .HasColumnName("ISOCode");

                entity.Property(e => e.Isonumber).HasColumnName("ISONumber");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.ValueInNzd)
                    .HasColumnType("money")
                    .HasColumnName("ValueInNZD");
            });

            modelBuilder.Entity<Instrument>(entity =>
            {
                entity.Property(e => e.Ceo)
                    .HasMaxLength(300)
                    .HasColumnName("CEO");

                entity.Property(e => e.Employee).HasDefaultValueSql("((0))");

                entity.Property(e => e.InstrumentType)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.IssVolatile).HasDefaultValueSql("((0))");

                entity.Property(e => e.KidsRecommended).HasDefaultValueSql("((0))");

                entity.Property(e => e.MarketId).HasColumnName("MarketID");

                entity.Property(e => e.MarketPrice).HasColumnType("money");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Peratio)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("PERatio");

                entity.Property(e => e.RiskRating).HasDefaultValueSql("((0))");

                entity.Property(e => e.Shid).HasColumnName("SHID");

                entity.Property(e => e.Symbol)
                    .IsRequired()
                    .HasMaxLength(6);

                entity.Property(e => e.UpdatedOn).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.WebsiteUrl)
                    .HasMaxLength(400)
                    .IsUnicode(false);

                entity.HasOne(d => d.Market)
                    .WithMany(p => p.Instruments)
                    .HasForeignKey(d => d.MarketId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Instrumen__Marke__5070F446");
            });

            modelBuilder.Entity<Market>(entity =>
            {
                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Country).HasMaxLength(100);

                entity.Property(e => e.Currency)
                    .IsRequired()
                    .HasMaxLength(5);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<PriceHistory>(entity =>
            {
                entity.ToTable("PriceHistory");

                entity.Property(e => e.Price).HasColumnType("money");

                entity.Property(e => e.RecordedOn).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Instrument)
                    .WithMany(p => p.PriceHistories)
                    .HasForeignKey(d => d.InstrumentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PriceHist__Instr__5EBF139D");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
