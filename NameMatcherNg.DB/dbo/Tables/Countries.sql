CREATE TABLE [dbo].[Countries] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (MAX) NULL,
    [CountryCode] NVARCHAR (MAX) NULL
);
GO;

CREATE INDEX IX_Countries_CountryCode 
    ON dbo.Countries (CountryCode);

