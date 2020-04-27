using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DataContext _Context;
        private DbSet<T> DbSet = null;
        public GenericRepository(DataContext dataContext)
        {
            _Context = dataContext;
            DbSet = _Context.Set<T>();
        }


        public void Delete(object id)
        {
            T existingEntity = GetById(id);
            DbSet.Remove(existingEntity);
        }

        public IEnumerable<T> GetAll()
        {
            return DbSet.ToList();
        }

        public T GetById(object id)
        {
            return DbSet.Find(id);
        }

        public void Insert(T entity)
        {
            DbSet.Add(entity);
        }

        public void Update(T entity)
        {
            _Context.Attach(entity);
            _Context.Entry(entity).State = EntityState.Modified;
        }
    }
}
