using System;
using EscolaDeCursos.Dominio.Modulos.ModuloInstrutor;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EscolaDeCursos.Infra.Modulos.ModuloInstrutor;

public class InstrutorConfiguration : IEntityTypeConfiguration<Instrutor>
{
    public void Configure(EntityTypeBuilder<Instrutor> builder)
    {
        builder.ToTable("TBInstrutor");

        builder.HasKey(i => i.Id);

        builder.Property(i => i.Nome)
            .HasColumnName("NOME")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(i => i.Cpf)
            .HasColumnName("CPF")
            .HasMaxLength(14)
            .IsRequired();

        builder.Property(i => i.Email)
            .HasColumnName("EMAIL")
            .HasMaxLength(250)
            .IsRequired();

        builder.HasIndex(i => i.Email)
            .IsUnique()
            .HasDatabaseName("UQ_TBInstrutor_Email");

        builder.HasIndex(i => i.Cpf)
            .IsUnique()
            .HasDatabaseName("UQ_TBInstrutor_Cpf");
    }
}
