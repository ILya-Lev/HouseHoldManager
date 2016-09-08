using BusinessLogic;
using DataAccess.Repositories;
using DomainObjects;
using System;
using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Data;

namespace HouseHoldDeskClient
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private readonly ElectricityTarifsRepository _tarifRepo;
		private readonly ElectricityConsumptionRepository _consumptionRepo;
		private readonly ElectricityCalculator _calculator;

		public MainWindow ()
		{
			InitializeComponent();
			_tarifRepo = new ElectricityTarifsRepository();
			_consumptionRepo = new ElectricityConsumptionRepository();
			_calculator = new ElectricityCalculator(_tarifRepo, _consumptionRepo);

			Thread.CurrentThread.CurrentCulture = new CultureInfo("en-UA");

			btnCalculate.IsEnabled = false;
		}

		private void Window_Loaded (object sender, RoutedEventArgs e)
		{
			LoadActualViewSources();
		}

		private void LoadActualViewSources ()
		{
			var tarifViewSource = (CollectionViewSource) this.FindResource("tarifViewSource");
			// Load data by setting the CollectionViewSource.Source property:
			tarifViewSource.Source = new[] { _tarifRepo.InForceAt(DateTime.Today) };

			var consumptionViewSource = (CollectionViewSource) this.FindResource("consumptionViewSource");
			// Load data by setting the CollectionViewSource.Source property:
			var month = Month.From(dtDate.DisplayDate);
			consumptionViewSource.Source = _consumptionRepo.OrderedMeasurementsPerMonth(month);
		}

		private void btnCalculate_Click (object sender, RoutedEventArgs e)
		{
			var month = Month.From(dtDate.DisplayDate);
			priceBlock.Text = _calculator.PriceForMonth(month).ToString("C");
		}

		private void dtDate_CalendarClosed (object sender, RoutedEventArgs e)
		{
			btnCalculate.IsEnabled = dtDate.SelectedDate <= DateTime.Today;
			if (!btnCalculate.IsEnabled)
			{
				MessageBox.Show("Please select past date!");
				return;
			}

			LoadActualViewSources();
		}
	}
}
