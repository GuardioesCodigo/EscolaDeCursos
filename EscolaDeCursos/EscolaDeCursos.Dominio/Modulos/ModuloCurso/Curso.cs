using EscolaDeCursos.Dominio.Compartilhado;

namespace EscolaDeCursos.Dominio.Modulos.ModuloCurso;

public class Curso : EntidadeBase<Curso>
{
    public string Titulo { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public int CargaHoraria { get; set; }
    public NivelCurso Nivel { get; set; }
    public Guid CategoriaId { get; set; }

    public Curso() { }

    public Curso(string titulo, string descricao, int cargaHoraria, NivelCurso nivel, Guid categoriaId)
    {
        Titulo = titulo;
        Descricao = descricao;
        CargaHoraria = cargaHoraria;
        Nivel = nivel;
        CategoriaId = categoriaId;
    }

    public override List<string> Validar()
    {
        List<string> erros = [];

        if (string.IsNullOrWhiteSpace(Titulo) || Titulo.Length < 2 || Titulo.Length > 100)
            erros.Add("O campo 'Título' deve conter entre 2 e 100 caracteres.");

        if (string.IsNullOrWhiteSpace(Descricao))
            erros.Add("O campo 'Descrição' é obrigatório.");

        if (CargaHoraria <= 0)
            erros.Add("A Carga Horária deve ser maior que zero.");

        if (CategoriaId == Guid.Empty)
            erros.Add("O curso deve estar obrigatoriamente vinculado a uma categoria.");

        return erros;
    }

    public override void Atualizar(Curso entidadeAtualizada)
    {
        Titulo = entidadeAtualizada.Titulo;
        Descricao = entidadeAtualizada.Descricao;
        CargaHoraria = entidadeAtualizada.CargaHoraria;
        Nivel = entidadeAtualizada.Nivel;
        CategoriaId = entidadeAtualizada.CategoriaId;
    }
}