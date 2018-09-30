using GestorArquivo.Data.Contexto;
using System;

namespace GestorArquivo.Data.Repositorio
{
    public class UnitOfWork<TEntity> : IDisposable where TEntity : class
    {
        private readonly ContextoGestorArquivo _context = new ContextoGestorArquivo();

        private RepositorioBase<TEntity> _baseRepositorio;

        public RepositorioBase<TEntity> BaseRepositorio
        {
            get
            {
                if (this._baseRepositorio == null)
                {
                    this._baseRepositorio = new RepositorioBase<TEntity>(_context);
                }

                return _baseRepositorio;
            }
        }

        private bool _disposed = false;

        public void Dispose()
        {
            Clear(true);
            GC.SuppressFinalize(this);
        }

        private void Clear(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        ~UnitOfWork()
        {
            Clear(false);
        }
    }
}
