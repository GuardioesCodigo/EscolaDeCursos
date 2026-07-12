namespace EscolaDeCursos.Aplicacao.Modulos.ModuloAluno;

public record DetalhesAlunoDto(
    Guid Id, 
    string Nome, 
    string Cpf, 
    string Email
);

public record CadastrarAlunoDto(
    string Nome, 
    string Cpf, 
    string Email
);

public record ListarAlunosDto(
    Guid Id, 
    string Nome, 
    string Cpf, 
    string Email
);

public record EditarAlunoDto(
    Guid Id, 
    string Nome, 
    string Cpf,  
    string Email)
;