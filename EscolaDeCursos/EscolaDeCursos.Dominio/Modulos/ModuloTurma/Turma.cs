using EscolaDeCursos.Dominio.Compartilhado;

namespace EscolaDeCursos.Dominio.Modulos.ModuloTurma;

public class Turma : EntidadeBase<Turma>
{
    public Guid CursoId { get; set; }
    public Guid InstrutorId { get; set; }
    public string Nome {get; set;} = string.Empty;
    public DateTime DataInicio { get; set; }
    public DateTime DataTermino { get; set; }
    public int CapacidadeAlunos { get; set; }

    public Turma() { }

    public Turma(Guid cursoId, Guid instrutorId, DateTime dataInicio, DateTime dataTermino, int capacidadeAlunos, string nome)
    {
        CursoId = cursoId;
        InstrutorId = instrutorId;
        Nome = nome;
        DataInicio = dataInicio;
        DataTermino = dataTermino;
        CapacidadeAlunos = capacidadeAlunos;
    }

    public override List<string> Validar()
    {
        List<string> erros = [];

        if (CursoId == Guid.Empty) erros.Add("O campo \"Curso\" é obrigatório.");
        
        if (InstrutorId == Guid.Empty) erros.Add("O campo \"Instrutor\" é obrigatório.");

        if (string.IsNullOrWhiteSpace(Nome) || Nome.Length < 3 || Nome.Length > 100)
            erros.Add("O campo \"Nome\" deve conter entre 3 e 100 caracteres.");

        if (CapacidadeAlunos <= 0)
            erros.Add("A capacidade máximo de alunos deve ser maior que zero.");

        if (DataTermino <= DataInicio)
            erros.Add("A data de término da turma deve ser posterior à data de início.");

        return erros;
    }

    public override void Atualizar(Turma entidadeAtualizada)
    {
        CursoId = entidadeAtualizada.CursoId;
        InstrutorId = entidadeAtualizada.InstrutorId;
        Nome = entidadeAtualizada.Nome;
        DataInicio = entidadeAtualizada.DataInicio;
        DataTermino = entidadeAtualizada.DataTermino;
        CapacidadeAlunos = entidadeAtualizada.CapacidadeAlunos;
    }
}