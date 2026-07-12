using AutoMapper;
using EscolaDeCursos.Aplicacao.Modulos.ModuloMatricula;

namespace EscolaDeCursos.WebApp.Modulos.ModuloMatricula;

public class MapeadorMatricula : Profile
{
    public MapeadorMatricula()
    {
        CreateMap<CadastrarMatriculaViewModel, CadastrarMatriculaDto>();
        CreateMap<ListarMatriculaDto, ListarMatriculaViewModel>();
        CreateMap<DetalhesMatriculaDto, DetalhesMatriculaViewModel>();
        CreateMap<DetalhesMatriculaDto, ExcluirMatriculaViewModel>();
        CreateMap<DetalhesMatriculaDto, CancelarMatriculaViewModel>();
        CreateMap<DetalhesMatriculaDto, ConcluirMatriculaViewModel>();
    }
}