﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Sheepshead.Logic.Models;
using System;

namespace Sheepshead.Logic.Migrations
{
    [DbContext(typeof(SheepsheadContext))]
    [Migration("20190116233359_ReplaceCoinsPointsWithScores")]
    partial class ReplaceCoinsPointsWithScores
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Sheepshead.Logic.Models.Game", b =>
                {
                    b.Property<Guid>("Id");

                    b.Property<DateTime?>("CreationTime");

                    b.Property<DateTime?>("LastModifiedTime");

                    b.Property<bool>("LeastersEnabled");

                    b.Property<string>("PartnerMethod")
                        .IsRequired()
                        .HasColumnType("char(1)");

                    b.HasKey("Id");

                    b.ToTable("Game");
                });

            modelBuilder.Entity("Sheepshead.Logic.Models.Hand", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BlindCards")
                        .IsRequired()
                        .HasColumnType("nchar(6)");

                    b.Property<string>("BuriedCards")
                        .IsRequired()
                        .HasColumnType("nchar(6)");

                    b.Property<Guid>("GameId");

                    b.Property<string>("PartnerCard")
                        .HasColumnType("nchar(2)");

                    b.Property<int?>("PartnerParticipantId");

                    b.Property<bool>("PickPhaseComplete");

                    b.Property<int?>("PickerParticipantId");

                    b.Property<int>("SortOrder");

                    b.Property<int>("StartingParticipantId");

                    b.HasKey("Id");

                    b.HasIndex("GameId")
                        .HasName("IX_FK_Hand_Game");

                    b.HasIndex("PartnerParticipantId")
                        .HasName("IX_FK_Hand_Partner");

                    b.HasIndex("PickerParticipantId")
                        .HasName("IX_FK_Hand_Picker");

                    b.HasIndex("StartingParticipantId")
                        .HasName("IX_FK_Hand_StartingParticipant");

                    b.ToTable("Hand");
                });

            modelBuilder.Entity("Sheepshead.Logic.Models.Participant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("AssignedToClient");

                    b.Property<string>("Cards")
                        .HasColumnType("nchar(35)");

                    b.Property<Guid>("GameId");

                    b.Property<Guid>("Guid");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)")
                        .IsUnicode(false);

                    b.Property<int>("SortOrder");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("char(1)");

                    b.HasKey("Id");

                    b.HasIndex("GameId")
                        .HasName("IX_FK_Player_Game");

                    b.ToTable("Participant");
                });

            modelBuilder.Entity("Sheepshead.Logic.Models.ParticipantRefusingPick", b =>
                {
                    b.Property<int>("HandId");

                    b.Property<int>("ParticipantId");

                    b.HasKey("HandId", "ParticipantId");

                    b.HasIndex("ParticipantId");

                    b.ToTable("ParticipantRefusingPick");
                });

            modelBuilder.Entity("Sheepshead.Logic.Models.Score", b =>
                {
                    b.Property<int>("HandId");

                    b.Property<int>("ParticipantId");

                    b.Property<int>("Coins");

                    b.Property<int>("Points");

                    b.HasKey("HandId", "ParticipantId");

                    b.HasIndex("HandId")
                        .HasName("IX_FK_Score_Hand");

                    b.HasIndex("ParticipantId")
                        .HasName("IX_FK_Score_Participant");

                    b.ToTable("Score");
                });

            modelBuilder.Entity("Sheepshead.Logic.Models.Trick", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("HandId");

                    b.Property<int>("SortOrder");

                    b.Property<int>("StartingParticipantId");

                    b.HasKey("Id");

                    b.HasIndex("HandId")
                        .HasName("IX_FK_Trick_Hand");

                    b.HasIndex("StartingParticipantId")
                        .HasName("IX_FK_Trick_Participant");

                    b.ToTable("Trick");
                });

            modelBuilder.Entity("Sheepshead.Logic.Models.TrickPlay", b =>
                {
                    b.Property<int>("ParticipantId");

                    b.Property<int>("TrickId");

                    b.Property<string>("Card")
                        .IsRequired()
                        .HasColumnType("nchar(2)");

                    b.Property<int>("SortOrder");

                    b.HasKey("ParticipantId", "TrickId");

                    b.HasIndex("TrickId")
                        .HasName("IX_FK_TrickPlay_Trick");

                    b.ToTable("TrickPlay");
                });

            modelBuilder.Entity("Sheepshead.Logic.Models.Hand", b =>
                {
                    b.HasOne("Sheepshead.Logic.Models.Game", "Game")
                        .WithMany("Hands")
                        .HasForeignKey("GameId")
                        .HasConstraintName("FK_Hand_Game")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Sheepshead.Logic.Models.Participant", "PartnerParticipant")
                        .WithMany("HandsAsPartner")
                        .HasForeignKey("PartnerParticipantId")
                        .HasConstraintName("FK_Hand_Partner");

                    b.HasOne("Sheepshead.Logic.Models.Participant", "PickerParticipant")
                        .WithMany("HandsAsPicker")
                        .HasForeignKey("PickerParticipantId")
                        .HasConstraintName("FK_Hand_Picker");

                    b.HasOne("Sheepshead.Logic.Models.Participant", "StartingParticipant")
                        .WithMany("HandsStarted")
                        .HasForeignKey("StartingParticipantId")
                        .HasConstraintName("FK_Hand_StartingParticipant");
                });

            modelBuilder.Entity("Sheepshead.Logic.Models.Participant", b =>
                {
                    b.HasOne("Sheepshead.Logic.Models.Game", "Game")
                        .WithMany("Participants")
                        .HasForeignKey("GameId")
                        .HasConstraintName("FK_Player_Game")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Sheepshead.Logic.Models.ParticipantRefusingPick", b =>
                {
                    b.HasOne("Sheepshead.Logic.Models.Hand", "Hand")
                        .WithMany("ParticipantsRefusingPick")
                        .HasForeignKey("HandId")
                        .HasConstraintName("FK_ParticipantRefusingPick_Hand");

                    b.HasOne("Sheepshead.Logic.Models.Participant", "Participant")
                        .WithMany("PicksRefused")
                        .HasForeignKey("ParticipantId")
                        .HasConstraintName("FK_ParticipantRefusingPick_Participant");
                });

            modelBuilder.Entity("Sheepshead.Logic.Models.Score", b =>
                {
                    b.HasOne("Sheepshead.Logic.Models.Hand", "Hand")
                        .WithMany("ScoreList")
                        .HasForeignKey("HandId")
                        .HasConstraintName("FK_Score_Hand");

                    b.HasOne("Sheepshead.Logic.Models.Participant", "Participant")
                        .WithMany("Scores")
                        .HasForeignKey("ParticipantId")
                        .HasConstraintName("FK_Score_Participant");
                });

            modelBuilder.Entity("Sheepshead.Logic.Models.Trick", b =>
                {
                    b.HasOne("Sheepshead.Logic.Models.Hand", "Hand")
                        .WithMany("Tricks")
                        .HasForeignKey("HandId")
                        .HasConstraintName("FK_Trick_Hand");

                    b.HasOne("Sheepshead.Logic.Models.Participant", "StartingParticipant")
                        .WithMany("Tricks")
                        .HasForeignKey("StartingParticipantId")
                        .HasConstraintName("FK_Trick_Participant");
                });

            modelBuilder.Entity("Sheepshead.Logic.Models.TrickPlay", b =>
                {
                    b.HasOne("Sheepshead.Logic.Models.Participant", "Participant")
                        .WithMany("TrickPlays")
                        .HasForeignKey("ParticipantId")
                        .HasConstraintName("FK_TrickPlay_Participant");

                    b.HasOne("Sheepshead.Logic.Models.Trick", "Trick")
                        .WithMany("TrickPlays")
                        .HasForeignKey("TrickId")
                        .HasConstraintName("FK_TrickPlay_Trick");
                });
#pragma warning restore 612, 618
        }
    }
}
