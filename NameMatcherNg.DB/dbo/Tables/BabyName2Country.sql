CREATE TABLE [dbo].[BabyName2Country]
( 
    [Id] INT NOT NULL, 
    [Frequency] INT NOT NULL, 
    [BabyNameId] INT NOT NULL, 
    [CountryId] INT NOT NULL,
    CONSTRAINT [FK_BabyName2Country_BabyNames] FOREIGN KEY ([BabyNameId]) REFERENCES [BabyNames]([Id]), 
    CONSTRAINT [FK_BabyName2Country_Countries] FOREIGN KEY ([CountryId]) REFERENCES [Countries]([Id]), 
    CONSTRAINT [PK_BabyName2Country] PRIMARY KEY ([Id]) 
)

GO

CREATE NONCLUSTERED INDEX [IX_CountryId]
    ON [dbo].[BabyName2Country]([CountryId] ASC);
GO

CREATE NONCLUSTERED INDEX [IX_BabyNameId]
    ON [dbo].[BabyName2Country]([BabyNameId] ASC);
GO

CREATE UNIQUE INDEX [AK_BabyName2Country] 
	ON [dbo].[BabyName2Country] ([BabyNameId], [CountryId]);
GO
