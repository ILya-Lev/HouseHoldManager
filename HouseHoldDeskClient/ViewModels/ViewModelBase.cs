using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace HouseHoldDeskClient.ViewModels
{
	public abstract class ViewModelBase
	{
		public virtual event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}