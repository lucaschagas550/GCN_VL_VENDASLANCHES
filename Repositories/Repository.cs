using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using VL_VendasLanches.Context;
using VL_VendasLanches.Models;
using VL_VendasLanches.Repositories.Interfaces;

namespace VL_VendasLanches.Repositories
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, new()
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<TEntity> _DbSet;

        protected Repository(AppDbContext db)
        {
            _context = db;
            _DbSet = db.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> Buscar(Expression<Func<TEntity, bool>> predicate)
        {
            return await _DbSet.AsNoTracking().Where(predicate).ToListAsync();
        }

        public virtual async Task<TEntity> ObterPorId(Guid id)
        {
            return await _DbSet.FindAsync(id);
        }

        public virtual async Task<List<TEntity>> ObterTodos()
        {
            return await _DbSet.ToListAsync();
        }

        public virtual async Task Adicionar(TEntity entity)
        {
            _DbSet.Add(entity);
            await SaveChanges();
        }

        public async Task AdicionarLista(List<TEntity> entities)
        {
            _DbSet.AddRange(entities);
            await SaveChanges();
        }

        public virtual async Task Atualizar(TEntity entity)
        {
            _DbSet.Update(entity);
            await SaveChanges();
        }

        public virtual async Task Deletar(Guid id)
        {
            _DbSet.Remove(new TEntity { Id = id });
            await SaveChanges();
        }

        public virtual async Task DeletarLista(List<TEntity> entities)
        {
            _DbSet.RemoveRange(entities);
            await SaveChanges();
        }
        public async Task<int> SaveChanges()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}