using System.ComponentModel.DataAnnotations;

namespace EscolaDeCursos.WebApp.Modulos.ModuloInstrutor;

public record ListarInstrutorViewModel(
    Guid Id,
    string Nome,
    string Cpf,
    string Email
);

public record CadastrarInstrutorViewModel(
    [Required(ErrorMessage = "O campo \"Nome\" deve ser preenchido.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "O campo \"Nome\" deve conter entre 2 e 100 caracteres.")]
    string Nome,

    [Required(ErrorMessage = "O campo \"CPF\" deve ser preenchido.")]
    [StringLength(14, MinimumLength = 11, ErrorMessage = "O campo \"CPF\" deve conter entre 11 e 14 caracteres.")]
    string Cpf,

    [Required(ErrorMessage = "O campo \"Email\" deve ser preenchido.")]
    [EmailAddress(ErrorMessage = "O campo \"Email\" deve conter um endereço de e-mail válido.")]
    [StringLength(250, ErrorMessage = "O campo \"Email\" deve conter no máximo 250 caracteres.")]
    string Email
);

public record EditarInstrutorViewModel(
    Guid Id,

    [Required(ErrorMessage = "O campo \"Nome\" deve ser preenchido.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "O campo \"Nome\" deve conter entre 2 e 100 caracteres.")]
    string Nome,

    [Required(ErrorMessage = "O campo \"CPF\" deve ser preenchido.")]
    [StringLength(14, MinimumLength = 11, ErrorMessage = "O campo \"CPF\" deve conter entre 11 e 14 caracteres.")]
    string Cpf,

    [Required(ErrorMessage = "O campo \"Email\" deve ser preenchido.")]
    [EmailAddress(ErrorMessage = "O campo \"Email\" deve conter um endereço de e-mail válido.")]
    [StringLength(250, ErrorMessage = "O campo \"Email\" deve conter no máximo 250 caracteres.")]
    string Email
);

public record ExcluirInstrutorViewModel(
    Guid Id,
    string Nome,
    string Cpf,
    string Email
);

public record DetalhesInstrutorViewModel(
    Guid Id,
    string Nome,
    string Cpf,
    string Email
);

