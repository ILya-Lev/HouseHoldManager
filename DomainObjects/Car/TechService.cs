using System;

namespace DomainObjects.Car
{
	public class TechService
	{
		public string Action { get; set; }
		public DateTime Date { get; set; }
		public decimal MaterialPrice { get; set; }
		public decimal WorkPrice { get; set; }
		public Status Status { get; set; }
	}

	public enum Status
	{
		Completed,
		Pending
	}
}