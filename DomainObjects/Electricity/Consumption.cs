using System;

namespace DomainObjects.Electricity
{
	public class Consumption
	{
		public int Id { get; set; }
		public decimal MeterReadings { get; set; }
		public DateTime MeasurementTime { get; set; }
	}
}
