# DotNet8WebApi.PostgreSqlSample\

```

dotnet ef dbcontext scaffold "Host=localhost;Database=postgres;Username=postgres;Password=sasa@123" Npgsql.EntityFrameworkCore.PostgreSQL -o Models -f

```

### Table Script

```sql

CREATE TABLE Authors (
    AuthorID SERIAL PRIMARY KEY,
    Name VARCHAR(255) NOT NULL,
    Email VARCHAR(255) NOT NULL,
    Bio TEXT
);

CREATE TABLE Categories (
    CategoryID SERIAL PRIMARY KEY,
    CategoryName VARCHAR(255) NOT NULL
);

CREATE TABLE BlogPosts (
    PostID SERIAL PRIMARY KEY,
    Title VARCHAR(255) NOT NULL,
    Content TEXT NOT NULL,
    AuthorID INT,
    CategoryID INT,
    PublishedDate TIMESTAMP,
    LastUpdatedDate TIMESTAMP,
    IsPublished BOOLEAN,
    Tags VARCHAR(255),
    FOREIGN KEY (AuthorID) REFERENCES Authors(AuthorID),
    FOREIGN KEY (CategoryID) REFERENCES Categories(CategoryID)
);

CREATE TABLE Comments (
    CommentID SERIAL PRIMARY KEY,
    PostID INT,
    CommenterName VARCHAR(255),
    CommenterEmail VARCHAR(255),
    CommentText TEXT,
    CommentDate TIMESTAMP,
    FOREIGN KEY (PostID) REFERENCES BlogPosts(PostID)
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