﻿using Microsoft.EntityFrameworkCore;
using Northwind.Domain;
using System;
using Northwind.Persistence;

namespace Northwind.Application.Tests
{
    public class TestBase
    {
        private bool useSqlite;

        public NorthwindDbContext GetDbContext()
        {
            var builder = new DbContextOptionsBuilder<NorthwindDbContext>();
            if (useSqlite)
            {
                // Use Sqlite DB.
                builder.UseSqlite("DataSource=:memory:", x => { });
            }
            else
            {
                // Use In-Memory DB.
                builder.UseInMemoryDatabase(Guid.NewGuid().ToString());
            }

            var dbContext = new NorthwindDbContext(builder.Options);
            if (useSqlite)
            {
                // SQLite needs to open connection to the DB.
                // Not required for in-memory-database and MS SQL.
                dbContext.Database.OpenConnection();
            }

            dbContext.Database.EnsureCreated();

            return dbContext;
        }

        public void UseSqlite()
        {
            useSqlite = true;
        }
    }
}
