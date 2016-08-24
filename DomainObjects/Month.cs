using System;

namespace DomainObjects
{
	public class Month
	{
		public DateTime FirstDay { get; set; }
		public DateTime LastDay { get; set; }

		public bool IsDayOfTheMonth (DateTime day) => day >= FirstDay && day <= LastDay;
		public bool IsFinished () => LastDay > DateTime.Today;
		public bool IsStarted () => FirstDay <= DateTime.Today;
		public Month Previous () => new Month
		{
			FirstDay = FirstDay.AddMonths(-1),
			LastDay = LastDay.AddMonths(-1)
		};

		public Month Next () => new Month
		{
			FirstDay = FirstDay.AddMonths(1),
			LastDay = LastDay.AddMonths(1)
		};
	}
}
