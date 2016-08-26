using System;
using System.Linq;
using DomainObjects.Electricity;

namespace DataAccess.Repositories
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
		{
			return _context.ElectricityTarifs
				.Include(nameof(Tarif.ConsumptionRanges))
				.FirstOrDefault(tarif => tarif.ApplicableSince < day
									  && (tarif.ApplicableTill == null || tarif.ApplicableTill >= day));
		}

		public void AddTarif (Tarif tarif)
		{
			_context.ElectricityTarifs.Add(tarif);
			_context.SaveChanges();
		}
	}
}
