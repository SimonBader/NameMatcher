CREATE TABLE [dbo].[Countries] (
    [Id]   INT            IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (MAX) NULL,
    [HRef] NVARCHAR (MAX) NULL,
    [CountryCode] NVARCHAR(5) NULL, 
    CONSTRAINT [PK_dbo.Countries] PRIMARY KEY CLUSTERED ([Id] ASC)
);

