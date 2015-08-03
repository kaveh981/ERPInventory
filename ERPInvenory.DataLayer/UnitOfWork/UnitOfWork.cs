﻿using ERPInvetnory.DataLayer.Repository;
using ERPInventory.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPInvetnory.DataLayer
{

    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private ERPInventoryDBContext entities;
        private Dictionary<Type, object> _repositories;
        private bool disposed;
        public UnitOfWork()
        {
            entities = new ERPInventoryDBContext();
            _repositories = new Dictionary<Type, object>();
            disposed = false;
        }



        public IGenericRepository<T> Repository<T>() where T : class
        {
            if (_repositories.Keys.Contains(typeof(T)) == true)
            {
                return _repositories[typeof(T)] as GenericRepository<T>;
            }
            IGenericRepository<T> repo = new GenericRepository<T>(entities);
            _repositories.Add(typeof(T), repo);
            return repo;
        }

        public void Save()
        {
            entities.SaveChanges();
        }



        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    entities.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~UnitOfWork()
        {
            Dispose(false);
        }
    }

}
