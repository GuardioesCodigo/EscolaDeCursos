CREATE TABLE [dbo].[Curso] (
    [Id]           UNIQUEIDENTIFIER NOT NULL,
    [Titulo]       NVARCHAR (MAX)   NOT NULL,
    [Descricao]    NVARCHAR (MAX)   NOT NULL,
    [CargaHoraria] INT              NOT NULL,
    [Nivel]        INT              NOT NULL,
    [CategoriaId]  UNIQUEIDENTIFIER NOT NULL
);
GO

ALTER TABLE [dbo].[Curso]
    ADD CONSTRAINT [PK_Curso] PRIMARY KEY CLUSTERED ([Id] ASC);
GO

