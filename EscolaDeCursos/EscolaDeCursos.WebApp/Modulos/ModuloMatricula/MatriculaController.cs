using AutoMapper;
using EscolaDeCursos.Aplicacao.Modulos.ModuloMatricula;
using EscolaDeCursos.Aplicacao.Modulos.ModuloAluno;
using EscolaDeCursos.Aplicacao.Modulos.ModuloTurma;
using EscolaDeCursos.WebApp.Compartilhado.Extensions;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EscolaDeCursos.WebApp.Modulos.ModuloMatricula;

public class MatriculaController(
    IMapper mapeador,
    ServicoMatricula servicoMatricula,
    ServicoAluno servicoAluno,
    ServicoTurma servicoTurma) : Controller
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
        CarregarSelecaoListas();

        CadastrarMatriculaViewModel cadastrarVm = new(Guid.Empty, Guid.Empty);

        return View(cadastrarVm);
    }

    [HttpPost]
    public ActionResult Cadastrar(CadastrarMatriculaViewModel cadastrarVm)
    {
        if (!ModelState.IsValid)
        {
            CarregarSelecaoListas();
            return View(cadastrarVm);
        }

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

            CarregarSelecaoListas();
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

    [HttpPost]
    public ActionResult Cancelar(Guid id)
    {
        Result resultado = servicoMatricula.Cancelar(id);

        if (resultado.IsFailed)
            TempData.AddErrorMessage(resultado);

        return RedirectToAction(nameof(Detalhes), new { id });
    }

    [HttpPost]
    public ActionResult Concluir(Guid id)
    {
        Result resultado = servicoMatricula.Concluir(id);

        if (resultado.IsFailed)
            TempData.AddErrorMessage(resultado);

        return RedirectToAction(nameof(Detalhes), new { id });
    }

    private void CarregarSelecaoListas()
    {
        List<ListarAlunosDto> alunos = servicoAluno.SelecionarTodos();
        List<ListarTurmaDto> turmas = servicoTurma.SelecionarTodos();

        ViewBag.Alunos = new SelectList(alunos, "Id", "Nome");
        ViewBag.Turmas = new SelectList(turmas, "Id", "Nome");
    }
}