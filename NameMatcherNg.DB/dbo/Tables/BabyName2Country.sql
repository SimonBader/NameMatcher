CREATE TABLE [dbo].[BabyName2Country] (
    [Id]         INT IDENTITY (1, 1) NOT NULL,
    [CountryId]  INT NOT NULL,
    [BabyNameId] INT NOT NULL,
    [Frequency]  INT NOT NULL,
    CONSTRAINT [FK_BabyName2Country_BabyNames] FOREIGN KEY ([BabyNameId]) REFERENCES [BabyNames]([Id]) ON DELETE CASCADE, 
    CONSTRAINT [FK_BabyName2Country_Countries] FOREIGN KEY ([CountryId]) REFERENCES [Countries]([Id]) ON DELETE CASCADE, 
    CONSTRAINT [PK_BabyName2Country] PRIMARY KEY CLUSTERED ([Id] ASC) 
);


GO
CREATE NONCLUSTERED INDEX [IX_CountryId]
    ON [dbo].[BabyName2Country]([CountryId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_BabyNameId]
    ON [dbo].[BabyName2Country]([BabyNameId] ASC);

