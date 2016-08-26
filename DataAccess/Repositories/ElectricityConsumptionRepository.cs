using System.Collections.Generic;
using System.Linq;
using DomainObjects;
using DomainObjects.Electricity;

namespace DataAccess.Repositories
{
	public class ElectricityConsumptionRepository
	{
		private readonly Context _context;

		public ElectricityConsumptionRepository ()
		{
			_context = new Context();
		}

		public List<Consumption> OrderedMeasurementsPerMonth (Month month)
		{
			return _context.ElectricityConsumptions
					.Where(c => c.MeasurementTime >= month.FirstDay && c.MeasurementTime <= month.LastDay)
					.OrderBy(c => c.Id)
					.ToList();
		}

		public void AddMeasurement (Consumption measurement)
		{
			_context.ElectricityConsumptions.Add(measurement);
			_context.SaveChanges();
		}
	}
}
