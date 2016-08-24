using System;

namespace DomainObjects
{
	public class ElectricityTarif
	{
		public int Id { get; set; }
		public decimal Price { get; set; }      // in UAH per kWt/h

		public decimal AmountFrom { get; set; }     // in kWt/h in month; defines range lower band
		public decimal? AmountTo { get; set; }      // in kWt/h in month; defines range upper band

		public DateTime ApplicableSince { get; set; }
		public DateTime? ApplicableTill { get; set; }
	}
}
