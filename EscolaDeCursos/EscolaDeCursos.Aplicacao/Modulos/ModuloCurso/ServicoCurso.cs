// using EscolaDeCursos.Dominio.Modulos.ModuloCurso;
// using EscolaDeCursos.Dominio.Modulos.ModuloCategoria; // Importante para validações

// namespace EscolaDeCursos.Aplicacao.Modulos.ModuloCurso
// {
//     public class ServicoCurso
//     {
//         private readonly IRepositorioCurso _repositorioCurso;
//         private readonly IRepositorioCategoria _repositorioCategoria;
//         // Futuramente, você precisará injetar o IRepositorioTurma aqui para a regra de exclusão

//         public ServicoCurso(IRepositorioCurso repositorioCurso, IRepositorioCategoria repositorioCategoria)
//         {
//             _repositorioCurso = repositorioCurso;
//             _repositorioCategoria = repositorioCategoria;
//         }

//         public void Cadastrar(Curso novoCurso)
//         {
//             var erros = novoCurso.Validar();

//             // Regra Extra: Verificar se a Categoria informada existe
//             var categoria = _repositorioCategoria.SelecionarPorId(novoCurso.CategoriaId);
//             if (categoria == null)
//                 erros.Add("A categoria selecionada não existe.");

//             if (erros.Count > 0)
//                 throw new Exception(string.Join(" | ", erros));

//             _repositorioCurso.Cadastrar(novoCurso);
//         }

//         public void Excluir(Guid id)
//         {
//             if (_repositorioTurma.CursoEstaSendoUsado(id))
//                 throw new Exception("Não é possível excluir um curso que possui turmas vinculadas.");

//             _repositorioCurso.Excluir(id);
//         }

//         public void Editar(Curso cursoAtualizado)
//         {
//             var erros = cursoAtualizado.Validar();

//             var categoria = _repositorioCategoria.SelecionarPorId(cursoAtualizado.CategoriaId);
//             if (categoria == null)
//                 erros.Add("A categoria selecionada não existe.");

//             if (erros.Count > 0)
//                 throw new Exception(string.Join(" | ", erros));

//             _repositorioCurso.Editar(cursoAtualizado);
//         }

//         public void Excluir(Guid id)
//         {
//             if (_repositorioTurma.CursoEstaSendoUsado(id))
//                 throw new Exception("Não é possível excluir um curso que possui turmas vinculadas.");
                
//             _repositorioCurso.Excluir(id);
//         }
//     }
// }