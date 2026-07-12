using System;
using AutoMapper;
using EscolaDeCursos.Aplicacao.Modulos.ModuloInstrutor;

namespace EscolaDeCursos.WebApp.Modulos.ModuloInstrutor;

public class MapeadorInstrutor : Profile
{
    public MapeadorInstrutor()
    {
        CreateMap<CadastrarInstrutorViewModel, CadastrarInstrutorDto>();
        CreateMap<EditarInstrutorViewModel, EditarInstrutorDto>();
        CreateMap<ListarInstrutorDto, ListarInstrutorViewModel>();
        CreateMap<DetalhesInstrutorDto, DetalhesInstrutorViewModel>();
        CreateMap<DetalhesInstrutorDto, ExcluirInstrutorViewModel>();
    }
}
