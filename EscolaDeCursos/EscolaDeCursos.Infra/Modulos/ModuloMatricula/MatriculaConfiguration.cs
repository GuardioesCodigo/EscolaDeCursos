using System;
using EscolaDeCursos.Dominio.Modulos.ModuloAluno;
using EscolaDeCursos.Dominio.Modulos.ModuloMatricula;
using EscolaDeCursos.Dominio.Modulos.ModuloTurma;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EscolaDeCursos.Infra.Modulos.ModuloMatricula;

public class MatriculaConfiguration
{
    public void Configure(EntityTypeBuilder<Matricula> builder)
    {
        builder.ToTable("TBMatricula");

        builder.HasKey(m => m.Id);

        builder.Property(m => m.DataMatricula)
            .HasColumnName("DATA_MATRICULA")
            .HasColumnType("date")
            .IsRequired();

        builder.Property(m => m.Situacao)
            .HasColumnName("SITUACAO")
            .HasConversion<string>()
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(m => m.AlunoId)
            .HasColumnName("ALUNO_ID")
            .IsRequired();

        builder.Property(m => m.TurmaId)
            .HasColumnName("TURMA_ID")
            .IsRequired();

        builder.HasOne<Aluno>()
            .WithMany()
            .HasForeignKey(m => m.AlunoId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<Turma>()
            .WithMany()
            .HasForeignKey(m => m.TurmaId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
