using AccountWeb.Infrustructure.Abstracts;
using AccountWeb.Infrustructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AccountWeb.Infrustructure.Repositories
{
    public class MainRepository<T>:IMainRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;

        public MainRepository(ApplicationDbContext context)
        {
            this._context = context;
        }
        public virtual async Task<T> FindByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public virtual T SelectOne(Expression<Func<T, bool>> match)
        {
            //return context.Set<T>().SingleOrDefault(match);
            var results = _context.Set<T>().Where(match).ToList();

            if (results.Count > 0)
            {
                return results.First();
            }
            return default(T);
        }

        public virtual async Task<IEnumerable<T>> FindAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }
        //////////////////////////////////////////////

        public virtual void Add(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
        }

        public virtual void Update(T entity)
        {
            _context.Set<T>().Update(entity);
            _context.SaveChanges();
        }

        public virtual void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
        }

        public virtual void AddList(IEnumerable<T> MyList)
        {
            _context.Set<T>().AddRange(MyList);
            _context.SaveChanges();
        }

        public virtual void UpdateList(IEnumerable<T> MyList)
        {
            _context.Set<T>().UpdateRange(MyList);
            _context.SaveChanges();
        }

        public virtual void DeleteList(IEnumerable<T> MyList)
        {
            _context.Set<T>().RemoveRange(MyList);
            _context.SaveChanges();
        }

        public virtual bool IsExists(T entity)
        {
            return _context.Set<T>().Any(x => x.Equals(entity));
            //return context.Set<T>().FirstOrDefault(x =>x.Equals(entity)) != null;
        }
        public virtual bool IsExistsById(int id)
        {
            return _context.Set<T>().Find(id) != null;
        }

        public virtual T FindById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public virtual async Task<IEnumerable<T>> FindAllAsync(params string[] agers)
        {
            IQueryable<T> query = _context.Set<T>();
            if (agers.Length > 0)
            {
                foreach (var ager in agers)
                {
                    query = query.Include(ager);
                }

            }
            return await query.ToListAsync();
        }

        public virtual IEnumerable<T> FindAll(params string[] agers)
        {
            IQueryable<T> query = _context.Set<T>();
            if (agers.Length > 0)
            {
                foreach (var ager in agers)
                {
                    query = query.Include(ager);
                }

            }
            return query.ToList();
        }
        public IQueryable<T> GetTableNoTracking()
        {
            return  _context.Set<T>().AsNoTracking().AsQueryable();
        }


        public virtual async Task AddRangeAsync(ICollection<T> entities)
        {
            await _context.Set<T>().AddRangeAsync(entities);
            await _context.SaveChangesAsync();

        }

        public virtual async Task DeleteRangeAsync(ICollection<T> entities)
        {
            foreach (var entity in entities)
            {
                _context.Entry(entity).State = EntityState.Deleted;
            }
            await _context.SaveChangesAsync();
        }
        public IDbContextTransaction BeginTransaction()
        {


            return _context.Database.BeginTransaction();
        }

        public void Commit()
        {
            _context.Database.CommitTransaction();

        }

        public void RollBack()
        {
            _context.Database.RollbackTransaction();

        }

        public IQueryable<T> GetTableAsTracking()
        {
            return _context.Set<T>().AsQueryable();

        }

        public virtual async Task UpdateRangeAsync(ICollection<T> entities)
        {
            _context.Set<T>().UpdateRange(entities);
            await _context.SaveChangesAsync();
        }
        public virtual async Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }
        public virtual async Task UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();

        }
        public virtual async Task<T> AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity;
        }
    }
}
