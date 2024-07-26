using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DotNet8WebApi.PostgreSqlSample.Database.AppDbContextModels;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Blogpost> Blogposts { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(e => e.Authorid).HasName("authors_pkey");

            entity.ToTable("authors");

            entity.Property(e => e.Authorid).HasColumnName("authorid");
            entity.Property(e => e.Bio).HasColumnName("bio");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Blogpost>(entity =>
        {
            entity.HasKey(e => e.Postid).HasName("blogposts_pkey");

            entity.ToTable("blogposts");

            entity.Property(e => e.Postid).HasColumnName("postid");
            entity.Property(e => e.Authorid).HasColumnName("authorid");
            entity.Property(e => e.Categoryid).HasColumnName("categoryid");
            entity.Property(e => e.Content).HasColumnName("content");
            entity.Property(e => e.Ispublished).HasColumnName("ispublished");
            entity.Property(e => e.Lastupdateddate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("lastupdateddate");
            entity.Property(e => e.Publisheddate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("publisheddate");
            entity.Property(e => e.Tags)
                .HasMaxLength(255)
                .HasColumnName("tags");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");

            entity.HasOne(d => d.Author).WithMany(p => p.Blogposts)
                .HasForeignKey(d => d.Authorid)
                .HasConstraintName("blogposts_authorid_fkey");

            entity.HasOne(d => d.Category).WithMany(p => p.Blogposts)
                .HasForeignKey(d => d.Categoryid)
                .HasConstraintName("blogposts_categoryid_fkey");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Categoryid).HasName("categories_pkey");

            entity.ToTable("categories");

            entity.Property(e => e.Categoryid).HasColumnName("categoryid");
            entity.Property(e => e.Categoryname)
                .HasMaxLength(255)
                .HasColumnName("categoryname");
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.Commentid).HasName("comments_pkey");

            entity.ToTable("comments");

            entity.Property(e => e.Commentid).HasColumnName("commentid");
            entity.Property(e => e.Commentdate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("commentdate");
            entity.Property(e => e.Commenteremail)
                .HasMaxLength(255)
                .HasColumnName("commenteremail");
            entity.Property(e => e.Commentername)
                .HasMaxLength(255)
                .HasColumnName("commentername");
            entity.Property(e => e.Commenttext).HasColumnName("commenttext");
            entity.Property(e => e.Postid).HasColumnName("postid");

            entity.HasOne(d => d.Post).WithMany(p => p.Comments)
                .HasForeignKey(d => d.Postid)
                .HasConstraintName("comments_postid_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
