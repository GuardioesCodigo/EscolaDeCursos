using AutoMapper;
using EscolaDeCursos.Aplicacao.Modulos.ModuloTurma;

namespace EscolaDeCursos.WebApp.Modulos.ModuloTurma;

public class MapeadorTurma : Profile
{
    public MapeadorTurma()
    {
        CreateMap<CadastrarTurmaViewModel, CadastrarTurmaDto>();
        CreateMap<EditarTurmaViewModel, EditarTurmaDto>();
        CreateMap<ListarTurmaDto, ListarTurmaViewModel>();
        CreateMap<DetalhesTurmaDto, DetalhesTurmaViewModel>();
        CreateMap<DetalhesTurmaDto, ExcluirTurmaViewModel>();
    }
}