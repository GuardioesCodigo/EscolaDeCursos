using System.ComponentModel.DataAnnotations;
using EscolaDeCursos.Dominio.Modulos.ModuloMatricula;

namespace EscolaDeCursos.WebApp.Modulos.ModuloMatricula;

public record ListarMatriculaViewModel(
    Guid Id,
    string NomeAluno,
    string NomeTurma,
    DateTime DataMatricula,
    SituacaoMatricula Situacao
);

public record CadastrarMatriculaViewModel(
    [Required(ErrorMessage = "O campo \"Aluno\" deve ser preenchido.")]
    Guid AlunoId,

    [Required(ErrorMessage = "O campo \"Turma\" deve ser preenchido.")]
    Guid TurmaId
);

public record DetalhesMatriculaViewModel(
    Guid Id,
    string NomeAluno,
    string NomeTurma,
    DateTime DataMatricula,
    SituacaoMatricula Situacao
);

public record ExcluirMatriculaViewModel(
    Guid Id,
    string NomeAluno,
    string NomeTurma,
    DateTime DataMatricula,
    SituacaoMatricula Situacao
);

public record CancelarMatriculaViewModel(
    Guid Id,
    string NomeAluno,
    string NomeTurma,
    DateTime DataMatricula,
    SituacaoMatricula Situacao
);

public record ConcluirMatriculaViewModel(
    Guid Id,
    string NomeAluno,
    string NomeTurma,
    DateTime DataMatricula,
    SituacaoMatricula Situacao
);