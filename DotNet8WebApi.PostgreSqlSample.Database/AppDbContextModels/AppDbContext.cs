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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Database=postgres;Username=postgres;Password=sasa@123");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(e => e.AuthorId).HasName("authors_pkey");

            entity.ToTable("authors");

            entity.Property(e => e.AuthorId).HasColumnName("author_id");
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
            entity.HasKey(e => e.PostId).HasName("blogposts_pkey");

            entity.ToTable("blogposts");

            entity.Property(e => e.PostId).HasColumnName("post_id");
            entity.Property(e => e.AuthorId).HasColumnName("author_id");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.Content).HasColumnName("content");
            entity.Property(e => e.IsPublished).HasColumnName("is_published");
            entity.Property(e => e.LastUpdatedDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("last_updated_date");
            entity.Property(e => e.PublishedDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("published_date");
            entity.Property(e => e.Tags)
                .HasMaxLength(255)
                .HasColumnName("tags");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");

            entity.HasOne(d => d.Author).WithMany(p => p.Blogposts)
                .HasForeignKey(d => d.AuthorId)
                .HasConstraintName("blogposts_author_id_fkey");

            entity.HasOne(d => d.Category).WithMany(p => p.Blogposts)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("blogposts_category_id_fkey");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("categories_pkey");

            entity.ToTable("categories");

            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(255)
                .HasColumnName("category_name");
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.CommentId).HasName("comments_pkey");

            entity.ToTable("comments");

            entity.Property(e => e.CommentId).HasColumnName("comment_id");
            entity.Property(e => e.CommentDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("comment_date");
            entity.Property(e => e.CommentText).HasColumnName("comment_text");
            entity.Property(e => e.CommenterEmail)
                .HasMaxLength(255)
                .HasColumnName("commenter_email");
            entity.Property(e => e.CommenterName)
                .HasMaxLength(255)
                .HasColumnName("commenter_name");
            entity.Property(e => e.PostId).HasColumnName("post_id");

            entity.HasOne(d => d.Post).WithMany(p => p.Comments)
                .HasForeignKey(d => d.PostId)
                .HasConstraintName("comments_post_id_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
