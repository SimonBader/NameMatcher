CREATE TABLE [dbo].[BabyNames] (
    [Id]        INT            IDENTITY (1, 1) NOT NULL,
    [Name]      NVARCHAR (100) NULL,
    [line]      NVARCHAR (MAX) NULL,
    [IsFemale]  BIT            NOT NULL,
    [Frequency] INT            NOT NULL, 
    [CountriesWithSimilarNameCount] INT NULL
);
GO;

CREATE INDEX IX_BabyNames_Name 
    ON dbo.BabyNames (Name);
GO;


