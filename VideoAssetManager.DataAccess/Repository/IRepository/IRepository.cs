using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace VideoAssetManager.DataAccess.Repository.IRepository
{
    public interface IRepository <T> where T:class
    {
        T GetFirstOrDefault(Expression<Func<T, bool>> Filter, string? includeParameters = null, bool tracked = true);
        IEnumerable<T> GetAll();
        void Add(T entity);
        void Update(T entity); 
        void Remove(T entity);
        void ToList(T entity);
        void ToListAsync(T entity);
        //Task<ActionResult<IEnumerable<T>>> GetAllAsync();
        //Task<ActionResult<IEnumerable<T>>> GetAllAsync();
        //void GetAllAsync();
    }
}
