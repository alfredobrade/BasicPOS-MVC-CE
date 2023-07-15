using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Linq.Expressions;

namespace BasicPOS.DAL.GenericRepository
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<TEntity> Obtener(Expression<Func<TEntity, bool>> filtro);
        Task<IQueryable<TEntity>> Consultar(Expression<Func<TEntity, bool>> filtro = null);
        Task<TEntity> Crear(TEntity entity);
        Task<bool> Editar(TEntity entity);
        Task<bool> Eliminar(TEntity entity);
        
    }
}
