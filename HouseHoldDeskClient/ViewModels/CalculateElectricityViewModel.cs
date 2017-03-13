using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace HouseHoldDeskClient.ViewModels
{
	public class CalculateElectricityViewModel : INotifyPropertyChanged
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

		#region INotifyPropertyChanged implementation
		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
		#endregion
	}
}
