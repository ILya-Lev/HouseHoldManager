using DomainObjects.Electricity;
using System.Data.Entity;

namespace DataAccess
{
	public class Context : DbContext
	{
		public DbSet<Tarif> ElectricityTarifs { get; set; }
		public DbSet<Consumption> ElectricityConsumptions { get; set; }

		//public Context (string nameOrConnectionString =
		//	@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DataAccess.Context;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False")
		//	: base(nameOrConnectionString)
		//{
		//}
	}
}
