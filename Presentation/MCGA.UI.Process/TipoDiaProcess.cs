using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCGA.Entities;

namespace MCGA.UI.Process
{
	public class TipoDiaProcess : IDisposable
	{
		private Business.TipoDiaCoponent business = new Business.TipoDiaCoponent();

		public List<TipoDia> GetAll()
		{

			return business.GetAll();
		}

		public TipoDia GetById(int? id)
		{
			return business.GetById(id);
		}

		public void Add(TipoDia tipoDia)
		{
			business.Add(tipoDia);
		}

		public void Modify(TipoDia tipoDia)
		{
			business.Modify(tipoDia);
		}

		public void Delete(TipoDia tipoDia)
		{
			business.Delete(tipoDia);
		}

		public void Dispose()
		{
			business.Dispose();
		}
	
	}
}
