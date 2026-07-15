CREATE TABLE [dbo].[Matriculas] (
    [Id]            UNIQUEIDENTIFIER NOT NULL,
    [AlunoId]       UNIQUEIDENTIFIER NOT NULL,
    [TurmaId]       UNIQUEIDENTIFIER NOT NULL,
    [DataMatricula] DATETIME2 (7)    NOT NULL,
    [Situacao]      INT              NOT NULL
);
GO

ALTER TABLE [dbo].[Matriculas]
    ADD CONSTRAINT [PK_Matriculas] PRIMARY KEY CLUSTERED ([Id] ASC);
GO

