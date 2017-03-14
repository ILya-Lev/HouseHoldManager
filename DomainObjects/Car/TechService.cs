using System;

namespace DomainObjects.Car
{
	public class TechService
	{
		public int Id { get; set; }
		public string Action { get; set; }
		public DateTime Date { get; set; }
		public decimal MaterialPrice { get; set; }
		public decimal WorkPrice { get; set; }
		public ServiceStatus Status { get; set; }
	}

	public enum ServiceStatus
	{
		Completed,
		Pending
	}
}