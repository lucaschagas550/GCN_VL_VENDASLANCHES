using System.Linq.Expressions;
using VL_VendasLanches.Models;

namespace VL_VendasLanches.Repositories.Interfaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : Entity
    {
        Task Adicionar(TEntity entity);
        Task AdicionarLista(List<TEntity> entities);
        Task<TEntity> ObterPorId(Guid id);
        Task<List<TEntity>> ObterTodos();
        Task Atualizar(TEntity entity);
        Task Deletar(Guid id);
        Task DeletarLista(List<TEntity> entities);
        Task<IEnumerable<TEntity>> Buscar(Expression<Func<TEntity, bool>> predicate);
        Task<int> SaveChanges();
    }
}
