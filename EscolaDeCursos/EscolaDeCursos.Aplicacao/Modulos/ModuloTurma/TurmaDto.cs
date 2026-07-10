namespace EscolaDeCursos.Aplicacao.Modulos.ModuloTurma;

public record CadastrarTurmaDto(
    Guid CursoId,
    Guid InstrutorId,
    string Nome,
    DateTime DataInicio,
    DateTime DataTermino,
    int CapacidadeAlunos
);

public record EditarTurmaDto(
    Guid Id,
    Guid CursoId,
    Guid InstrutorId,
    string Nome,
    DateTime DataInicio,
    DateTime DataTermino,
    int CapacidadeAlunos
);

public record ListarTurmaDto(
    Guid Id,
    string Nome,
    string NomeCurso,
    string NomeInstrutor,
    DateTime DataInicio,
    DateTime DataTermino,
    int CapacidadeAlunos
);

public record DetalhesTurmaDto(
    Guid Id,
    string Nome,
    string NomeCurso,
    string NomeInstrutor,
    DateTime DataInicio,
    DateTime DataTermino,
    int CapacidadeAlunos
);
