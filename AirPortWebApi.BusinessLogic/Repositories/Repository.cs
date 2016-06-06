﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using AirPortWebApi.Data.DbContext;
using AirPortWebApi.Infrastructure.Interfaces;
using AirPortWebApi.Infrastructure.Repositories;


namespace AirPortWebApi.BusinessLogic.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private static readonly string LineSeparator = new string('*', 80);
        private static readonly string SourceInfoLineSeparator = new string('-', 80);

        private static readonly Action<string, string, int> BeforeLogQuery =
            (memberName, sourceFilePath, sourceLineNumber) =>
            {
#if DEBUG
                Trace.WriteLine(string.Empty);
                Trace.WriteLine(LineSeparator);
                Trace.WriteLine("Method: " + memberName);
                Trace.WriteLine("SourceFilePath: " + sourceFilePath);
                Trace.WriteLine("SourceLineNumber: " + sourceLineNumber);
                Trace.WriteLine(SourceInfoLineSeparator);
#endif
            };

        private static readonly Action AfterLogQuery = () =>
        {
#if DEBUG
            Trace.WriteLine(LineSeparator);
            Trace.WriteLine(string.Empty);
#endif
        };

        internal IDbContext Context;
        internal DbSet<TEntity> DbSet;

        public Repository(IDbContextContainer contextContainer)
        {
            Context = contextContainer.GetContextForEntityType(typeof(TEntity));
            DbSet = contextContainer.GetContextForEntityType(typeof(TEntity)).Set<TEntity>();
        }

        public void SaveChanges([CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
        {
            BeforeLogQuery(memberName, sourceFilePath, sourceLineNumber);
            Context.SaveChanges();
            AfterLogQuery();
        }

        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "", [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
        {
            IQueryable<TEntity> query = DbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            query = includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }

            BeforeLogQuery(memberName, sourceFilePath, sourceLineNumber);
            var result = query.ToList();
            AfterLogQuery();
            return result;
        }

        public IEnumerable<TEntity> GetReadOnly(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "", [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
        {
            IQueryable<TEntity> query = DbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }


            query = includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            BeforeLogQuery(memberName, sourceFilePath, sourceLineNumber);
            var result = orderBy?.Invoke(query).ToList() ?? query.AsNoTracking().ToList();
            AfterLogQuery();
            return result;
        }

        public void DetachAddedEntities()
        {
            foreach (var entry in ((DbContext)Context).ChangeTracker.Entries().Where(x => x.State == EntityState.Added))
            {
                entry.State = EntityState.Detached;
            }
        }

        public virtual TEntity GetById(object id)
        {
            return DbSet.Find(id);
        }

        public virtual void Add(TEntity entity)
        {
            //Trace.WriteLine/*If(typeof(TEntity).FullName != nameof(BannedIpAddress),/*typeof(IDbContext) == typeof(Entities),*/
            //	($"Add;{typeof(TEntity)};"
            //	+ $"CreatedBy:{Context.Entry(entity).Property("CreatedBy")?.CurrentValue};"
            //	+ $"CreatedOn:{Context.Entry(entity).Property("CreatedOn")?.CurrentValue};"
            //	+ $"UpdatedBy:{Context.Entry(entity).Property("UpdatedBy")?.CurrentValue};"
            //	+ $"UpdatedOn{Context.Entry(entity).Property("UpdatedOn")?.CurrentValue}");
            DbSet.Add(entity);
        }

        public virtual void Delete(object id)
        {
            TEntity entityToDelete = DbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(IEnumerable<TEntity> entitiesToDelete)
        {
            foreach (var entity in entitiesToDelete)
            {
                if (Context.Entry(entity).State == EntityState.Detached)
                {
                    DbSet.Attach(entity);
                }
                DbSet.Remove(entity);
            }
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (Context.Entry(entityToDelete).State == EntityState.Detached)
            {
                DbSet.Attach(entityToDelete);
            }
            DbSet.Remove(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            //DbSet.AddOrUpdate(entityToUpdate);
            DbSet.Attach(entityToUpdate);
            Context.Entry(entityToUpdate).State = EntityState.Modified;

            //Trace.WriteLine(/*If(typeof(TEntity).FullName != nameof(BannedIpAddress),/*typeof(IDbContext) == typeof(Entities),*/ 
            //	$"Update;{typeof(TEntity)};"
            //	+ $"CreatedBy:{Context.Entry(entityToUpdate).Property("CreatedBy")?.CurrentValue};"
            //	+ $"CreatedOn:{Context.Entry(entityToUpdate).Property("CreatedOn")?.CurrentValue};"
            //	+ $"UpdatedBy:{Context.Entry(entityToUpdate).Property("UpdatedBy")?.CurrentValue};"
            //	+ $"UpdatedOn{Context.Entry(entityToUpdate).Property("UpdatedOn")?.CurrentValue}");

            //UpdatedOn property of TravelAir and TravelGround was set in AttendeeTravelService
            
        }

        public void Dispose()
        {
            Context?.Dispose();
        }
    }
}
