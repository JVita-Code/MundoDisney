﻿// <auto-generated />
using System;
using ApiMundoDisney.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ApiMundoDisney.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20220322141715_modeloInicial")]
    partial class modeloInicial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.15")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ApiMundoDisney.Entities.Genero", b =>
                {
                    b.Property<int>("GeneroId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RutaImagen")
                        .HasColumnType("int");

                    b.HasKey("GeneroId");

                    b.ToTable("Generos");
                });

            modelBuilder.Entity("ApiMundoDisney.Entities.Pelicula", b =>
                {
                    b.Property<int>("PeliculaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Calificacion")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("datetime2");

                    b.Property<int?>("GeneroId")
                        .HasColumnType("int");

                    b.Property<string>("RutaImagen")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Titulo")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PeliculaId");

                    b.HasIndex("GeneroId");

                    b.ToTable("Peliculas");
                });

            modelBuilder.Entity("ApiMundoDisney.Entities.Personaje", b =>
                {
                    b.Property<int>("PersonajeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Edad")
                        .HasColumnType("int");

                    b.Property<string>("Historia")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Peso")
                        .HasColumnType("int");

                    b.Property<string>("RutaImagen")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PersonajeId");

                    b.ToTable("Personajes");
                });

            modelBuilder.Entity("PeliculaPersonaje", b =>
                {
                    b.Property<int>("PeliculasPeliculaId")
                        .HasColumnType("int");

                    b.Property<int>("PersonajesPersonajeId")
                        .HasColumnType("int");

                    b.HasKey("PeliculasPeliculaId", "PersonajesPersonajeId");

                    b.HasIndex("PersonajesPersonajeId");

                    b.ToTable("PeliculaPersonaje");
                });

            modelBuilder.Entity("ApiMundoDisney.Entities.Pelicula", b =>
                {
                    b.HasOne("ApiMundoDisney.Entities.Genero", null)
                        .WithMany("Peliculas")
                        .HasForeignKey("GeneroId");
                });

            modelBuilder.Entity("PeliculaPersonaje", b =>
                {
                    b.HasOne("ApiMundoDisney.Entities.Pelicula", null)
                        .WithMany()
                        .HasForeignKey("PeliculasPeliculaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ApiMundoDisney.Entities.Personaje", null)
                        .WithMany()
                        .HasForeignKey("PersonajesPersonajeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ApiMundoDisney.Entities.Genero", b =>
                {
                    b.Navigation("Peliculas");
                });
#pragma warning restore 612, 618
        }
    }
}
