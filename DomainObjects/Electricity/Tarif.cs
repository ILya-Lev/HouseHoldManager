using System;
using System.Collections.Generic;
using System.Linq;

namespace DomainObjects.Electricity
{
	public class Tarif
	{
		public int Id { get; set; }

		public List<ConsumptionRange> ConsumptionRanges { get; set; }

		public DateTime ApplicableSince { get; set; }
		public DateTime? ApplicableTill { get; set; }

		public decimal CalculatePrice (decimal consumedPower)
		{
			var orderedRanges = ConsumptionRanges.OrderBy(r => r.AmountFrom);
			var total = 0.0m;
			foreach (var range in orderedRanges)
			{
				if (range.AmountTo == null || consumedPower < range.AmountTo)
				{
					total += (consumedPower - range.AmountFrom) * range.Price;
					break;
				}
				total += (range.AmountTo.Value - range.AmountFrom) * range.Price;
			}

			return total;
		}
	}
}
