using MCGA.Data;
using MCGA.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace MCGA.Business
{
	public class TipoDiaCoponent : IDisposable
	{

		private MedicureContext db = new MedicureContext();

		public List<TipoDia> GetAll()
		{
			return db.TipoDia.ToList();
		}

		public TipoDia GetById(int? id)
		{
			return db.TipoDia.Find(id);
		}

		public void Add(TipoDia tipoDia)
		{
			using (TransactionScope scope = new TransactionScope())
			{ 
				db.TipoDia.Add(tipoDia);
				db.SaveChanges();

				scope.Complete();
			}
		}

		public void Modify(TipoDia tipoDia)
		{
			using (TransactionScope scope = new TransactionScope())
			{
				db.Entry(tipoDia).State = EntityState.Modified;
				db.SaveChanges();

				scope.Complete();
			}

		}

		public void Delete(TipoDia tipoDia)
		{
			using (TransactionScope scope = new TransactionScope())
			{
				db.TipoDia.Remove(tipoDia);
				db.SaveChanges();

				scope.Complete();
			}
		}

		public void Dispose()
		{
			db.Dispose();
		}

	}
}
