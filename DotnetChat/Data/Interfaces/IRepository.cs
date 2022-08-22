using System.Linq.Expressions;

namespace DotnetChat.Data
{
    public interface IRepository<T>
    {
        public void Create(T entity);
        public void Update(T entity);
        public void Delete(T entity);

        public T? Get(Expression<Func<T, bool>> expression);
        public IQueryable<T> Where(Expression<Func<T, bool>> expression);
    }
}
