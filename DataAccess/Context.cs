using DomainObjects;
using System.Data.Entity;

namespace DataAccess
{
	public class Context : DbContext
	{
		public DbSet<ElectricityTarif> ElectricityTarifs { get; set; }
		public DbSet<ElectricityConsumption> ElectricityConsumptions { get; set; }

	}
}
