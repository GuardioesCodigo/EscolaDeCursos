using System.ComponentModel.DataAnnotations;

namespace EscolaDeCursos.WebApp.Modulos.ModuloTurma;

public record ListarTurmaViewModel(
    Guid Id,
    string Nome,
    string NomeCurso,
    string NomeInstrutor,
    DateTime DataInicio,
    DateTime DataTermino,
    int CapacidadeAlunos
);

public record CadastrarTurmaViewModel(
    [Required(ErrorMessage = "O campo \"Curso\" deve ser preenchido.")]
    Guid CursoId,

    [Required(ErrorMessage = "O campo \"Instrutor\" deve ser preenchido.")]
    Guid InstrutorId,

    [Required(ErrorMessage = "O campo \"Nome\" deve ser preenchido.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "O campo \"Nome\" deve conter entre 2 e 100 caracteres.")]
    string Nome,

    [Required(ErrorMessage = "O campo \"Data de Início\" deve ser preenchido.")]
    DateTime DataInicio,

    [Required(ErrorMessage = "O campo \"Data de Término\" deve ser preenchido.")]
    DateTime DataTermino,

    [Required(ErrorMessage = "O campo \"Capacidade de Alunos\" deve ser preenchido.")]
    [Range(1, 500, ErrorMessage = "O campo \"Capacidade de Alunos\" deve estar entre 1 e 500.")]
    int CapacidadeAlunos
);

public record EditarTurmaViewModel(
    Guid Id,

    [Required(ErrorMessage = "O campo \"Curso\" deve ser preenchido.")]
    Guid CursoId,

    [Required(ErrorMessage = "O campo \"Instrutor\" deve ser preenchido.")]
    Guid InstrutorId,

    [Required(ErrorMessage = "O campo \"Nome\" deve ser preenchido.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "O campo \"Nome\" deve conter entre 2 e 100 caracteres.")]
    string Nome,

    [Required(ErrorMessage = "O campo \"Data de Início\" deve ser preenchido.")]
    DateTime DataInicio,

    [Required(ErrorMessage = "O campo \"Data de Término\" deve ser preenchido.")]
    DateTime DataTermino,

    [Required(ErrorMessage = "O campo \"Capacidade de Alunos\" deve ser preenchido.")]
    [Range(1, 500, ErrorMessage = "O campo \"Capacidade de Alunos\" deve estar entre 1 e 500.")]
    int CapacidadeAlunos
);

public record DetalhesTurmaViewModel(
    Guid Id,
    string Nome,
    string NomeCurso,
    string NomeInstrutor,
    DateTime DataInicio,
    DateTime DataTermino,
    int CapacidadeAlunos
);

public record ExcluirTurmaViewModel(
    Guid Id,
    string Nome,
    string NomeCurso,
    string NomeInstrutor,
    DateTime DataInicio,
    DateTime DataTermino,
    int CapacidadeAlunos
);