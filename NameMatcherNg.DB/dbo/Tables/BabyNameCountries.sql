CREATE TABLE [dbo].[BabyNameCountries] (
    [BabyName_Id] INT NOT NULL,
    [Country_Id]  INT NOT NULL
);


GO
CREATE NONCLUSTERED INDEX [IX_BabyName_Id]
    ON [dbo].[BabyNameCountries]([BabyName_Id] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Country_Id]
    ON [dbo].[BabyNameCountries]([Country_Id] ASC);


GO
ALTER TABLE [dbo].[BabyNameCountries]
    ADD CONSTRAINT [PK_dbo.BabyNameCountries] PRIMARY KEY CLUSTERED ([BabyName_Id] ASC, [Country_Id] ASC);
