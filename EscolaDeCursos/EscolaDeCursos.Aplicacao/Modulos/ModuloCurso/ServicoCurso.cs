using EscolaDeCursos.Dominio.Modulos.ModuloCurso;
using EscolaDeCursos.Dominio.Modulos.ModuloCategoria;
using EscolaDeCursos.Dominio.Modulos.ModuloTurma; // Certifique-se de importar o namespace da Turma

namespace EscolaDeCursos.Aplicacao.Modulos.ModuloCurso
{
    public class ServicoCurso
    {
        private readonly IRepositorioCurso _repositorioCurso;
        private readonly IRepositorioCategoria _repositorioCategoria;
        private readonly IRepositorioTurma _repositorioTurma;

        public ServicoCurso(
            IRepositorioCurso repositorioCurso, 
            IRepositorioCategoria repositorioCategoria, 
            IRepositorioTurma repositorioTurma)
        {
            _repositorioCurso = repositorioCurso;
            _repositorioCategoria = repositorioCategoria;
            _repositorioTurma = repositorioTurma;
        }

        public void Cadastrar(Curso novoCurso)
        {
            var erros = novoCurso.Validar();

            var categoria = _repositorioCategoria.SelecionarPorId(novoCurso.CategoriaId);
            if (categoria == null)
                erros.Add("A categoria selecionada não existe.");

            if (erros.Count > 0)
                throw new Exception(string.Join(" | ", erros));

            _repositorioCurso.Cadastrar(novoCurso);
        }

        public void Editar(Curso cursoAtualizado)
        {
            var erros = cursoAtualizado.Validar();

            var categoria = _repositorioCategoria.SelecionarPorId(cursoAtualizado.CategoriaId);
            if (categoria == null)
                erros.Add("A categoria selecionada não existe.");

            if (erros.Count > 0)
                throw new Exception(string.Join(" | ", erros));

            // Ajustado para passar o ID e o objeto atualizado (padrão de interface genérica)
            _repositorioCurso.Editar(cursoAtualizado.Id, cursoAtualizado);
        }

        public void Excluir(Guid id)
        {
            // if (_repositorioTurma.CursoEstaSendoUsado(id))
            //     throw new Exception("Não é possível excluir um curso que possui turmas vinculadas.");

            _repositorioCurso.Excluir(id);
        }
    }
}