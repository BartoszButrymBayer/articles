﻿// <auto-generated />
using System;
using ArticlesApiPostgres.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ArticlesApiPostgres.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20241120223008_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ArticlesApiPostgres.Models.Article", b =>
                {
                    b.Property<int>("ArticleID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ArticleID"));

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FullContent")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("PublishedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("ShortDescription")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ArticleID");

                    b.ToTable("Articles");
                });

            modelBuilder.Entity("ArticlesApiPostgres.Models.ArticleTag", b =>
                {
                    b.Property<int>("ArticleID")
                        .HasColumnType("integer");

                    b.Property<int>("TagID")
                        .HasColumnType("integer");

                    b.HasKey("ArticleID", "TagID");

                    b.HasIndex("TagID");

                    b.ToTable("ArticleTags");
                });

            modelBuilder.Entity("ArticlesApiPostgres.Models.Tag", b =>
                {
                    b.Property<int>("TagID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("TagID"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("TagID");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("ArticlesApiPostgres.Models.ArticleTag", b =>
                {
                    b.HasOne("ArticlesApiPostgres.Models.Article", "Article")
                        .WithMany("ArticleTags")
                        .HasForeignKey("ArticleID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ArticlesApiPostgres.Models.Tag", "Tag")
                        .WithMany("ArticleTags")
                        .HasForeignKey("TagID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Article");

                    b.Navigation("Tag");
                });

            modelBuilder.Entity("ArticlesApiPostgres.Models.Article", b =>
                {
                    b.Navigation("ArticleTags");
                });

            modelBuilder.Entity("ArticlesApiPostgres.Models.Tag", b =>
                {
                    b.Navigation("ArticleTags");
                });
#pragma warning restore 612, 618
        }
    }
}
