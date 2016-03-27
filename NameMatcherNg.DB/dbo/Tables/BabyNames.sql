CREATE TABLE [dbo].[BabyNames] (
    [Id]                            INT            IDENTITY (1, 1) NOT NULL,
    [Name]                          NVARCHAR (MAX) NULL,
    [line]                          NVARCHAR (MAX) NULL,
    [IsFemale]                      BIT            NOT NULL,
    [CountriesWithSimilarNameCount] INT            NULL, 
    CONSTRAINT [PK_BabyNames] PRIMARY KEY ([Id])
);
GO;

CREATE INDEX IX_BabyNames_Name 
    ON dbo.BabyNames (Name);
GO;

