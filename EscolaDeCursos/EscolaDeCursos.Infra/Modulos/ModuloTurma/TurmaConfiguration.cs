using EscolaDeCursos.Dominio.Modulos.ModuloCurso;
using EscolaDeCursos.Dominio.Modulos.ModuloInstrutor;
using EscolaDeCursos.Dominio.Modulos.ModuloTurma;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EscolaDeCursos.Infraestrutura.Orm.Modulos.ModuloTurma;

public class TurmaConfiguration : IEntityTypeConfiguration<Turma>
{
    public void Configure(EntityTypeBuilder<Turma> builder)
    {
        builder.ToTable("TBTurma");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Nome)
            .HasColumnName("NOME")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(t => t.DataInicio)
            .HasColumnName("DATA_INICIO")
            .HasColumnType("date")
            .IsRequired();

        builder.Property(t => t.DataTermino)
            .HasColumnName("DATA_TERMINO")
            .HasColumnType("date")
            .IsRequired();

        builder.Property(t => t.CapacidadeAlunos)
            .HasColumnName("CAPACIDADE_ALUNOS")
            .IsRequired();

        builder.Property(t => t.CursoId)
            .HasColumnName("CURSO_ID")
            .IsRequired();

        builder.Property(t => t.InstrutorId)
            .HasColumnName("INSTRUTOR_ID")
            .IsRequired();

        builder.HasOne<Curso>()
            .WithMany()
            .HasForeignKey(t => t.CursoId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<Instrutor>()
            .WithMany()
            .HasForeignKey(t => t.InstrutorId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}