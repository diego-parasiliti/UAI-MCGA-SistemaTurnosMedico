using MCGA.Entities;
using MCGA.UI.Process;
using MCGA.WebSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MCGA.WebSite.Controllers
{
	[Authorize(Roles = "Administrador")]
	public class DatoAdicionalAfiliadoController : Controller
    {
		AfiliadoProcess afiliadoProcess = new AfiliadoProcess();
		TipoKeyProcess tipoKeyProcess = new TipoKeyProcess();
		DatoAdicionalAfiliadoProcess datoAdicionalAfiliadoProcess = new DatoAdicionalAfiliadoProcess();

		public JsonResult GetTipoKey()
		{
			var listTipoKey = tipoKeyProcess.GetAll().OrderBy(o => o.Descripcion).Select( o=> new { Id = o.Id, Descripcion = o.Descripcion }).ToList();
			return Json(listTipoKey, JsonRequestBehavior.AllowGet);
		}
		
		public JsonResult GetCamposByTipoKey(int tipoKeyId)
		{
			List<ControlDatoAdicionalAfiliadoViewModel> listControl = new List<ControlDatoAdicionalAfiliadoViewModel>();
			var tipoKey = tipoKeyProcess.GetById(tipoKeyId);
			foreach(DetalleTipoKey detalleTipoKey in tipoKey.DetalleTipoKey)
			{
				ControlDatoAdicionalAfiliadoViewModel control = new ControlDatoAdicionalAfiliadoViewModel();
				control.ControlId = detalleTipoKey.Id;
				control.TipoControl = detalleTipoKey.TipoControl.NombreInterno;
				control.LabelControl = detalleTipoKey.Nombre;
				control.JsonData = detalleTipoKey.JsonData;
				listControl.Add(control);
			}
			return Json(listControl, JsonRequestBehavior.AllowGet);
		}

		// GET: DatoAdicionalAfiliado
		public ActionResult Create(int afiliadoId)
        {
			var afiliado = afiliadoProcess.GetById(afiliadoId);
			ViewBag.AfiliadoId = afiliado.Id;
			ViewBag.NombreAfiliado = string.Format("{0} {1}", afiliado.Nombre, afiliado.Apellido);
			return View();
        }

		
		[HttpPost]
		public JsonResult GuardarDatoAdicionalAfiliad(string AfiliadoId, string TipoKeyId, string JsonData)
		{
			DatoAdicionalAfiliado datoAdicionalAfiliado = new DatoAdicionalAfiliado();
			datoAdicionalAfiliado.Fecha = DateTime.Now;
			datoAdicionalAfiliado.AfiliadoId = Convert.ToInt32(AfiliadoId);
			datoAdicionalAfiliado.TipoKeyId = Convert.ToInt32(TipoKeyId);
			datoAdicionalAfiliado.JsonData = JsonData;

			datoAdicionalAfiliadoProcess.Add(datoAdicionalAfiliado);
			return Json(datoAdicionalAfiliado, JsonRequestBehavior.AllowGet);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				afiliadoProcess.Dispose();
				tipoKeyProcess.Dispose();
				datoAdicionalAfiliadoProcess.Dispose();
			}
			base.Dispose(disposing);
		}
	}
}