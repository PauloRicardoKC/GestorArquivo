using GestorArquivo.Data.Contexto;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace GestorArquivo.Data.Repositorio
{
    public class RepositorioBase<TEntity> : IDisposable where TEntity : class
    {
        internal ContextoGestorArquivo _context;
        internal DbSet<TEntity> _dbSet;

        public RepositorioBase(ContextoGestorArquivo context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }


        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        #region CRUD

        public void Incluir(TEntity entidade)
        {
            _dbSet.Add(entidade);
            _context.SaveChanges();
        }

        public void Alterar(TEntity entidade)
        {
            _context.Entry(entidade).State = EntityState.Modified;
            _context.SaveChanges();
        }

        #endregion

        #region Consulta

        public IQueryable<TEntity> PegarCondicao(Expression<Func<TEntity, bool>> condicao)
        {
            return _context.Set<TEntity>().Where(condicao);
        }

        public List<TEntity> PegarListaFiltrada(Expression<Func<TEntity, bool>> expressao)
        {
            return _context.Set<TEntity>().Where(expressao).ToList();
        }

        public TEntity PegarPrimeiro(Expression<Func<TEntity, bool>> expressao)
        {
            return _context.Set<TEntity>().FirstOrDefault(expressao);
        }

        #endregion
    }
}
