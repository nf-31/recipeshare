CREATE TABLE Recipe
(
    ID          INT IDENTITY(1,1) PRIMARY KEY,
    Title       NVARCHAR(255) NOT NULL,
    CookingTime INT NOT NULL
);
