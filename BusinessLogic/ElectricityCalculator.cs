using DataAccess;
using DomainObjects;
using DomainObjects.Electricity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic
{
	public class ElectricityCalculator
	{
		private readonly ElectricityTarifsRepository _tarifRepo;
		private readonly ElectricityConsumptionRepository _consumptionRepo;

		public ElectricityCalculator ()
		{
			_tarifRepo = new ElectricityTarifsRepository();
			_consumptionRepo = new ElectricityConsumptionRepository();
		}

		public ElectricityCalculator (ElectricityTarifsRepository tarifRepo, ElectricityConsumptionRepository consumptionRepo)
		{
			_tarifRepo = tarifRepo;
			_consumptionRepo = consumptionRepo;
		}

		public decimal PriceForMonth (Month month)
		{
			var consumption = _consumptionRepo.OrderedMeasurementsPerMonth(month);
			if (consumption.Count == 0)
			{
				if (!month.IsFinished())
					throw new ArgumentOutOfRangeException(nameof(month), "The month is not finished yet and there is no measurements present in the system. Go and take meter readings!");
				if (!month.IsStarted())
					throw new ArgumentOutOfRangeException(nameof(month), "The month is not started yet. There could not be any records. Be patient!");
				return InterpolateViaAmbientMonthes(month);
			}

			if (consumption.Count == 1)
			{
				if (!month.IsFinished())
					throw new ArgumentOutOfRangeException(nameof(month), "The month is not finished yet and there is only one record in the system. Please, go and take another one!");
				return InterpolateViaRecordAndAmbientMonthes(month, consumption.First());
			}

			return ExtrapolateForWholeMonth(month, consumption);
		}

		private decimal ExtrapolateForWholeMonth (Month month, List<Consumption> consumption)
		{
			var startTarif = _tarifRepo.InForceAt(month.FirstDay);
			var endTarif = _tarifRepo.InForceAt(month.LastDay);

			var measuredConsumedPower = consumption.Last().MeterReadings - consumption.First().MeterReadings;
			var totalDaysInterval = month.LastDay.Day - month.FirstDay.Day;
			var daysBetweenMeasure = consumption.Last().MeasurementTime.Day -
									 consumption.First().MeasurementTime.Day;

			if (startTarif.Id == endTarif.Id)
			{
				var totalConsumedPower = totalDaysInterval * measuredConsumedPower / daysBetweenMeasure;
				return startTarif.CalculatePrice(totalConsumedPower);
			}

			//suppose there could be 2 tariffs per month at most
			var consumedByFirstTariff =
			(endTarif.ApplicableSince.Day - month.FirstDay.Day) * measuredConsumedPower / daysBetweenMeasure;

			var consumedBySecondTariff =
			(month.LastDay.Day - endTarif.ApplicableSince.Day) * measuredConsumedPower / daysBetweenMeasure;

			return startTarif.CalculatePrice(consumedByFirstTariff) +
				   endTarif.CalculatePrice(consumedBySecondTariff);
		}

		private decimal InterpolateViaRecordAndAmbientMonthes (Month month, Consumption theOnlyConsumption)
		{
			throw new NotImplementedException();
		}

		private decimal InterpolateViaAmbientMonthes (Month month)
		{
			throw new NotImplementedException();
		}
	}
}
