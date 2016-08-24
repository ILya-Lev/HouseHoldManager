using DomainObjects;
using System;
using System.Linq;

namespace DataAccess
{
	public class ElectricityTarifsRepository
	{
		private readonly Context _context;

		public ElectricityTarifsRepository ()
		{
			_context = new Context();
		}

		public ElectricityTarif CurrentlyInForce () => InForceAt(DateTime.Today);

		public ElectricityTarif InForceAt (DateTime day)
			=> _context.ElectricityTarifs.FirstOrDefault(
				tarif => tarif.ApplicableSince < day && (tarif.ApplicableTill ?? day) >= day);

		public void AddTarif (ElectricityTarif tarif)
		{
			_context.ElectricityTarifs.Add(tarif);
			_context.SaveChanges();
		}
	}
}
