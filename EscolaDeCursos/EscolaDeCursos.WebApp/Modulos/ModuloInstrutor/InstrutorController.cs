using AutoMapper;
using EscolaDeCursos.Aplicacao.Modulos.ModuloInstrutor;
using EscolaDeCursos.WebApp.Compartilhado.Extensions;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace EscolaDeCursos.WebApp.Modulos.ModuloInstrutor;

public class InstrutorController(IMapper mapeador, ServicoInstrutor servicoInstrutor) : Controller
{
    [HttpGet]
    public ActionResult Listar()
    {
        List<ListarInstrutorDto> dtos = servicoInstrutor.SelecionarTodos();

        List<ListarInstrutorViewModel> listarVms = mapeador.Map<List<ListarInstrutorViewModel>>(dtos);

        return View(listarVms);
    }

    [HttpGet]
    public ActionResult Cadastrar()
    {
        CadastrarInstrutorViewModel cadastrarVm = new(string.Empty, string.Empty, string.Empty);

        return View(cadastrarVm);
    }

    [HttpPost]
    public ActionResult Cadastrar(CadastrarInstrutorViewModel cadastrarVm)
    {
        if (!ModelState.IsValid)
            return View(cadastrarVm);

        CadastrarInstrutorDto dto = mapeador.Map<CadastrarInstrutorDto>(cadastrarVm);
        Result resultado = servicoInstrutor.Cadastrar(dto);

        if (resultado.IsFailed)
        {
            foreach (IError erro in resultado.Errors)
            {
                string campo =
                    erro.Metadata["Campo"] is string
                        ? erro.Metadata["Campo"].ToString()!
                        : string.Empty;

                ModelState.AddModelError(campo, erro.Message);
            }

            return View(cadastrarVm);
        }

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Editar(Guid id)
    {
        Result<DetalhesInstrutorDto> resultado = servicoInstrutor.SelecionarPorId(id);

        if (resultado.IsFailed)
        {
            TempData.AddErrorMessage(resultado);

            return RedirectToAction(nameof(Listar));
        }

        EditarInstrutorViewModel editarVm = mapeador.Map<EditarInstrutorViewModel>(resultado.Value);

        return View(editarVm);
    }

    [HttpPost]
    public ActionResult Editar(EditarInstrutorViewModel editarVm)
    {
        if (!ModelState.IsValid)
            return View(editarVm);

        EditarInstrutorDto dto = mapeador.Map<EditarInstrutorDto>(editarVm);
        Result resultado = servicoInstrutor.Editar(dto);

        if (resultado.IsFailed)
        {
            ModelState.AddModelError(resultado);

            return View(editarVm);
        }

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Excluir(Guid id)
    {
        Result<DetalhesInstrutorDto> resultado = servicoInstrutor.SelecionarPorId(id);

        if (resultado.IsFailed)
        {
            TempData.AddErrorMessage(resultado);

            return RedirectToAction(nameof(Listar));
        }

        ExcluirInstrutorViewModel excluirVm = mapeador.Map<ExcluirInstrutorViewModel>(resultado.Value);

        return View(excluirVm);
    }

    [HttpPost]
    public ActionResult Excluir(ExcluirInstrutorViewModel excluirVm)
    {
        Result resultado = servicoInstrutor.Excluir(excluirVm.Id);

        if (resultado.IsFailed)
            TempData.AddErrorMessage(resultado);

        return RedirectToAction(nameof(Listar));
    }
}