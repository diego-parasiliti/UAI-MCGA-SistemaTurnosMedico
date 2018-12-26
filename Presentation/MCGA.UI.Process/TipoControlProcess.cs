using MCGA.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCGA.UI.Process
{
	public class TipoControlProcess : IDisposable
	{
		private Business.TipoControlComponent business = new Business.TipoControlComponent();

		public List<TipoControl> GetAll()
		{
			try
			{
				return business.GetAll();
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
				return business.GetById(id);
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
				business.Add(tipoControl);
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
				business.Edit(tipoControl);
			}
			catch
			{
				throw;
			}
		}

		public void Dispose()
		{
			business.Dispose();
		}

	}
}