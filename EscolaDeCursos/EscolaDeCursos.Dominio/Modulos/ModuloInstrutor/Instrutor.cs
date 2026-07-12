using System.Text.RegularExpressions;
using EscolaDeCursos.Dominio.Compartilhado;

namespace EscolaDeCursos.Dominio.Modulos.ModuloInstrutor;

public class Instrutor : EntidadeBase<Instrutor>
{
    public string Nome { get; set; } = string.Empty;
    public string Cpf { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    public Instrutor() { }

    public Instrutor(string nome, string cpf, string email)
    {
        Nome = nome;
        Cpf = cpf;
        Email = email;
    }

    public override List<string> Validar()
    {
        List<string> erros = [];

        if (string.IsNullOrWhiteSpace(Nome) || Nome.Length < 3 || Nome.Length > 100)
            erros.Add("O campo \"Nome\" deve conter entre 3 e 100 caracteres.");

        if (string.IsNullOrWhiteSpace(Cpf) || !Regex.IsMatch(Cpf, @"^\d{3}\.\d{3}\.\d{3}-\d{2}$"))
            erros.Add("O campo \"CPF\" deve estar no formato 000.000.000-00.");

        if (string.IsNullOrWhiteSpace(Email) || !Regex.IsMatch(Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            erros.Add("O campo \"Email\" deve conter um e-mail válido.");
        return erros;
    }

    public override void Atualizar(Instrutor entidadeAtualizada)
    {
        Nome = entidadeAtualizada.Nome;
        Cpf = entidadeAtualizada.Cpf;
        Email = entidadeAtualizada.Email;
    }
}