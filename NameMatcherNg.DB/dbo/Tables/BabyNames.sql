CREATE TABLE [dbo].[BabyNames] (
    [Id]        INT            IDENTITY (1, 1) NOT NULL,
    [CountryId] INT            NOT NULL,
    [Name]      NVARCHAR (100) NULL,
    [HRef]      NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_dbo.BabyNames] PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO;

CREATE INDEX IX_BabyNames_Name 
    ON dbo.BabyNames (Name);
GO;
	
CREATE INDEX IX_BabyNames_CountryId
    ON dbo.BabyNames (CountryId);

