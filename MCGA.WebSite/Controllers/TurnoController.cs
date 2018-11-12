using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using MCGA.Constants;
using MCGA.Data;
using MCGA.Entities;
using MCGA.UI.Process;
using PagedList;

namespace MCGA.WebSite.Controllers
{
	[Authorize()]
	public class TurnoController : Controller
    {
        private TurnoProcess process = new TurnoProcess();
		private AfiliadoProcess afiliadoProcess = new AfiliadoProcess();
		private EspecialidadesProfesionalProcess especialidadesProfesionalProcess = new EspecialidadesProfesionalProcess();
		private TipoResevaProcess tipoReservaProcess = new TipoResevaProcess(); 

		public FileResult ExportExcel()
		{
			string[] aColumnas = { "Fecha", "Hora", "Especialidad", "Profesional", "Afiliado", "Reserva", "Observaciones" };
			List<dynamic> lstDatos = process.GetAll().Select(o => new {Fecha = o.Fecha.ToShortDateString(), Hora = o.Hora.ToString(),  Especialidad = o.EspecialidadesProfesional.Especialidad.descripcion, Profesional = string.Format("{0} {1}", o.EspecialidadesProfesional.Profesional.Nombre, o.EspecialidadesProfesional.Profesional.Apellido), Afiliado = string.Format("{0} {1}", o.Afiliado.Nombre, o.Afiliado.Apellido), Reserva= o.reserva.ToString(), o.Observaciones}).ToList<dynamic>();
			return File(new Framework.ExportExcel().ExportarExcel(aColumnas, lstDatos), "application/vnd.ms-excel", "Listado de turnos.xls");
		}

		public ActionResult List()
		{
			return this.View();
		}

		// GET: Turno
		[Route("listado-turno", Name = TurnoControllerRoute.GetIndex)]
		public ActionResult Index(int? page, bool turnoGenerado = false)
        {

			//var roles = User.IsInRole("Admin");
			if (turnoGenerado)
				ViewBag.TurnoGenerado = true;

			var turno = process.GetAll().OrderBy(o => o.Fecha).ThenBy(o => o.Hora);
			int pageSize = int.Parse(ConfigurationManager.AppSettings.Get("CantidadFilasPagina"));
			int pageNumber = (page ?? 1);
			return View(turno.ToPagedList(pageNumber, pageSize));
		}

		// GET: Turno/Create
		[Route("agregar-turno", Name = TurnoControllerRoute.GetCreate)]
		public ActionResult Create()
        {
			ViewBag.AfiliadoId = new SelectList(afiliadoProcess.GetAll().Select(o => new { o.Id, Nombre = string.Format("{0} {1}", o.Nombre, o.Apellido) }).ToList(), "Id", "Nombre");
            ViewBag.EspecialidadProfesionalId = new SelectList(especialidadesProfesionalProcess.GetAll().Select(o => new { o.Id, Especialidad = string.Format("{0} ({1} {2})", o.Especialidad.descripcion, o.Profesional.Nombre, o.Profesional.Apellido) }).ToList(), "Id", "Especialidad");
			ViewBag.reserva = new SelectList(tipoReservaProcess.GetAll(), "Id", "descripcion");
			return View();
        }

        // POST: Turno/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
		[Route("agregar-turno", Name = TurnoControllerRoute.PostCreate)]
		public ActionResult Create([Bind(Include = "Id,Fecha,Hora,AfiliadoId,EspecialidadProfesionalId,reserva,Observaciones")] Turno turno)
        {
            if (ModelState.IsValid)
            {
				process.Add(turno);
				return RedirectToAction("Index", new { turnoGenerado = true });
            }

            ViewBag.AfiliadoId = new SelectList(afiliadoProcess.GetAll().Select(o => new { o.Id, Nombre = string.Format("{0} {1}", o.Nombre, o.Apellido) }).ToList(), "Id", "Nombre", turno.AfiliadoId);
            ViewBag.EspecialidadProfesionalId = new SelectList(especialidadesProfesionalProcess.GetAll().Select(o => new { o.Id, Especialidad = string.Format("{0} ({1} {2})", o.Especialidad.descripcion, o.Profesional.Nombre, o.Profesional.Apellido) }).ToList(), "Id", "Especialidad", turno.EspecialidadProfesionalId);
			
            return View(turno);
        }

		[HttpPost]
		public JsonResult CreateTurno(Turno turno)
		{
			process.Add(turno);
			return Json(turno, JsonRequestBehavior.AllowGet);
		}

		// GET: Turno/Edit/5
		[Route("editar-turno", Name = TurnoControllerRoute.GetEdit)]
		public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Turno turno = process.GetById(id);
            if (turno == null)
            {
                return HttpNotFound();
            }
            ViewBag.AfiliadoId = new SelectList(afiliadoProcess.GetAll().Select(o => new { o.Id, Nombre = string.Format("{0} {1}", o.Nombre, o.Apellido) }).ToList(), "Id", "Nombre", turno.AfiliadoId);
            ViewBag.EspecialidadProfesionalId = new SelectList(especialidadesProfesionalProcess.GetAll().Select(o => new { o.Id, Especialidad = string.Format("{0} ({1} {2})", o.Especialidad.descripcion, o.Profesional.Nombre, o.Profesional.Apellido) }).ToList(), "Id", "Especialidad", turno.EspecialidadProfesionalId);
            return View(turno);
        }

        // POST: Turno/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
		[Route("editar-turno", Name = TurnoControllerRoute.PostEdit)]
		public ActionResult Edit([Bind(Include = "Id,Fecha,Hora,AfiliadoId,EspecialidadProfesionalId,reserva,Observaciones")] Turno turno)
        {
            if (ModelState.IsValid)
            {
				process.Edit(turno);
                return RedirectToAction("Index");
            }
            ViewBag.AfiliadoId = new SelectList(afiliadoProcess.GetAll().Select(o => new { o.Id, Nombre = string.Format("{0} {1}", o.Nombre, o.Apellido) }).ToList(), "Id", "Nombre", turno.AfiliadoId);
            ViewBag.EspecialidadProfesionalId = new SelectList(especialidadesProfesionalProcess.GetAll().Select(o => new { o.Id, Especialidad = string.Format("{0} ({1} {2})", o.Especialidad.descripcion, o.Profesional.Nombre, o.Profesional.Apellido) }).ToList(), "Id", "Especialidad", turno.EspecialidadProfesionalId);
            return View(turno);
        }

		// GET: Turno/Delete/5
		[Route("eliminar-turno", Name = TurnoControllerRoute.GetDelete)]
		public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Turno turno = process.GetById(id);
            if (turno == null)
            {
                return HttpNotFound();
            }
            return View(turno);
        }

        // POST: Turno/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
		[Route("eliminar-turno", Name = TurnoControllerRoute.PostDelete)]
		public ActionResult DeleteConfirmed(int id)
        {
            Turno turno = process.GetById(id);
            process.Remove(turno);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
				process.Dispose();
				afiliadoProcess.Dispose();
				especialidadesProfesionalProcess.Dispose();
				tipoReservaProcess.Dispose();

			}
            base.Dispose(disposing);
        }
    }
}
