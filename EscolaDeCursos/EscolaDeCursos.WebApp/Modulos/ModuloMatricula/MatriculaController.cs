using AutoMapper;
using EscolaDeCursos.Aplicacao.Modulos.ModuloMatricula;
using EscolaDeCursos.WebApp.Compartilhado.Extensions;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace EscolaDeCursos.WebApp.Modulos.ModuloMatricula;

public class MatriculaController(IMapper mapeador, ServicoMatricula servicoMatricula) : Controller
{
    [HttpGet]
    public ActionResult Listar()
    {
        List<ListarMatriculaDto> dtos = servicoMatricula.SelecionarTodos();

        List<ListarMatriculaViewModel> listarVms = mapeador.Map<List<ListarMatriculaViewModel>>(dtos);

        return View(listarVms);
    }

    [HttpGet]
    public ActionResult Cadastrar()
    {
        CadastrarMatriculaViewModel cadastrarVm = new(Guid.Empty, Guid.Empty);

        return View(cadastrarVm);
    }

    [HttpPost]
    public ActionResult Cadastrar(CadastrarMatriculaViewModel cadastrarVm)
    {
        if (!ModelState.IsValid)
            return View(cadastrarVm);

        CadastrarMatriculaDto dto = mapeador.Map<CadastrarMatriculaDto>(cadastrarVm);
        Result resultado = servicoMatricula.Cadastrar(dto);

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
    public ActionResult Detalhes(Guid id)
    {
        Result<DetalhesMatriculaDto> resultado = servicoMatricula.SelecionarPorId(id);

        if (resultado.IsFailed)
        {
            TempData.AddErrorMessage(resultado);

            return RedirectToAction(nameof(Listar));
        }

        DetalhesMatriculaViewModel detalhesVm = mapeador.Map<DetalhesMatriculaViewModel>(resultado.Value);

        return View(detalhesVm);
    }

    [HttpGet]
    public ActionResult Excluir(Guid id)
    {
        Result<DetalhesMatriculaDto> resultado = servicoMatricula.SelecionarPorId(id);

        if (resultado.IsFailed)
        {
            TempData.AddErrorMessage(resultado);

            return RedirectToAction(nameof(Listar));
        }

        ExcluirMatriculaViewModel excluirVm = mapeador.Map<ExcluirMatriculaViewModel>(resultado.Value);

        return View(excluirVm);
    }

    [HttpPost]
    public ActionResult Excluir(ExcluirMatriculaViewModel excluirVm)
    {
        Result resultado = servicoMatricula.Excluir(excluirVm.Id);

        if (resultado.IsFailed)
            TempData.AddErrorMessage(resultado);

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Cancelar(Guid id)
    {
        Result<DetalhesMatriculaDto> resultado = servicoMatricula.SelecionarPorId(id);

        if (resultado.IsFailed)
        {
            TempData.AddErrorMessage(resultado);

            return RedirectToAction(nameof(Listar));
        }

        CancelarMatriculaViewModel cancelarVm = mapeador.Map<CancelarMatriculaViewModel>(resultado.Value);

        return View(cancelarVm);
    }

    [HttpPost]
    public ActionResult Cancelar(CancelarMatriculaViewModel cancelarVm)
    {
        Result resultado = servicoMatricula.Cancelar(cancelarVm.Id);

        if (resultado.IsFailed)
            TempData.AddErrorMessage(resultado);

        return RedirectToAction(nameof(Listar));
    }

    [HttpGet]
    public ActionResult Concluir(Guid id)
    {
        Result<DetalhesMatriculaDto> resultado = servicoMatricula.SelecionarPorId(id);

        if (resultado.IsFailed)
        {
            TempData.AddErrorMessage(resultado);

            return RedirectToAction(nameof(Listar));
        }

        ConcluirMatriculaViewModel concluirVm = mapeador.Map<ConcluirMatriculaViewModel>(resultado.Value);

        return View(concluirVm);
    }

    [HttpPost]
    public ActionResult Concluir(ConcluirMatriculaViewModel concluirVm)
    {
        Result resultado = servicoMatricula.Concluir(concluirVm.Id);

        if (resultado.IsFailed)
            TempData.AddErrorMessage(resultado);

        return RedirectToAction(nameof(Listar));
    }
}