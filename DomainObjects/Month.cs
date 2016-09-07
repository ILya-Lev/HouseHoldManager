using System;

namespace DomainObjects
{
	public class Month
	{
		public DateTime FirstDay { get; set; }
		public DateTime LastDay { get; set; }

		public bool IsDayOfTheMonth (DateTime day) => day >= FirstDay && day <= LastDay;
		public bool IsFinished () => LastDay < DateTime.Today;
		public bool IsStarted () => FirstDay <= DateTime.Today;
		public Month Previous () => new Month
		{
			FirstDay = FirstDay.AddMonths(-1),
			LastDay = FirstDay.AddDays(-1)
		};

		public Month Next () => new Month
		{
			FirstDay = FirstDay.AddMonths(1),
			LastDay = FirstDay.AddMonths(2).AddDays(-1)
		};

		public static Month From (DateTime dt) => new Month
		{
			FirstDay = new DateTime(dt.Year, dt.Month, 1),
			LastDay = new DateTime(dt.Year, dt.Month, 1).AddMonths(1).AddDays(-1)
		};
	}
}
