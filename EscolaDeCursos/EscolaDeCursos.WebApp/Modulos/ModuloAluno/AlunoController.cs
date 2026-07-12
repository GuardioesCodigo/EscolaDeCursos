using AutoMapper;
using EscolaDeCursos.Aplicacao.Modulos.ModuloAluno;
using EscolaDeCursos.WebApp.Compartilhado.Extensions;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace EscolaDeCursos.WebApp.Modulos.ModuloAluno;

public class AlunoController(IMapper mapeador, ServicoAluno servicoAluno) : Controller
{
    [HttpGet]
    public ActionResult Listar()
    {
        List<ListarAlunosDto> dtos = servicoAluno.SelecionarTodos();

        List<ListarAlunoViewModel> listarVms = mapeador.Map<List<ListarAlunoViewModel>>(dtos);

        return View(listarVms);
    }

    [HttpGet]
    public ActionResult Cadastrar()
    {
        CadastrarAlunoViewModel cadastrarVm = new(string.Empty, string.Empty, string.Empty);

        return View(cadastrarVm);
    }

    [HttpPost]
    public ActionResult Cadastrar(CadastrarAlunoViewModel cadastrarVm)
    {
        if (!ModelState.IsValid)
            return View(cadastrarVm);

        CadastrarAlunoDto dto = mapeador.Map<CadastrarAlunoDto>(cadastrarVm);
        Result resultado = servicoAluno.Cadastrar(dto);

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
        Result<DetalhesAlunoDto> resultado = servicoAluno.SelecionarPorId(id);

        if (resultado.IsFailed)
        {
            TempData.AddErrorMessage(resultado);

            return RedirectToAction(nameof(Listar));
        }

        EditarAlunoViewModel editarVm = mapeador.Map<EditarAlunoViewModel>(resultado.Value);

        return View(editarVm);
    }

    [HttpPost]
    public ActionResult Editar(EditarAlunoViewModel editarVm)
    {
        if (!ModelState.IsValid)
            return View(editarVm);

        EditarAlunoDto dto = mapeador.Map<EditarAlunoDto>(editarVm);
        Result resultado = servicoAluno.Editar(dto);

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
        Result<DetalhesAlunoDto> resultado = servicoAluno.SelecionarPorId(id);

        if (resultado.IsFailed)
        {
            TempData.AddErrorMessage(resultado);

            return RedirectToAction(nameof(Listar));
        }

        ExcluirAlunoViewModel excluirVm = mapeador.Map<ExcluirAlunoViewModel>(resultado.Value);

        return View(excluirVm);
    }

    [HttpPost]
    public ActionResult Excluir(ExcluirAlunoViewModel excluirVm)
    {
        Result resultado = servicoAluno.Excluir(excluirVm.Id);

        if (resultado.IsFailed)
            TempData.AddErrorMessage(resultado);

        return RedirectToAction(nameof(Listar));
    }
}