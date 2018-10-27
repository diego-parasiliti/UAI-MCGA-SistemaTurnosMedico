using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DayPilot.Web.Mvc;
using MCGA.Constants;
using MCGA.Data;
using MCGA.Entities;
using MCGA.UI.Process;

namespace MCGA.WebSite.Controllers
{

	class Dpc : DayPilotCalendar
	{
		//DataClasses1DataContext db = new DataClasses1DataContext();

		//protected override void OnInit(InitArgs e)
		//{
		//	Update(CallBackUpdateType.Full);
		//}

		//protected override void OnEventResize(EventResizeArgs e)
		//{
		//	var toBeResized = (from ev in db.Events where ev.id == Convert.ToInt32(e.Id) select ev).First();
		//	toBeResized.eventstart = e.NewStart;
		//	toBeResized.eventend = e.NewEnd;
		//	db.SubmitChanges();
		//	Update();
		//}

		//protected override void OnEventMove(EventMoveArgs e)
		//{
		//	var toBeResized = (from ev in db.Events where ev.id == Convert.ToInt32(e.Id) select ev).First();
		//	toBeResized.eventstart = e.NewStart;
		//	toBeResized.eventend = e.NewEnd;
		//	db.SubmitChanges();
		//	Update();
		//}

		//protected override void OnTimeRangeSelected(TimeRangeSelectedArgs e)
		//{
		//	var toBeCreated = new Event { eventstart = e.Start, eventend = e.End, text = (string)e.Data["name"] };
		//	db.Events.InsertOnSubmit(toBeCreated);
		//	db.SubmitChanges();
		//	Update();
		//}

		//protected override void OnFinish()
		//{
		//	if (UpdateType == CallBackUpdateType.None)
		//	{
		//		return;
		//	}

		//	Events = from ev in db.Events select ev;

		//	DataIdField = "id";
		//	DataTextField = "text";
		//	DataStartField = "eventstart";
		//	DataEndField = "eventend";
		//}

	}

	public class TurnoController : Controller
    {
        private TurnoProcess process = new TurnoProcess();
		private AfiliadoProcess afiliadoProcess = new AfiliadoProcess();
		private EspecialidadesProfesionalProcess especialidadesProfesionalProcess = new EspecialidadesProfesionalProcess();

		
		public ActionResult List()
		{
			return View();
		}

		public ActionResult Backend()
		{
			return new Dpc().CallBack(this);
		}

		// GET: Turno
		[Route("listado-turno", Name = TurnoControllerRoute.GetIndex)]
		public ActionResult Index(bool turnoGenerado = false)
        {
			if (turnoGenerado)
				ViewBag.TurnoGenerado = true;

			return View(process.GetAll().OrderBy(o => o.Fecha).ThenBy(o => o.Hora).ToList());
        }

		// GET: Turno/Create
		[Route("agregar-turno", Name = TurnoControllerRoute.GetCreate)]
		public ActionResult Create()
        {
			ViewBag.AfiliadoId = new SelectList(afiliadoProcess.GetAll().Select(o => new { o.Id, Nombre = string.Format("{0} {1}", o.Nombre, o.Apellido) }).ToList(), "Id", "Nombre");
            ViewBag.EspecialidadProfesionalId = new SelectList(especialidadesProfesionalProcess.GetAll().Select(o => new { o.Id, Especialidad = string.Format("{0} ({1} {2})", o.Especialidad.descripcion, o.Profesional.Nombre, o.Profesional.Apellido) }).ToList(), "Id", "Especialidad");
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

			}
            base.Dispose(disposing);
        }
    }
}
