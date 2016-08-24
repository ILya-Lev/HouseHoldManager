using DomainObjects.Electricity;
using System.Data.Entity;

namespace DataAccess
{
	public class Context : DbContext
	{
		public DbSet<Tarif> ElectricityTarifs { get; set; }
		public DbSet<Consumption> ElectricityConsumptions { get; set; }

	}
}
