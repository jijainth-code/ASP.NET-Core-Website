CREATE TABLE [dbo].[customer] (
    [Id]     INT            IDENTITY (1, 1) NOT NULL,
    [Name]   NVARCHAR (MAX) NOT NULL,
    [amount] INT            NULL,
    CONSTRAINT [PK_customer] PRIMARY KEY CLUSTERED ([Id] ASC)
);

