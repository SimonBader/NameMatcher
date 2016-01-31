CREATE TABLE [dbo].[todoItems] (
    [id]        INT            IDENTITY (1, 1) NOT NULL,
    [task]      NVARCHAR (MAX) NULL,
    [completed] BIT            NOT NULL,
    [User_Id]   NVARCHAR (128) NULL,
    CONSTRAINT [PK_dbo.todoItems] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_dbo.todoItems_dbo.AspNetUsers_User_Id] FOREIGN KEY ([User_Id]) REFERENCES [dbo].[AspNetUsers] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_User_Id]
    ON [dbo].[todoItems]([User_Id] ASC);

