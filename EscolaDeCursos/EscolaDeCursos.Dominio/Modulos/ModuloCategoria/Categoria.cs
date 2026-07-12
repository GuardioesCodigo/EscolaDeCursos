using EscolaDeCursos.Dominio.Compartilhado;

namespace EscolaDeCursos.Dominio.Modulos.ModuloCategoria;

public class Categoria : EntidadeBase<Categoria>
{
    public string Nome { get; set; } = string.Empty;

    public Categoria() { }

    public Categoria(string nome)
    {
        Nome = nome;
    }

    public override List<string> Validar()
    {
        List<string> erros = [];

        if (string.IsNullOrWhiteSpace(Nome) || Nome.Length < 2 || Nome.Length > 100)
            erros.Add("O campo 'Nome' deve conter entre 2 e 100 caracteres.");

        return erros;
    }

    public override void Atualizar(Categoria entidadeAtualizada)
    {
        Nome = entidadeAtualizada.Nome;
    }
}