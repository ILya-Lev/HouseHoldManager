using System;
using System.Windows.Input;

namespace HouseHoldDeskClient.ViewModels
{
	public class CalculateElectricityViewModel : ViewModelBase
	{
		private decimal _bill;
		private DateTime _dateSet = DateTime.Today;

		public ICommand Calculate { get; set; }

		public DateTime DateSet
		{
			get { return _dateSet; }
			set { _dateSet = value; OnPropertyChanged(); }
		}

		public decimal Bill
		{
			get { return _bill; }
			set { _bill = value; OnPropertyChanged(); }
		}
	}
}
