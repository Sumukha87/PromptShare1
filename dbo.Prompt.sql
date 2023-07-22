CREATE TABLE [dbo].[Prompt] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [PromptName] NVARCHAR (MAX) NOT NULL,
    [PromptDescription]  NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_Prompt] PRIMARY KEY CLUSTERED ([Id] ASC)
);

