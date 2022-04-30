using ApiFuncionarios.Infra.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiFuncionarios.Infra.Data.Mappings
{
    /// <summary>
    /// Classe de mapeamento ORM para funcionário
    /// </summary>
    public class FuncionarioMap : IEntityTypeConfiguration<Funcionario>
    {
        public void Configure(EntityTypeBuilder<Funcionario> builder)
        {
            //nome da tabela
            builder.ToTable("FUNCIONARIO");

            //chave primária
            builder.HasKey(f => f.IdFuncionario);

            //campos da tabela
            builder.Property(f => f.IdFuncionario)
                .HasColumnName("IDFUNCIONARIO")
                .IsRequired();

            builder.Property(f => f.Nome)
                .HasColumnName("NOME")
                .HasMaxLength(250)
                .IsRequired();

            builder.Property(f => f.Cpf)
                .HasColumnName("CPF")
                .HasMaxLength(14)
                .IsRequired();

            builder.HasIndex(f => f.Cpf)
                .IsUnique();

            builder.Property(f => f.Matricula)
                .HasColumnName("MATRICULA")
                .HasMaxLength(10)
                .IsRequired();

            builder.HasIndex(f => f.Matricula)
                .IsUnique();

            builder.Property(f => f.DataAdmissao)
                .HasColumnName("DATAADMISSAO")
                .HasColumnType("date")
                .IsRequired();
        }
    }
}



