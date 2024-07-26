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

    public virtual DbSet<BlogPost> BlogPosts { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

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

        modelBuilder.Entity<BlogPost>(entity =>
        {
            entity.HasKey(e => e.PostId).HasName("blog_posts_pkey");

            entity.ToTable("blog_posts");

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

            entity.HasOne(d => d.Author).WithMany(p => p.BlogPosts)
                .HasForeignKey(d => d.AuthorId)
                .HasConstraintName("blog_posts_author_id_fkey");

            entity.HasOne(d => d.Category).WithMany(p => p.BlogPosts)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("blog_posts_category_id_fkey");
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
