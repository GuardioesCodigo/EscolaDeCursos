using System.ComponentModel.DataAnnotations;

namespace EscolaDeCursos.WebApp.Modulos.ModuloAluno;

public record ListarAlunoViewModel(
    Guid Id,
    string Nome,
    string Cpf,
    string Email
);

public record CadastrarAlunoViewModel(
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

public record EditarAlunoViewModel(
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

public record ExcluirAlunoViewModel(
    Guid Id,
    string Nome,
    string Cpf,
    string Email
);

public record DetalhesAlunoViewModel(
    Guid Id,
    string Nome,
    string Cpf,
    string Email
);