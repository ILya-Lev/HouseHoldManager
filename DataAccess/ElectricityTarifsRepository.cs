using DomainObjects.Electricity;
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

		public Tarif CurrentlyInForce () => InForceAt(DateTime.Today);

		public Tarif InForceAt (DateTime day)
			=> _context.ElectricityTarifs.FirstOrDefault(
				tarif => tarif.ApplicableSince < day && (tarif.ApplicableTill ?? day) >= day);

		public void AddTarif (Tarif tarif)
		{
			_context.ElectricityTarifs.Add(tarif);
			_context.SaveChanges();
		}
	}
}
