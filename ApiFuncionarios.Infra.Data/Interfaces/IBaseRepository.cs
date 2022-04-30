using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiFuncionarios.Infra.Data.Interfaces
{
    /// <summary>
    /// Interface genérica do repositório
    /// </summary>
    public interface IBaseRepository<TEntity>
        where TEntity : class
    {
        void Inserir(TEntity obj);
        void Alterar(TEntity obj);
        void Excluir(TEntity obj);

        List<TEntity> Consultar();
        TEntity ObterPorId(Guid id);
    }
}



