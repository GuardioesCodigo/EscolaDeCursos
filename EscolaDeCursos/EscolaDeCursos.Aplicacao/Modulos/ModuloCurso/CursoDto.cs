using EscolaDeCursos.Dominio.Modulos.ModuloCurso;

namespace EscolaDeCursos.Aplicacao.Modulos.ModuloCurso;

// Fica muito mais amigável na tela do usuário
public record ListarCursoDto(Guid Id, string Titulo, string Descricao, NivelCurso Nivel, string CategoriaNome);
public record CadastrarCursoDto(string Titulo, string Descricao, int CargaHoraria, NivelCurso Nivel, Guid CategoriaId);

public record EditarCursoDto(Guid Id, string Titulo, string Descricao, int CargaHoraria, NivelCurso Nivel, Guid CategoriaId);

public record DetalhesCursoDto(Guid Id, string Titulo, string Descricao, int CargaHoraria, NivelCurso Nivel, Guid CategoriaId, string CategoriaNome);