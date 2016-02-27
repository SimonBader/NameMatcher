CREATE TABLE [dbo].[States] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [CountryCode] NVARCHAR (MAX) NOT NULL,
    [Pin]         NVARCHAR (MAX) NOT NULL,
    [Offset]      FLOAT (53)     NOT NULL,
    [Points]      NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_dbo.States] PRIMARY KEY CLUSTERED ([Id] ASC)
);

