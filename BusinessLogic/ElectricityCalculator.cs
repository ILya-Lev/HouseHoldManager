using DataAccess;
using DomainObjects;
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

		private decimal ExtrapolateForWholeMonth (Month month, List<ElectricityConsumption> consumption)
		{
			if (consumption.First().MeasurementTime.Day == month.FirstDay.Day
				&& consumption.Last().MeasurementTime.Day == month.LastDay.Day)
			{

			}
		}

		private decimal InterpolateViaRecordAndAmbientMonthes (Month month, ElectricityConsumption theOnlyConsumption)
		{
			throw new NotImplementedException();
		}

		private decimal InterpolateViaAmbientMonthes (Month month)
		{
			throw new NotImplementedException();
		}
	}
}
