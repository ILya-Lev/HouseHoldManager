using DataAccess.Repositories;
using DomainObjects;
using DomainObjects.Electricity;
using MathNet.Numerics;
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

		//for testing purposes
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
					//throw new ArgumentOutOfRangeException(nameof(month), "The month is not finished yet and there is only one record in the system. Please, go and take another one!");
					return InterpolateViaRecordAndAmbientMonthes(month, consumption.First());
			}

			return ExtrapolateForWholeMonth(month, consumption);
		}

		/// <summary>
		/// performs linear extrapolation - tg(phi) = dy/dx => dy' = dx' * tg(phi)
		/// where dx = last measurement date - first measurement date
		/// dx' - last day of the month - first day of the month
		/// dy - last measurement value - first measurement value
		/// dy' - approximate difference between measurements in beginning and ending of the month
		/// </summary>
		/// <param name="month">the month calculations are performed for</param>
		/// <param name="consumption">a list of measurements performed during the month</param>
		/// <returns>amount of money should be paid for the month</returns>
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

		private decimal InterpolateViaAmbientMonthes (Month month)
		{
			var previousMeasurements = _consumptionRepo.OrderedMeasurementsPerMonth(month.Previous());
			var nextMeasurements = _consumptionRepo.OrderedMeasurementsPerMonth(month.Next());

			var overallMeasurements = previousMeasurements.Concat(nextMeasurements);

			return PriceForMeasurementInterpolation(month, overallMeasurements);
		}

		private decimal InterpolateViaRecordAndAmbientMonthes (Month month, Consumption theOnlyConsumption)
		{
			var previousMeasurements = _consumptionRepo.OrderedMeasurementsPerMonth(month.Previous());
			var nextMeasurements = _consumptionRepo.OrderedMeasurementsPerMonth(month.Next());

			previousMeasurements.Add(theOnlyConsumption);

			var overallMeasurements = previousMeasurements.Concat(nextMeasurements);

			return PriceForMeasurementInterpolation(month, overallMeasurements);
		}

		private decimal PriceForMeasurementInterpolation (Month month, IEnumerable<Consumption> overallMeasurements)
		{
			var totalMeasurements = overallMeasurements
				.Select(m => new
				{
					TimePoint = (m.MeasurementTime - DateTime.Today).TotalDays,
					Value = (double) m.MeterReadings
				}).ToList();

			var coefficients = Fit.Line(totalMeasurements.Select(item => item.TimePoint).ToArray(),
				totalMeasurements.Select(item => item.Value).ToArray());
			//part 'coefficients.Item1 + ' is redundant, 'cause we need substitution only
			var firstDayValue = coefficients.Item2 * (month.FirstDay - DateTime.Today).TotalDays;
			var lastDayValue = coefficients.Item2 * (month.LastDay - DateTime.Today).TotalDays;

			var consumedPerTheMonth = (decimal) (lastDayValue - firstDayValue);

			var firstTarif = _tarifRepo.InForceAt(month.FirstDay);

			return (firstTarif ?? _tarifRepo.InForceAt(month.LastDay)).CalculatePrice(consumedPerTheMonth);
		}
	}
}
