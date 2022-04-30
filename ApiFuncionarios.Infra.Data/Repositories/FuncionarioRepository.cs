using ApiFuncionarios.Infra.Data.Contexts;
using ApiFuncionarios.Infra.Data.Entities;
using ApiFuncionarios.Infra.Data.Interfaces;
using Dapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiFuncionarios.Infra.Data.Repositories
{
    public class FuncionarioRepository : BaseRepository<Funcionario>, IFuncionarioRepository
    {
        //atributo
        private readonly SqlServerContext _sqlServerContext;

        //construtor para injeção de dependência (inicialização)
        public FuncionarioRepository(SqlServerContext sqlServerContext) : base(sqlServerContext)
        {
            _sqlServerContext = sqlServerContext;
        }

        public Funcionario ConsultarPorCpf(string cpf)
        {
            /*
            //LAMBDA
            return _sqlServerContext.Funcionario
                .Where(f => f.Cpf.Equals(cpf))
                .FirstOrDefault();
            */

            var query = @"
                SELECT * FROM FUNCIONARIO
                WHERE CPF = @cpf
            ";

            return _sqlServerContext.Database
                .GetDbConnection()
                .Query<Funcionario>(query, new { cpf }) //DAPPER!
                .FirstOrDefault();
        }

        public Funcionario ConsultarPorMatricula(string matricula)
        {
            /*
            //LAMBDA
            return _sqlServerContext.Funcionario
                .Where(f => f.Matricula.Equals(matricula))
                .FirstOrDefault();
            */

            var query = @"
                SELECT * FROM FUNCIONARIO
                WHERE MATRICULA = @matricula
            ";

            return _sqlServerContext.Database
                .GetDbConnection()
                .Query<Funcionario>(query, new { matricula }) //DAPPER!
                .FirstOrDefault();
        }
    }
}



