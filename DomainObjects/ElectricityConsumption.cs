using System;

namespace DomainObjects
{
	public class ElectricityConsumption
	{
		public int Id { get; set; }
		public decimal MeterReadings { get; set; }
		public DateTime MeasurementTime { get; set; }
	}
}
