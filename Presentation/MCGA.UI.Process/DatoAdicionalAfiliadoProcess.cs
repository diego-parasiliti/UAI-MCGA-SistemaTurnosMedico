using MCGA.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCGA.UI.Process
{
	public class DatoAdicionalAfiliadoProcess : IDisposable
	{
		private Business.DatoAdicionalAfiliadoComponent business = new Business.DatoAdicionalAfiliadoComponent();

		public List<DatoAdicionalAfiliado> GetAll()
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

		public DatoAdicionalAfiliado GetById(int? id)
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

		public void Add(DatoAdicionalAfiliado datoAdicionalAfiliado)
		{
			try
			{
				business.Add(datoAdicionalAfiliado);
			}
			catch
			{
				throw;
			}
		}

		public void Edit(DatoAdicionalAfiliado datoAdicionalAfiliado)
		{
			try
			{
				business.Edit(datoAdicionalAfiliado);
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
