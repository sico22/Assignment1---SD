using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Assignment1.DAL.Models;

namespace Assignment1.DAL.DataContext;

public partial class Assignment1Context : DbContext
{
    public Assignment1Context()
    {
    }

    public Assignment1Context(DbContextOptions<Assignment1Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Admin> Admins { get; set; }

    public virtual DbSet<Cashier> Cashiers { get; set; }

    public virtual DbSet<Performance> Performances { get; set; }

    public virtual DbSet<Ticket> Tickets { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    { }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Admin>(entity =>
        {
            entity.HasKey(e => e.AdminId).HasName("PK__Admins__43AA414177EE4145");

            entity.Property(e => e.AdminId)
                .ValueGeneratedNever()
                .HasColumnName("admin_id");
            entity.Property(e => e.Password)
                .HasMaxLength(512)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Username)
                .HasMaxLength(512)
                .IsUnicode(false)
                .HasColumnName("username");
        });

        modelBuilder.Entity<Cashier>(entity =>
        {
            entity.HasKey(e => e.CashierId).HasName("PK__Cashiers__B366F9FAF721825B");

            entity.Property(e => e.CashierId)
                .ValueGeneratedNever()
                .HasColumnName("cashier_id");
            entity.Property(e => e.Password)
                .HasMaxLength(512)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Username)
                .HasMaxLength(512)
                .IsUnicode(false)
                .HasColumnName("username");
        });

        modelBuilder.Entity<Performance>(entity =>
        {
            entity.HasKey(e => e.PerformanceId).HasName("PK__Performa__8C2C0F60797BB487");

            entity.Property(e => e.PerformanceId).HasColumnName("performance_id");
            entity.Property(e => e.Artist)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("artist");
            entity.Property(e => e.Date)
                .HasColumnType("datetime")
                .HasColumnName("date");
            entity.Property(e => e.Genre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("genre");
            entity.Property(e => e.NrOfTickets).HasColumnName("nr_of_tickets");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("title");
        });

        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(e => e.TicketId).HasName("PK__Tickets__D596F96B6F97D428");

            entity.Property(e => e.TicketId).HasColumnName("ticket_id");
            entity.Property(e => e.NrOfPlaces).HasColumnName("nr_of_places");
            entity.Property(e => e.PerformanceId).HasColumnName("performance_id");

            entity.HasOne(d => d.Performance).WithMany(p => p.Tickets)
                .HasForeignKey(d => d.PerformanceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tickets__perform__6E01572D");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
