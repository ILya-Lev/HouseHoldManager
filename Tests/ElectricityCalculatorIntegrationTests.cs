using BusinessLogic;
using DomainObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Globalization;
using System.Threading;

namespace Tests
{
	[TestClass]
	public class ElectricityCalculatorIntegrationTests
	{
		[TestInitialize]
		public void TestInitialize ()
		{
			Thread.CurrentThread.CurrentCulture = new CultureInfo("en-UA");
		}

		[TestMethod]
		public void PriceForTariff2014 ()
		{
			var calc = new ElectricityCalculator();
			var month = new Month
			{
				FirstDay = new DateTime(2014, 12, 01),
				LastDay = new DateTime(2014, 12, 31)
			};
			var price = calc.PriceForMonth(month);

			Console.WriteLine($"Price for {month.FirstDay.Month}-{month.FirstDay.Year} is {price.ToString("C")}.");
		}

		[TestMethod]
		public void PriceForTariff2015 ()
		{
			var calc = new ElectricityCalculator();
			var month = new Month
			{
				FirstDay = new DateTime(2015, 11, 01),
				LastDay = new DateTime(2015, 11, 30)
			};
			var price = calc.PriceForMonth(month);

			Console.WriteLine($"Price for {month.FirstDay.Month}-{month.FirstDay.Year} is {price.ToString("C")}.");
		}

		[TestMethod]
		public void PriceForTariff2016 ()
		{
			var calc = new ElectricityCalculator();
			var month = new Month
			{
				FirstDay = new DateTime(2016, 08, 01),
				LastDay = new DateTime(2016, 08, 31)
			};

			var price = calc.PriceForMonth(month);

			Console.WriteLine($"Price for {month.FirstDay.Month}-{month.FirstDay.Year} is {price.ToString("C")}.");
		}


		[TestMethod]
		public void PriceForNotStartedMonth_ShouldThrowAnException ()
		{
			var calc = new ElectricityCalculator();
			var month = new Month
			{
				FirstDay = new DateTime(2016, 11, 01),
				LastDay = new DateTime(2016, 11, 30)
			};

			try
			{
				var price = calc.PriceForMonth(month);
				Console.WriteLine($"Price for {month.FirstDay.Month}-{month.FirstDay.Year}" +
								  $" is {price.ToString("C")}.");
			}
			catch (ArgumentOutOfRangeException exc)
			{
				Console.WriteLine(exc.Message);
			}
		}
	}
}
