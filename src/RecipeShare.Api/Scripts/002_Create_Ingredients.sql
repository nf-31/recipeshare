CREATE TABLE Ingredients
(
    ID       INT IDENTITY(1,1) PRIMARY KEY,
    RID      INT            NOT NULL,
    Name     NVARCHAR(255) NOT NULL,
    Quantity DECIMAL(10, 2) NOT NULL,
    Unit     NVARCHAR(50),
    CONSTRAINT FK_Ingredients_Recipe FOREIGN KEY (RID) REFERENCES Recipe (ID)
);

CREATE INDEX IX_Ingredients_RID ON Ingredients (RID);
