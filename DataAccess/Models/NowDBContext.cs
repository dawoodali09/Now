using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace SQLDataAccess.Models
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

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<ClosingDay> ClosingDays { get; set; }
        public virtual DbSet<Currency> Currencies { get; set; }
        public virtual DbSet<Instrument> Instruments { get; set; }
        public virtual DbSet<InstrumentCategory> InstrumentCategories { get; set; }
        public virtual DbSet<Market> Markets { get; set; }
        public virtual DbSet<PriceHistory> PriceHistories { get; set; }
        public virtual DbSet<Rule> Rules { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(400)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<ClosingDay>(entity =>
            {
                entity.Property(e => e.ClosingDate).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Reason)
                    .IsRequired()
                    .HasMaxLength(500)
                    .HasDefaultValueSql("('Weekend')");

                entity.HasOne(d => d.Market)
                    .WithMany(p => p.ClosingDays)
                    .HasForeignKey(d => d.MarketId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ClosingDa__Marke__0E6E26BF");
            });

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

                entity.Property(e => e.FiveYearMaximum).HasColumnType("decimal(10, 4)");

                entity.Property(e => e.FiveYearMinimum).HasColumnType("decimal(10, 4)");

                entity.Property(e => e.FiveYearPercentage).HasColumnType("decimal(10, 4)");

                entity.Property(e => e.FiveYearValue).HasColumnType("decimal(10, 4)");

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

                entity.Property(e => e.OneDayMaximum).HasColumnType("decimal(10, 4)");

                entity.Property(e => e.OneDayMinimum).HasColumnType("decimal(10, 4)");

                entity.Property(e => e.OneDayPercentage).HasColumnType("decimal(10, 4)");

                entity.Property(e => e.OneDayValue).HasColumnType("decimal(10, 4)");

                entity.Property(e => e.OneMonthMaximum).HasColumnType("decimal(10, 4)");

                entity.Property(e => e.OneMonthMinimum).HasColumnType("decimal(10, 4)");

                entity.Property(e => e.OneMonthPercentage).HasColumnType("decimal(10, 4)");

                entity.Property(e => e.OneMonthValue).HasColumnType("decimal(10, 4)");

                entity.Property(e => e.OneWeekMaximum).HasColumnType("decimal(10, 4)");

                entity.Property(e => e.OneWeekMinimum).HasColumnType("decimal(10, 4)");

                entity.Property(e => e.OneWeekPercentage).HasColumnType("decimal(10, 4)");

                entity.Property(e => e.OneWeekValue).HasColumnType("decimal(10, 4)");

                entity.Property(e => e.OneYearMaximum).HasColumnType("decimal(10, 4)");

                entity.Property(e => e.OneYearMinimum).HasColumnType("decimal(10, 4)");

                entity.Property(e => e.OneYearPercentage).HasColumnType("decimal(10, 4)");

                entity.Property(e => e.OneYearValue).HasColumnType("decimal(10, 4)");

                entity.Property(e => e.Peratio)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("PERatio");

                entity.Property(e => e.RiskRating).HasDefaultValueSql("((0))");

                entity.Property(e => e.Shid).HasColumnName("SHID");

                entity.Property(e => e.SixMonthMaximum).HasColumnType("decimal(10, 4)");

                entity.Property(e => e.SixMonthMinimum).HasColumnType("decimal(10, 4)");

                entity.Property(e => e.SixMonthPercentage).HasColumnType("decimal(10, 4)");

                entity.Property(e => e.SixMonthValue).HasColumnType("decimal(10, 4)");

                entity.Property(e => e.Symbol)
                    .IsRequired()
                    .HasMaxLength(6);

                entity.Property(e => e.ThreeMonthMaximum).HasColumnType("decimal(10, 4)");

                entity.Property(e => e.ThreeMonthMinimum).HasColumnType("decimal(10, 4)");

                entity.Property(e => e.ThreeMonthPercentage).HasColumnType("decimal(10, 4)");

                entity.Property(e => e.ThreeMonthValue).HasColumnType("decimal(10, 4)");

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

            modelBuilder.Entity<InstrumentCategory>(entity =>
            {
                entity.HasOne(d => d.Category)
                    .WithMany(p => p.InstrumentCategories)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Instrumen__Categ__531856C7");

                entity.HasOne(d => d.Instrument)
                    .WithMany(p => p.InstrumentCategories)
                    .HasForeignKey(d => d.InstrumentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Instrumen__Instr__5224328E");
            });

            modelBuilder.Entity<Market>(entity =>
            {
                entity.Property(e => e.ClosingTimeNz).HasColumnName("ClosingTimeNZ");

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

                entity.Property(e => e.OpeningTimeNz).HasColumnName("OpeningTimeNZ");
            });

            modelBuilder.Entity<PriceHistory>(entity =>
            {
                entity.ToTable("PriceHistory");

                entity.HasIndex(e => e.InstrumentId, "NonClusteredIndex-PriceHistory-InstrumentId");

                entity.Property(e => e.Price).HasColumnType("money");

                entity.Property(e => e.RecordedOn).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Instrument)
                    .WithMany(p => p.PriceHistories)
                    .HasForeignKey(d => d.InstrumentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PriceHist__Instr__5EBF139D");
            });

            modelBuilder.Entity<Rule>(entity =>
            {
                entity.Property(e => e.Description).HasMaxLength(800);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Uuid)
                    .HasColumnName("UUID")
                    .HasDefaultValueSql("(newid())");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
