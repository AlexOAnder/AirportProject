using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AirPortWebApi.Data.Application;
using AirPortWebApi.Data.DbContext;
using AirPortWebApi.Infrastructure.Interfaces;


namespace AirPortWebApi.BusinessLogic.Repositories
{
    public class DbContextContainer : IDbContextContainer
    {
        private readonly IEnumerable<IDbContext> _dbContexts;

        public DbContextContainer(ApplicationDbContext applicationDbContext,
            ApplicationLogsDbContext applicationLogsDbContext, Entities entities)
        {
            entities.Database.Log = LogEntityFrameworkQueries;
            _dbContexts = new List<IDbContext>
            {
            //    applicationDbContext,
            //    applicationLogsDbContext,
                entities,
            };
        }


        public IDbContext GetContextForEntityType(Type entityType)
        {
            var dbContext =
                _dbContexts.Select(ctx => new { Context = ctx, Types = GetContextType(ctx) })
                    .Where(x => x.Types != null && x.Types.Any(type => type != null && type == entityType.Name))
                    .Select(y => y.Context)
                    .FirstOrDefault();

            if (dbContext == null)
            {
                var contxetTypes = _dbContexts.Select(GetContextType)
                    .Aggregate(string.Empty,
                        (current1, types) =>
                            types.Where(t => t != null)
                                .Aggregate(current1, (current, type) => current + (type + Environment.NewLine)));
                throw new ApplicationException(string.Format("Can't find db context for entity type {0}{1}{2}{1}",
                    entityType.FullName, Environment.NewLine, contxetTypes));
            }
            return dbContext;
        }

        // IMPORTANT Different Dbcontext cannot contain entities with same name 
        private IEnumerable<string> GetContextType(IDbContext dbContext)
        {
            var tmp = ((IObjectContextAdapter) dbContext);
            var objectContext=  tmp.ObjectContext;
            var mdw = objectContext.MetadataWorkspace;
            var items = mdw.GetItems<EntityType>(DataSpace.CSpace);
            return items.Select(i => i.Name).ToList();
        }

        private static void LogEntityFrameworkQueries(string statementToLog)
        {
#if DEBUG
            Trace.WriteLine(string.Join(Environment.NewLine,
                statementToLog.Split(new[] { Environment.NewLine }, StringSplitOptions.None).Select(x => "\t" + x))
                .TrimEnd());
#endif
        }
    }
}