using System.Linq.Expressions;

namespace DotnetChat.Data
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private ChatContext db;

        public Repository(ChatContext chatContext)
        {
            db = chatContext;
        }

        public void Create(T entity)
        {
            db.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            db.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            db.Set<T>().Remove(entity);
        }

        public T? Get(Expression<Func<T, bool>> expression)
        {
            return Where(expression).FirstOrDefault();
        } 

        public IQueryable<T> Where(Expression<Func<T, bool>> expression)
        {
            return db
                .Set<T>()
                .Where(expression);
        }
    }
}
