using DataAccess.Interfaces;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Reposiotry
{
    public class UnitOfWork : IUnitOfWork
    {
        public DbContext Context { get; }

        public UnitOfWork(DemoDatabaseContext context)
        {
            Context = context;
        }
        public void Dispose()
        {
            Context.Dispose();

        }
    }
}
