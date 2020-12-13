﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using OT.StateManagement.DataAccess.EF;

namespace OT.StateManagement.DataAccess.EF.Migrations
{
    [DbContext(typeof(StateContext))]
    partial class StateContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityByDefaultColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("OT.StateManagement.Domain.Entities.Flow", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValueSql("now()");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Flows");
                });

            modelBuilder.Entity("OT.StateManagement.Domain.Entities.State", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValueSql("now()");

                    b.Property<Guid>("FlowId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("NextStateId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("PreviousStateId")
                        .HasColumnType("uuid");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("FlowId");

                    b.HasIndex("PreviousStateId")
                        .IsUnique();

                    b.ToTable("States");
                });

            modelBuilder.Entity("OT.StateManagement.Domain.Entities.StateChange", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValueSql("now()");

                    b.Property<Guid>("TaskId")
                        .HasColumnType("uuid");

                    b.Property<int>("TypeOfChange")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("TaskId");

                    b.ToTable("StateChanges");
                });

            modelBuilder.Entity("OT.StateManagement.Domain.Entities.Task", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValueSql("now()");

                    b.Property<Guid>("StateId")
                        .HasColumnType("uuid");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("StateId");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("OT.StateManagement.Domain.Entities.State", b =>
                {
                    b.HasOne("OT.StateManagement.Domain.Entities.Flow", "Flow")
                        .WithMany("States")
                        .HasForeignKey("FlowId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OT.StateManagement.Domain.Entities.State", "PreviousState")
                        .WithOne("NextState")
                        .HasForeignKey("OT.StateManagement.Domain.Entities.State", "PreviousStateId");

                    b.Navigation("Flow");

                    b.Navigation("PreviousState");
                });

            modelBuilder.Entity("OT.StateManagement.Domain.Entities.StateChange", b =>
                {
                    b.HasOne("OT.StateManagement.Domain.Entities.Task", "Task")
                        .WithMany("StateChanges")
                        .HasForeignKey("TaskId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Task");
                });

            modelBuilder.Entity("OT.StateManagement.Domain.Entities.Task", b =>
                {
                    b.HasOne("OT.StateManagement.Domain.Entities.State", "State")
                        .WithMany("Tasks")
                        .HasForeignKey("StateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("State");
                });

            modelBuilder.Entity("OT.StateManagement.Domain.Entities.Flow", b =>
                {
                    b.Navigation("States");
                });

            modelBuilder.Entity("OT.StateManagement.Domain.Entities.State", b =>
                {
                    b.Navigation("NextState");

                    b.Navigation("Tasks");
                });

            modelBuilder.Entity("OT.StateManagement.Domain.Entities.Task", b =>
                {
                    b.Navigation("StateChanges");
                });
#pragma warning restore 612, 618
        }
    }
}
