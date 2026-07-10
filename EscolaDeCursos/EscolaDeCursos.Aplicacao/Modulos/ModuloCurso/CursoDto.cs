using EscolaDeCursos.Dominio.Modulos.ModuloCurso;

namespace EscolaDeCursos.Aplicacao.Modulos.ModuloCurso;

public record ListarCursoDto(Guid Id, string Titulo, string Descricao, NivelCurso Nivel);

public record CadastrarCursoDto(string Titulo, string Descricao, int CargaHoraria, NivelCurso Nivel, Guid CategoriaId);

public record EditarCursoDto(Guid Id, string Titulo, string Descricao, int CargaHoraria, NivelCurso Nivel, Guid CategoriaId);

public record DetalhesCursoDto(Guid Id, string Titulo, string Descricao, int CargaHoraria, NivelCurso Nivel, Guid CategoriaId);
