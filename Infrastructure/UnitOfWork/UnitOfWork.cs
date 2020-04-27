using Core.Interfaces;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.UnitOfWork
{
    public class UnitOfWork<T> : IUnitOfWork<T> where T : class
    {
        private readonly DataContext _Context;
        private IGenericRepository<T> _Entity;

        public UnitOfWork(DataContext dataContext)
        {
            _Context = dataContext;
        }

        public IGenericRepository<T> Entity {
            get
            {
                return _Entity ?? (_Entity = new GenericRepository<T>(_Context));
            }        
        }

        public void Save()
        {
            _Context.SaveChanges();
        }
    }
}
