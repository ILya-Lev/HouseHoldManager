using DomainObjects.Car;
using HouseHoldDeskClient.Commands;
using HouseHoldDeskClient.Wrappers;
using System.Windows.Input;

namespace HouseHoldDeskClient.ViewModels
{
	public interface ICarPetrolEditViewModel
	{
		PetrolWrapper Petrol { get; set; }
		void Load(int? petrolId);
	}

	public class CarPetrolEditViewModel : ViewModelBase, ICarPetrolEditViewModel
	{
		private readonly IPetrolRepository _petrolRepository;
		public PetrolWrapper Petrol { get; set; }

		public ICommand SaveCommand { get; }
		public ICommand ResetCommand { get; }

		public CarPetrolEditViewModel(IPetrolRepository petrolRepository)
		{
			_petrolRepository = petrolRepository;
			SaveCommand = new DelegateCommand(CanSaveCommandExecute, OnSaveCommand);
			ResetCommand = new DelegateCommand(CanResetCommandExecute, OnResetCommand);
		}

		public void Load(int? petrolId)
		{
			Petrol = new PetrolWrapper
			(
				petrolId == null
				? new Petrol()
				: _petrolRepository.Get(petrolId.Value)
			);

			Petrol.PropertyChanged += (sender, args) =>
			{
				if (args.PropertyName == nameof(Petrol.IsChanged))
				{
					RaiseCanExecuteChanged();
				}
			};
			RaiseCanExecuteChanged();
		}

		private void RaiseCanExecuteChanged()
		{
			(SaveCommand as DelegateCommand).RaiseCanExecuteChanged();
			(ResetCommand as DelegateCommand).RaiseCanExecuteChanged();
		}

		private bool CanSaveCommandExecute(object arg)
		{
			return Petrol?.IsChanged ?? false;
		}
		private void OnSaveCommand(object obj)
		{
			if (Petrol.Model.Id == 0)
				_petrolRepository.Create(Petrol.Model);
			else
				_petrolRepository.Update(Petrol.Model);
			Petrol.Accept();
		}

		private bool CanResetCommandExecute(object arg)
		{
			return Petrol?.IsChanged ?? false;
		}

		private void OnResetCommand(object obj)
		{
			Petrol.Reset();
		}
	}

	public interface IPetrolRepository
	{
		Petrol Get(int id);

		void Create(Petrol petrol);

		void Update(Petrol petrol);
	}
}