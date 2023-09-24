using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using OgrenciNotlariEL.ResultModels;

namespace OgrenciNotlariBL.InterfaceofManagers
{
    public interface IManager<T,Tid>
    {
        IDataResult<T> Add(T entity);
        IDataResult<T> Update(T entity);
        IResult Delete(T entity);
        IDataResult<ICollection<T>> GetAll(Expression<Func<T, bool>>? whereFilter = null, string[]? joinTablesName = null);



        IDataResult<T> GetByCondition(Expression<Func<T, bool>>? whereFilter = null, string[]? joinTablesName = null);


        IDataResult<T> GetbyId(Tid id);
    }
}
