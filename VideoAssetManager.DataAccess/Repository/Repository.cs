using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VideoAssetManager.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace VideoAssetManager.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly VideoAssetManagerDBContext _VideoAssetManagerDBContext;
        internal  DbSet<T> dbSet;
        public Repository(VideoAssetManagerDBContext VideoAssetManagerDBContext)
        {
            _VideoAssetManagerDBContext = VideoAssetManagerDBContext;
            this.dbSet = _VideoAssetManagerDBContext.Set<T>();
        }

        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public IEnumerable<T> GetAll()
        {
            IQueryable<T> query = dbSet;
            return query.ToList();
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> Filter, string includeParameters = null, bool tracked = true)
        {
            //throw new NotImplementedException();
            IQueryable<T> query = dbSet;
            return query.FirstOrDefault();
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public void ToList(T entity)
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            dbSet.Update(entity);
        }
        public void ToListAsync(T entity)
        {
            throw new NotImplementedException();
        }
        //public async Task<ActionResult<IEnumerable<T>>> GetAllAsync()
        //{
        //    IQueryable<T> query = dbSet;
        //    return await query.ToListAsync();
        //}
        //public void GetAllAsync()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
