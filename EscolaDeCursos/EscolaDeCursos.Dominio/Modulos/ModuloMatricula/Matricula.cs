using EscolaDeCursos.Dominio.Compartilhado;

namespace EscolaDeCursos.Dominio.Modulos.ModuloMatricula;

public class Matricula : EntidadeBase<Matricula>
{
    public Guid AlunoId { get; set; }
    public Guid TurmaId { get; set; }
    public DateTime DataMatricula { get; set; }
    public SituacaoMatricula Situacao { get; set; }

    public Matricula() { }

    public Matricula(Guid alunoId, Guid turmaId, SituacaoMatricula situacao)
    {
        AlunoId = alunoId;
        TurmaId = turmaId;
        DataMatricula = DateTime.Now;
        Situacao = situacao;
    }

    public override List<string> Validar()
    {
        List<string> erros = [];

        if (AlunoId == Guid.Empty) erros.Add("O campo \"Aluno\" é obrigatório.");

        if (TurmaId == Guid.Empty) erros.Add("O campo \"Turma\" é obrigatório.");

        if (DataMatricula == default) erros.Add("O campo \"Data da Matrícula\" é obrigatório.");

        return erros;
    }
    
    public override void Atualizar(Matricula entidadeAtualizada)
    {
        Situacao = entidadeAtualizada.Situacao;
    }
}