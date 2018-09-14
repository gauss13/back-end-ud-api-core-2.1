using Contracts;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {

        protected AppDbContext AppContext { get; set; }

        public RepositoryBase(AppDbContext appContext)
        {
            this.AppContext = appContext;
        }


        public async Task<IEnumerable<T>> FindAllAsync()
        {
            return await this.AppContext.Set<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> FindByConditionAsync(Expression<Func<T, bool>> expression)
        {
            return await this.AppContext.Set<T>().
                Where(expression).ToListAsync();
        }

        public void Create(T entity)
        {
            this.AppContext.Set<T>().Add(entity);           
        }

        public void Delete(T entity)
        {
            this.AppContext.Set<T>().Remove(entity);
        }
    

        public async Task SaveAsync()
        {
          await  this.AppContext.SaveChangesAsync();
        }

        public void Update(T entity)
        {
            this.AppContext.Set<T>().Update(entity);
        }
    }
}
