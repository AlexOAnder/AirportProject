using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using AirPortWebApi.Infrastructure.Interfaces;

// ReSharper disable InconsistentNaming
// ReSharper disable once CheckNamespace

namespace AirPortWebApi.Data.DbContext
{
    // need for the IDbContext interfeace extend
    public partial class Entities : IDbContext
    {

    }
}
