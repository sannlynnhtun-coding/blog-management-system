# DotNet8WebApi.PostgreSqlSample

## Overview

The BlogAPI is a RESTful Web API developed using ASP.NET Core, Entity Framework Core, and PostgreSQL. It serves as a platform for managing authors, categories, blog posts, and comments, supporting essential CRUD (Create, Read, Update, Delete) operations for all associated entities. This comprehensive API allows for efficient blog management.

## Features

- **Authors Management**: Create, read, update, and delete author information.
- **Categories Management**: Organize blog posts by creating and managing categories.
- **Blog Posts Management**: Handle blog post creation and management, including assigning authors and categories, setting publish dates, and more.
- **Comments Management**: Manage comments on blog posts, including comment creation, retrieval, and deletion.

## Technologies Used

- **ASP.NET Core**: Framework for building the Web API.
- **Entity Framework Core**: ORM (Object-Relational Mapping) to interact with the PostgreSQL database.
- **PostgreSQL**: Database system.

## Database Schema

The database schema includes the following tables:

1. **Authors**
    - `AuthorID`: Primary key.
    - `Name`: Author's name.
    - `Email`: Author's email.
    - `Bio`: Short biography of the author.

2. **Categories**
    - `CategoryID`: Primary key.
    - `CategoryName`: Name of the category.

3. **BlogPosts**
    - `PostID`: Primary key.
    - `Title`: Title of the blog post.
    - `Content`: Content of the blog post.
    - `AuthorID`: Foreign key referencing `Authors`.
    - `CategoryID`: Foreign key referencing `Categories`.
    - `PublishedDate`: Date the post was published.
    - `LastUpdatedDate`: Date the post was last updated.
    - `IsPublished`: Boolean flag indicating if the post is published.
    - `Tags`: Tags associated with the post.

4. **Comments**
    - `CommentID`: Primary key.
    - `PostID`: Foreign key referencing `BlogPosts`.
    - `CommenterName`: Name of the commenter.
    - `CommenterEmail`: Email of the commenter.
    - `CommentText`: Text of the comment.
    - `CommentDate`: Date the comment was made.

## API Endpoints

The API provides the following endpoints:

- **Authors**
    - `GET /api/authors`
    - `GET /api/authors/{id}`
    - `POST /api/authors`
    - `PUT /api/authors/{id}`
    - `DELETE /api/authors/{id}`

- **Categories**
    - `GET /api/categories`
    - `GET /api/categories/{id}`
    - `POST /api/categories`
    - `PUT /api/categories/{id}`
    - `DELETE /api/categories/{id}`

- **BlogPosts**
    - `GET /api/blogposts`
    - `GET /api/blogposts/{id}`
    - `POST /api/blogposts`
    - `PUT /api/blogposts/{id}`
    - `DELETE /api/blogposts/{id}`

- **Comments**
    - `GET /api/comments`
    - `GET /api/comments/{id}`
    - `POST /api/comments`
    - `PUT /api/comments/{id}`
    - `DELETE /api/comments/{id}`

---

### Table Script

```sql
CREATE TABLE Authors (
                         author_id SERIAL PRIMARY KEY,
                         name VARCHAR(255) NOT NULL,
                         email VARCHAR(255) NOT NULL,
                         bio TEXT
);

CREATE TABLE Categories (
                            category_id SERIAL PRIMARY KEY,
                            category_name VARCHAR(255) NOT NULL
);

CREATE TABLE Blog_Posts (
                            post_id SERIAL PRIMARY KEY,
                            title VARCHAR(255) NOT NULL,
                            content TEXT NOT NULL,
                            author_id INT,
                            category_id INT,
                            published_date TIMESTAMP,
                            last_updated_date TIMESTAMP,
                            is_published BOOLEAN,
                            tags VARCHAR(255),
                            FOREIGN KEY (author_id) REFERENCES Authors(author_id),
                            FOREIGN KEY (category_id) REFERENCES Categories(category_id)
);

CREATE TABLE Comments (
                          comment_id SERIAL PRIMARY KEY,
                          post_id INT,
                          commenter_name VARCHAR(255),
                          commenter_email VARCHAR(255),
                          comment_text TEXT,
                          comment_date TIMESTAMP,
                          FOREIGN KEY (post_id) REFERENCES Blog_Posts(post_id)
);
```

### Insert Script

```sql
-- Truncate tables
TRUNCATE TABLE Comments, BlogPosts, Authors, Categories RESTART IDENTITY CASCADE;

-- Insert Records into Authors
DO $$
BEGIN
    FOR i IN 1..100 LOOP
        INSERT INTO Authors (Name, Email, Bio)
        VALUES (
            'Author ' || i,
            'author' || i || '@example.com',
            'This is a bio for Author ' || i || '. They are a prolific writer with numerous published works.'
        );
    END LOOP;
END $$;

-- Insert Records into Categories
DO $$
BEGIN
    FOR i IN 1..100 LOOP
        INSERT INTO Categories (CategoryName)
        VALUES ('Category ' || i);
    END LOOP;
END $$;

-- Insert Records into BlogPosts
DO $$
BEGIN
    FOR i IN 1..100 LOOP
        INSERT INTO BlogPosts (Title, Content, AuthorID, CategoryID, PublishedDate, LastUpdatedDate, IsPublished, Tags)
        VALUES (
            'Interesting Blog Post ' || i,
            'This is the content for blog post ' || i || '. It covers a variety of interesting topics related to Category ' || (i % 100) + 1 || '.',
            (i % 100) + 1, -- Valid AuthorID
            (i % 100) + 1, -- Valid CategoryID
            NOW() - ((i % 30) || ' days')::interval, -- Random PublishedDate within the last 30 days
            NOW() - ((i % 15) || ' days')::interval, -- Random LastUpdatedDate within the last 15 days
            (i % 2 = 0), -- Random IsPublished
            'tag' || (i % 10) -- Random Tag within 10 different tags
        );
    END LOOP;
END $$;

-- Insert Records into Comments
DO $$
DECLARE
    post_count INT;
BEGIN
    SELECT COUNT(*) INTO post_count FROM BlogPosts;

    FOR i IN 1..100 LOOP
        INSERT INTO Comments (PostID, CommenterName, CommenterEmail, CommentText, CommentDate)
        VALUES (
            (i % post_count) + 1, -- Valid PostID
            'Commenter ' || i,
            'commenter' || i || '@example.com',
            'This is a comment text for comment ' || i || '. It provides feedback and insights on Blog Post ' || (i % post_count) + 1 || '.',
            NOW() - ((i % 10) || ' days')::interval -- Random CommentDate within the last 10 days
        );
    END LOOP;
END $$;
```


To scaffold the database context, use the following command:

```
dotnet ef dbcontext scaffold "Host=localhost;Database=postgres;Username=postgres;Password=sasa@123" Npgsql.EntityFrameworkCore.PostgreSQL -o AppDbContextModels -c AppDbContext -f
```
