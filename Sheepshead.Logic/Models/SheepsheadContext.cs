﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Sheepshead.Logic.Models
{
    public partial class SheepsheadContext : DbContext
    {
        public virtual DbSet<Game> Game { get; set; }
        public virtual DbSet<Hand> Hand { get; set; }
        public virtual DbSet<Participant> Participant { get; set; }
        public virtual DbSet<ParticipantRefusingPick> ParticipantRefusingPick { get; set; }
        public virtual DbSet<Trick> Trick { get; set; }
        public virtual DbSet<TrickPlay> TrickPlay { get; set; }
        public virtual DbSet<Score> Score { get; set; }

        public SheepsheadContext(DbContextOptions<SheepsheadContext> dbContextOptions) : base(dbContextOptions)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Score>(entity =>
            {
                entity.HasKey(e => new { e.HandId, e.ParticipantId });

                entity.HasIndex(e => e.HandId)
                    .HasName("IX_FK_Score_Hand");

                entity.HasIndex(e => e.ParticipantId)
                    .HasName("IX_FK_Score_Participant");

                entity.HasOne(d => d.Hand)
                    .WithMany(p => p.ScoreList)
                    .HasForeignKey(d => d.HandId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Score_Hand");

                entity.HasOne(d => d.Participant)
                    .WithMany(p => p.Scores)
                    .HasForeignKey(d => d.ParticipantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Score_Participant");
            });

            modelBuilder.Entity<Game>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.PartnerMethod)
                    .IsRequired()
                    .HasColumnType("char(1)");
            });

            modelBuilder.Entity<Hand>(entity =>
            {
                entity.HasIndex(e => e.GameId)
                    .HasName("IX_FK_Hand_Game");

                entity.HasIndex(e => e.PartnerParticipantId)
                    .HasName("IX_FK_Hand_Partner");

                entity.HasIndex(e => e.PickerParticipantId)
                    .HasName("IX_FK_Hand_Picker");

                entity.HasIndex(e => e.StartingParticipantId)
                    .HasName("IX_FK_Hand_StartingParticipant");

                entity.Property(e => e.BlindCards)
                    .IsRequired()
                    .HasColumnType("nchar(6)");

                entity.Property(e => e.BuriedCards)
                    .IsRequired()
                    .HasColumnType("nchar(6)");

                entity.Property(e => e.PartnerCard).HasColumnType("nchar(2)");

                entity.HasOne(d => d.Game)
                    .WithMany(p => p.Hands)
                    .HasForeignKey(d => d.GameId)
                    .HasConstraintName("FK_Hand_Game");

                entity.HasOne(d => d.PartnerParticipant)
                    .WithMany(p => p.HandsAsPartner)
                    .HasForeignKey(d => d.PartnerParticipantId)
                    .HasConstraintName("FK_Hand_Partner");

                entity.HasOne(d => d.PickerParticipant)
                    .WithMany(p => p.HandsAsPicker)
                    .HasForeignKey(d => d.PickerParticipantId)
                    .HasConstraintName("FK_Hand_Picker");

                entity.HasOne(d => d.StartingParticipant)
                    .WithMany(p => p.HandsStarted)
                    .HasForeignKey(d => d.StartingParticipantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Hand_StartingParticipant");
            });

            modelBuilder.Entity<Participant>(entity =>
            {
                entity.HasIndex(e => e.GameId)
                    .HasName("IX_FK_Player_Game");

                entity.Property(e => e.Cards)
                    .HasColumnType("nchar(35)");

                entity.Property(e => e.Name)
                    .HasColumnType("nvarchar(max)")
                    .IsUnicode(false);

                entity.Property(e => e.Type)
                    .HasColumnType("char(1)")
                    .IsRequired();

                entity.HasOne(d => d.Game)
                    .WithMany(p => p.Participants)
                    .HasForeignKey(d => d.GameId)
                    .HasConstraintName("FK_Player_Game");
            });

            modelBuilder.Entity<ParticipantRefusingPick>(entity =>
            {
                entity.HasKey(e => new { e.HandId, e.ParticipantId });

                entity.HasOne(d => d.Hand)
                    .WithMany(p => p.ParticipantsRefusingPick)
                    .HasForeignKey(d => d.HandId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ParticipantRefusingPick_Hand");

                entity.HasOne(d => d.Participant)
                    .WithMany(p => p.PicksRefused)
                    .HasForeignKey(d => d.ParticipantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ParticipantRefusingPick_Participant");
            });

            modelBuilder.Entity<Trick>(entity =>
            {
                entity.HasIndex(e => e.HandId)
                    .HasName("IX_FK_Trick_Hand");

                entity.HasIndex(e => e.StartingParticipantId)
                    .HasName("IX_FK_Trick_Participant");

                entity.HasOne(d => d.Hand)
                    .WithMany(p => p.Tricks)
                    .HasForeignKey(d => d.HandId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Trick_Hand");

                entity.HasOne(d => d.StartingParticipant)
                    .WithMany(p => p.Tricks)
                    .HasForeignKey(d => d.StartingParticipantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Trick_Participant");
            });

            modelBuilder.Entity<TrickPlay>(entity =>
            {
                entity.HasKey(e => new { e.ParticipantId, e.TrickId });

                entity.HasIndex(e => e.TrickId)
                    .HasName("IX_FK_TrickPlay_Trick");

                entity.Property(e => e.Card)
                    .IsRequired()
                    .HasColumnType("nchar(2)");

                entity.HasOne(d => d.Participant)
                    .WithMany(p => p.TrickPlays)
                    .HasForeignKey(d => d.ParticipantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TrickPlay_Participant");

                entity.HasOne(d => d.Trick)
                    .WithMany(p => p.TrickPlays)
                    .HasForeignKey(d => d.TrickId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TrickPlay_Trick");
            });
        }
    }
}
