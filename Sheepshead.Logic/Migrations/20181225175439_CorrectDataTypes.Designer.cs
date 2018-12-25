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
    [Migration("20181225175439_CorrectDataTypes")]
    partial class CorrectDataTypes
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Sheepshead.Logic.Models.Coin", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Count");

                    b.Property<int>("HandId");

                    b.Property<int>("ParticipantId");

                    b.HasKey("Id");

                    b.HasIndex("HandId")
                        .HasName("IX_FK_Hand_Coin");

                    b.HasIndex("ParticipantId")
                        .HasName("IX_FK_Coin_Participant");

                    b.ToTable("Coin");
                });

            modelBuilder.Entity("Sheepshead.Logic.Models.Game", b =>
                {
                    b.Property<Guid>("Id");

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
                        .HasColumnType("char(6)");

                    b.Property<string>("BuriedCards")
                        .IsRequired()
                        .HasColumnType("char(6)");

                    b.Property<Guid>("GameId");

                    b.Property<string>("PartnerCard")
                        .HasColumnType("char(2)");

                    b.Property<int?>("PartnerParticipantId");

                    b.Property<int?>("PickerParticipantId");

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
                        .HasColumnType("char(35)");

                    b.Property<Guid>("GameId");

                    b.Property<Guid>("Guid");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)")
                        .IsUnicode(false);

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

            modelBuilder.Entity("Sheepshead.Logic.Models.Point", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("HandId");

                    b.Property<int>("ParticipantId");

                    b.Property<int>("Value");

                    b.HasKey("Id");

                    b.HasIndex("HandId");

                    b.HasIndex("ParticipantId");

                    b.ToTable("Point");
                });

            modelBuilder.Entity("Sheepshead.Logic.Models.Trick", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("HandId");

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
                        .HasColumnType("char(2)");

                    b.Property<int>("SortOrder");

                    b.HasKey("ParticipantId", "TrickId");

                    b.HasIndex("TrickId")
                        .HasName("IX_FK_TrickPlay_Trick");

                    b.ToTable("TrickPlay");
                });

            modelBuilder.Entity("Sheepshead.Logic.Models.Coin", b =>
                {
                    b.HasOne("Sheepshead.Logic.Models.Hand", "Hand")
                        .WithMany("Coin")
                        .HasForeignKey("HandId")
                        .HasConstraintName("FK_Hand_Coin");

                    b.HasOne("Sheepshead.Logic.Models.Participant", "Participant")
                        .WithMany("Coin")
                        .HasForeignKey("ParticipantId")
                        .HasConstraintName("FK_Coin_Participant");
                });

            modelBuilder.Entity("Sheepshead.Logic.Models.Hand", b =>
                {
                    b.HasOne("Sheepshead.Logic.Models.Game", "Game")
                        .WithMany("Hand")
                        .HasForeignKey("GameId")
                        .HasConstraintName("FK_Hand_Game")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Sheepshead.Logic.Models.Participant", "PartnerParticipant")
                        .WithMany("HandPartnerParticipant")
                        .HasForeignKey("PartnerParticipantId")
                        .HasConstraintName("FK_Hand_Partner");

                    b.HasOne("Sheepshead.Logic.Models.Participant", "PickerParticipant")
                        .WithMany("HandPickerParticipant")
                        .HasForeignKey("PickerParticipantId")
                        .HasConstraintName("FK_Hand_Picker");

                    b.HasOne("Sheepshead.Logic.Models.Participant", "StartingParticipant")
                        .WithMany("HandStartingParticipant")
                        .HasForeignKey("StartingParticipantId")
                        .HasConstraintName("FK_Hand_StartingParticipant");
                });

            modelBuilder.Entity("Sheepshead.Logic.Models.Participant", b =>
                {
                    b.HasOne("Sheepshead.Logic.Models.Game", "Game")
                        .WithMany("Participant")
                        .HasForeignKey("GameId")
                        .HasConstraintName("FK_Player_Game")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Sheepshead.Logic.Models.ParticipantRefusingPick", b =>
                {
                    b.HasOne("Sheepshead.Logic.Models.Hand", "Hand")
                        .WithMany("ParticipantRefusingPick")
                        .HasForeignKey("HandId")
                        .HasConstraintName("FK_ParticipantRefusingPick_Hand");

                    b.HasOne("Sheepshead.Logic.Models.Participant", "Participant")
                        .WithMany("ParticipantRefusingPick")
                        .HasForeignKey("ParticipantId")
                        .HasConstraintName("FK_ParticipantRefusingPick_Participant");
                });

            modelBuilder.Entity("Sheepshead.Logic.Models.Point", b =>
                {
                    b.HasOne("Sheepshead.Logic.Models.Hand", "Hand")
                        .WithMany("Point")
                        .HasForeignKey("HandId")
                        .HasConstraintName("FK_PointHand");

                    b.HasOne("Sheepshead.Logic.Models.Participant", "Participant")
                        .WithMany("Point")
                        .HasForeignKey("ParticipantId")
                        .HasConstraintName("FK_Point_Participant");
                });

            modelBuilder.Entity("Sheepshead.Logic.Models.Trick", b =>
                {
                    b.HasOne("Sheepshead.Logic.Models.Hand", "Hand")
                        .WithMany("Trick")
                        .HasForeignKey("HandId")
                        .HasConstraintName("FK_Trick_Hand");

                    b.HasOne("Sheepshead.Logic.Models.Participant", "Participant")
                        .WithMany("Trick")
                        .HasForeignKey("StartingParticipantId")
                        .HasConstraintName("FK_Trick_Participant");
                });

            modelBuilder.Entity("Sheepshead.Logic.Models.TrickPlay", b =>
                {
                    b.HasOne("Sheepshead.Logic.Models.Participant", "Participant")
                        .WithMany("TrickPlay")
                        .HasForeignKey("ParticipantId")
                        .HasConstraintName("FK_TrickPlay_Participant");

                    b.HasOne("Sheepshead.Logic.Models.Trick", "Trick")
                        .WithMany("TrickPlay")
                        .HasForeignKey("TrickId")
                        .HasConstraintName("FK_TrickPlay_Trick");
                });
#pragma warning restore 612, 618
        }
    }
}
