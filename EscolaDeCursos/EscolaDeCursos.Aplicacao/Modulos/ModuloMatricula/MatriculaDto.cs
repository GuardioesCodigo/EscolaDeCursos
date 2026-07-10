using EscolaDeCursos.Dominio.Modulos.ModuloMatricula;

namespace EscolaDeCursos.Aplicacao.Modulos.ModuloMatricula;

public record CadastrarMatriculaDto(
    Guid AlunoId, 
    Guid TurmaId
);

public record ListarMatriculaDto(
    Guid Id,
    string NomeAluno,
    string NomeTurma,
    DateTime DataMatricula,
    SituacaoMatricula Situacao
);

public record DetalhesMatriculaDto(
    Guid Id,
    string NomeAluno,
    string NomeTurma,
    DateTime DataMatricula,
    SituacaoMatricula Situacao
);