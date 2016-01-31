CREATE TABLE [dbo].[BabyNames] (
    [Id]        INT            IDENTITY (1, 1) NOT NULL,
    [CountryId] INT            NOT NULL,
    [Name]      NVARCHAR (MAX) NULL,
    [HRef]      NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_dbo.BabyNames] PRIMARY KEY CLUSTERED ([Id] ASC)
);

