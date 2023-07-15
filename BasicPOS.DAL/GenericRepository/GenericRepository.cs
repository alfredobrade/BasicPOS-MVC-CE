using BasicPOS.DAL.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BasicPOS.DAL.GenericRepository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly BasicPosPruebaContext _context;


        public GenericRepository(BasicPosPruebaContext context)
        {
            _context = context;
        }

        public async Task<TEntity> Obtener(Expression<Func<TEntity, bool>> filtro)
        {
            try
            {
                TEntity entity = await _context.Set<TEntity>().FirstOrDefaultAsync(filtro);

                return entity;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IQueryable<TEntity>> Consultar(Expression<Func<TEntity, bool>> filtro = null)
        {
            try
            {
                IQueryable<TEntity> queryEntity = filtro == null ? 
                    _context.Set<TEntity>() : 
                    _context.Set<TEntity>().Where(filtro); //que mierda es filtro ahi?

                return queryEntity.AsQueryable();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<TEntity> Crear(TEntity entity)
        {
            try
            {
                _context.Set<TEntity>().Add(entity);
                await _context.SaveChangesAsync();

                return entity;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> Editar(TEntity entity)
        {
            try
            {
                _context.Set<TEntity>().Update(entity); //TODO: tambien le hace _context.Update(entity); para que es el Set<TEntity> ????
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> Eliminar(TEntity entity)
        {
            try
            {
                _context.Set<TEntity>().Remove(entity);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        
    }
}
