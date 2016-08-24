using DomainObjects;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess
{
	public class ElectricityConsumptionRepository
	{
		private readonly Context _context;

		public ElectricityConsumptionRepository ()
		{
			_context = new Context();
		}

		public List<ElectricityConsumption> OrderedMeasurementsPerMonth (Month month)
			=> _context.ElectricityConsumptions
				.Where(c => month.IsDayOfTheMonth(c.MeasurementTime))
				.OrderBy(c => c.Id)
				.ToList();

		public void AddMeasurement (ElectricityConsumption measurement)
		{
			_context.ElectricityConsumptions.Add(measurement);
			_context.SaveChanges();
		}
	}
}
