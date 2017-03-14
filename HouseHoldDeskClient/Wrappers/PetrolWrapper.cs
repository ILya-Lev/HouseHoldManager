using DomainObjects.Car;
using HouseHoldDeskClient.ViewModels;
using System;
using System.Runtime.CompilerServices;

namespace HouseHoldDeskClient.Wrappers
{
	public class PetrolWrapper : ViewModelBase
	{
		public bool IsChanged { get; private set; }

		public Petrol Model { get; }

		public PetrolWrapper(Petrol model)
		{
			Model = model;
		}

		public decimal TotalPrice => Rate * (decimal) Amount;

		public decimal Rate
		{
			get { return GetValue<decimal>(nameof(Model.Price)); }
			set { SetValue(value, nameof(Model.Price)); }
		}

		public double Amount
		{
			get { return GetValue<double>(); }
			set { SetValue(value); }
		}

		public DateTime Date
		{
			get { return GetValue<DateTime>(); }
			set { SetValue(value); }
		}
		public GasolinStation GasolinStation
		{
			get { return GetValue<GasolinStation>(); }
			set { SetValue(value); }
		}

		public decimal BonusBalance
		{
			get { return GetValue<decimal>(); }
			set { SetValue(value); }
		}

		public PetrolType PetrolType
		{
			get { return GetValue<PetrolType>(); }
			set { SetValue(value); }
		}


		private TProperty GetValue<TProperty>([CallerMemberName] string propertyName = null)
		{
			var propertyInfo = Model.GetType().GetProperty(propertyName);
			return (TProperty) propertyInfo.GetValue(Model);
		}

		private void SetValue<TProperty>(TProperty value, [CallerMemberName] string propertyName = null)
		{
			var propertyInfo = Model.GetType().GetProperty(propertyName);

			if (Equals(value, propertyInfo.GetValue(Model)))
				return;

			propertyInfo.SetValue(Model, value);
			OnPropertyChanged(propertyName);
			IsChanged = true;
		}

		public void Reset()
		{
			throw new NotImplementedException();
			IsChanged = false;
		}

		public void Accept()
		{
			throw new NotImplementedException();
			IsChanged = false;
		}
	}
}