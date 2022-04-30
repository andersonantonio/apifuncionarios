using ApiFuncionarios.Infra.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiFuncionarios.Infra.Data.Interfaces
{
    /// <summary>
    /// Interface de repositório específica para Funcionario
    /// </summary>
    public interface IFuncionarioRepository : IBaseRepository<Funcionario>
    {
        Funcionario ConsultarPorCpf(string cpf);
        Funcionario ConsultarPorMatricula(string matricula);
    }
}





