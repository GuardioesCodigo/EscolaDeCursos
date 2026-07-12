using System;
using AutoMapper;
using EscolaDeCursos.Aplicacao.Modulos.ModuloAluno;

namespace EscolaDeCursos.WebApp.Modulos.ModuloAluno;

public class AlunoProfile : Profile
{
    public AlunoProfile()
    {
        CreateMap<CadastrarAlunoViewModel, CadastrarAlunoDto>();
        CreateMap<EditarAlunoViewModel, EditarAlunoDto>();
        CreateMap<ListarAlunosDto, ListarAlunoViewModel>();
        CreateMap<DetalhesAlunoDto, DetalhesAlunoViewModel>();
        CreateMap<DetalhesAlunoDto, ExcluirAlunoViewModel>();
    }

}
