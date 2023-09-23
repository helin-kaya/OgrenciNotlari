using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OgrenciNotlariDL.InterfaceofRepos
{
    public interface IRepository<T, Tid> where T : class, new()
    {
        int Add(T entity);
        int Update(T entity);
        int Delete(T entity);
        IQueryable<T> GetAll(Expression<Func<T, bool>>? whereFilter = null, string[]? joinTablesName = null);

        T GetByCondition(Expression<Func<T, bool>>? whereFilter = null, string[]? joinTablesName = null);

        T GetById(Tid id);

    }
}
