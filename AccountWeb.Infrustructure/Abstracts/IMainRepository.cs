using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AccountWeb.Infrustructure.Abstracts
{
    public interface IMainRepository<T> where T : class
    {
        Task<T> FindByIdAsync(int id);
        //Task<T> FindByIdAsync(int? id);
        T FindById(int id);
        T SelectOne(Expression<Func<T, bool>> match);
        bool IsExistsById(int id);
        Task<IEnumerable<T>> FindAllAsync();
        Task<IEnumerable<T>> FindAllAsync(params string[] agers);
        IEnumerable<T> FindAll(params string[] agers);

        bool IsExists(T entity);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);

        void AddList(IEnumerable<T> MyList);
        void UpdateList(IEnumerable<T> MyList);
        void DeleteList(IEnumerable<T> MyList);
        ////////
        Task DeleteRangeAsync(ICollection<T> entities);
        IDbContextTransaction BeginTransaction();
        void Commit();
        void RollBack();
       IQueryable<T>  GetTableNoTracking();
        IQueryable<T> GetTableAsTracking();
        Task AddRangeAsync(ICollection<T> entities);
        Task UpdateRangeAsync(ICollection<T> entities);
        Task<T> AddAsync(T entity);
        Task DeleteAsync(T entity);
        Task UpdateAsync(T entity);
    }
}
