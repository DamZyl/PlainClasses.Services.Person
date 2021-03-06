// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PlainClasses.Services.Person.Infrastructure.Sql;

namespace PlainClasses.Services.Person.Api.Migrations
{
    [DbContext(typeof(PersonContext))]
    [Migration("20210427161818_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PlainClasses.Services.Person.Domain.Models.MilitaryRank", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Acronym")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("MilitaryRanks");
                });

            modelBuilder.Entity("PlainClasses.Services.Person.Domain.Models.Person", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("FatherName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MilitaryRankAcr")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("MilitaryRankId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PersonalNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PersonalPhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("PlatoonId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Position")
                        .HasColumnType("int");

                    b.Property<string>("WorkPhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("MilitaryRankId");

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("PlainClasses.Services.Person.Domain.Models.PersonAuth", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AuthName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("PersonId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("PersonId");

                    b.ToTable("PersonAuths");
                });

            modelBuilder.Entity("PlainClasses.Services.Person.Domain.Models.Person", b =>
                {
                    b.HasOne("PlainClasses.Services.Person.Domain.Models.MilitaryRank", "MilitaryRank")
                        .WithMany("Persons")
                        .HasForeignKey("MilitaryRankId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PlainClasses.Services.Person.Domain.Models.PersonAuth", b =>
                {
                    b.HasOne("PlainClasses.Services.Person.Domain.Models.Person", "Person")
                        .WithMany("PersonAuths")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
