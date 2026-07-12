using AutoMapper;
using EscolaDeCursos.Aplicacao.Modulos.ModuloTurma;
using EscolaDeCursos.WebApp.Compartilhado.Extensions;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace EscolaDeCursos.WebApp.Modulos.ModuloTurma;

public class TurmaController(IMapper mapeador, ServicoTurma servicoTurma) : Controller
{
    [HttpGet]
    public ActionResult Listar()
    {
        List<ListarTurmaDto> dtos = servicoTurma.SelecionarTodos();

        List<ListarTurmaViewModel> listarVms = mapeador.Map<List<ListarTurmaViewModel>>(dtos);

        return View(listarVms);
    }

    [HttpGet]
    public ActionResult Cadastrar()
    {
        CadastrarTurmaViewModel cadastrarVm = new(Guid.Empty, Guid.Empty, string.Empty, DateTime.Today, DateTime.Today, 0);

        return View(cadastrarVm);
    }

    [HttpPost]
    public ActionResult Cadastrar(CadastrarTurmaViewModel cadastrarVm)
    {
        if (!ModelState.IsValid)
            return View(cadastrarVm);

        CadastrarTurmaDto dto = mapeador.Map<CadastrarTurmaDto>(cadastrarVm);
        Result resultado = servicoTurma.Cadastrar(dto);

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
        Result<DetalhesTurmaDto> resultado = servicoTurma.SelecionarPorId(id);

        if (resultado.IsFailed)
        {
            TempData.AddErrorMessage(resultado);

            return RedirectToAction(nameof(Listar));
        }

        EditarTurmaViewModel editarVm = mapeador.Map<EditarTurmaViewModel>(resultado.Value);

        return View(editarVm);
    }

    [HttpPost]
    public ActionResult Editar(EditarTurmaViewModel editarVm)
    {
        if (!ModelState.IsValid)
            return View(editarVm);

        EditarTurmaDto dto = mapeador.Map<EditarTurmaDto>(editarVm);
        Result resultado = servicoTurma.Editar(dto);

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
        Result<DetalhesTurmaDto> resultado = servicoTurma.SelecionarPorId(id);

        if (resultado.IsFailed)
        {
            TempData.AddErrorMessage(resultado);

            return RedirectToAction(nameof(Listar));
        }

        ExcluirTurmaViewModel excluirVm = mapeador.Map<ExcluirTurmaViewModel>(resultado.Value);

        return View(excluirVm);
    }

    [HttpPost]
    public ActionResult Excluir(ExcluirTurmaViewModel excluirVm)
    {
        Result resultado = servicoTurma.Excluir(excluirVm.Id);

        if (resultado.IsFailed)
            TempData.AddErrorMessage(resultado);

        return RedirectToAction(nameof(Listar));
    }
}