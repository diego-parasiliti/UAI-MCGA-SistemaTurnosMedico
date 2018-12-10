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
	public class TipoControlComponent : IDisposable
	{
		private MedicureContext db = new MedicureContext();

		public List<TipoControl> GetAll()
		{
			try
			{
				return db.TipoControl.ToList();
			}
			catch
			{
				throw;
			}
		}

		public TipoControl GetById(int? id)
		{
			try
			{
				return db.TipoControl.Find(id);
			}
			catch
			{
				throw;
			}
		}

		public void Add(TipoControl tipoControl)
		{
			try
			{
				using (TransactionScope scope = new TransactionScope())
				{
					db.TipoControl.Add(tipoControl);
					db.SaveChanges();

					scope.Complete();
				}
			}
			catch
			{
				throw;
			}
		}

		public void Edit(TipoControl tipoControl)
		{
			try
			{
				using (TransactionScope scope = new TransactionScope())
				{
					db.Entry(tipoControl).State = EntityState.Modified;
					db.SaveChanges();

					scope.Complete();
				}
			}
			catch
			{
				throw;
			}
		}
		
		public void Dispose()
		{
			db.Dispose();
		}

	}
}
