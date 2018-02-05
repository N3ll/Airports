using System;
using System.Linq.Expressions;

namespace ABS_v2.BusinessLogic.Interfaces.Repositories
{
    public interface IEntityRepository<T, E>
    {
        void Add(T entity);

        T GetEntity(Expression<Func<E, bool>> filter);
        void UpdateEntity(T entity);

    }
}
