using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Store.Core.DataLayer.Mapping;

namespace Store.Core.DataLayer
{
    public class StoreDbContext : Microsoft.EntityFrameworkCore.DbContext
	{
		public StoreDbContext(IOptions<AppSettings> appSettings, IEntityMapper entityMapper)
		{
			ConnectionString = appSettings.Value.ConnectionString;
			EntityMapper = entityMapper;
		}

		public String ConnectionString { get; }

		public IEntityMapper EntityMapper { get; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer(ConnectionString);
			
			base.OnConfiguring(optionsBuilder);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			EntityMapper.MapEntities(modelBuilder);
			
			base.OnModelCreating(modelBuilder);
		}
	}
}
