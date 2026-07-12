namespace EscolaDeCursos.Aplicacao.Modulos.ModuloInstrutor;

public record DetalhesInstrutorDto(
    Guid Id, 
    string Nome,
    string Cpf,
    string Email)
;

public record ListarInstrutorDto(
    Guid Id, 
    string Nome,
    string Cpf,
    string Email
);

public record CadastrarInstrutorDto(
    string Nome, 
    string Cpf,
    string Email
);

public record EditarInstrutorDto(
    Guid Id, 
    string Nome, 
    string Cpf,
    string Email
);