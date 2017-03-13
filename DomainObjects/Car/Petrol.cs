using System;

namespace DomainObjects.Car
{
	public class Petrol
	{
		public decimal Price { get; set; }
		public double Amount { get; set; }
		public DateTime Date { get; set; }
		public GasolinStation GasolinStation { get; set; }
		public decimal BonusBalance { get; set; }
		public PetrolType PetrolType { get; set; }
	}

	public enum PetrolType
	{
		A95
	}
}