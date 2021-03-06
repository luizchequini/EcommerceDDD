﻿using Domain.Interfaces.Generics;
using Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Infrastructure.Repository.Generics
{
    public class RepositoryGenerics<T> : IGeneric<T>, IDisposable where T : class
    {
        private readonly DbContextOptions<ContextBase> _dbContextOptions;

        public RepositoryGenerics()
        {
            _dbContextOptions = new DbContextOptions<ContextBase>();
        }

        public async Task Add(T Objeto)
        {
            using (var data = new ContextBase(_dbContextOptions))
            {
                await data.Set<T>().AddAsync(Objeto);
                await data.SaveChangesAsync();
            }
        }

        public async Task Delete(T Objeto)
        {
            using (var data = new ContextBase(_dbContextOptions))
            {
                data.Set<T>().Remove(Objeto);
                await data.SaveChangesAsync();
            }
        }

        public async Task<T> GetEntityById(int id)
        {
            using (var data = new ContextBase(_dbContextOptions))
            {
                return await data.Set<T>().FindAsync(id);                
            }
        }

        public async Task<List<T>> List()
        {
            using (var data = new ContextBase(_dbContextOptions))
            {
                return await data.Set<T>().AsNoTracking().ToListAsync();                
            }
        }

        public async Task Update(T Objeto)
        {
            using (var data = new ContextBase(_dbContextOptions))
            {
                data.Set<T>().Update(Objeto);
                await data.SaveChangesAsync();
            }
        }


        #region Disposed Microsoft Referência

        bool disposed = false;
        SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                handle.Dispose();
            }
            disposed = true;
        }

        #endregion
    }
}
