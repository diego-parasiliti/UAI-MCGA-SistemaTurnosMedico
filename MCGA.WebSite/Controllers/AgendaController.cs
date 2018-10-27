using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MCGA.Constants;
using MCGA.Data;
using MCGA.Entities;
using MCGA.UI.Process;

namespace MCGA.WebSite.Controllers
{
    public class AgendaController : Controller
    {
		private AgendaProcess process = new AgendaProcess();
		private TipoDiaProcess tipoDiaProcess = new TipoDiaProcess();
		private EspecialidadesProfesionalProcess especialidadesProfesionalProcess = new EspecialidadesProfesionalProcess();
		private EspecialidadProcess especialidadProcess = new EspecialidadProcess();
		private ProfesionalProcess profesionalProcess = new ProfesionalProcess();
		private TurnoProcess turnoProcess = new TurnoProcess();

		public JsonResult GetFechasDisponiblesByIdEspecialidadProfesional(int idEspecialidadProfesional = 0)
		{
			Agenda agenda = process.GetAll().Where(o => o.EspecialidadProfesionalId == idEspecialidadProfesional).FirstOrDefault();
			if (agenda != null)
			{
				List<dynamic> listaFecha = new List<dynamic>();
				DateTime fecha = agenda.fecha_desde;
				
				while(fecha <= agenda.fecha_hasta)
				{
					//Cantidad de horas que atiende en el día
					int cantidadHoras =
							(
								new DateTime(fecha.Year, fecha.Month, fecha.Day, agenda.hora_hasta.Hours, agenda.hora_hasta.Minutes, 0)
								-
								new DateTime(fecha.Year, fecha.Month, fecha.Day, agenda.hora_desde.Hours, agenda.hora_desde.Minutes, 0)		
							).Hours;

					//Verificar si ese día ya tiene todo agendado
					if (turnoProcess.GetAll().Where(o=> o.Fecha == fecha && o.EspecialidadProfesionalId == idEspecialidadProfesional).Count() != cantidadHoras)
					{ 
						listaFecha.Add(new
							{
								FechaText = string.Format("{0} {1} de {2} de {3}", fecha.ToString("ddd"), fecha.ToString("dd"), fecha.ToString("MMM"), fecha.ToString("yyyy")),
								FechaValue = fecha.ToString("yyyy-MM-dd")
							});
					}
					fecha = fecha.AddDays(1);
				}
				return Json(listaFecha, JsonRequestBehavior.AllowGet);
			}
			else
				return Json(null, JsonRequestBehavior.AllowGet);
		}

		public JsonResult GetHorarioDisponiblesByIdEspecialidadProfesionalFecha(int idEspecialidadProfesional = 0, DateTime? fecha = null)
		{
			Agenda agenda = process.GetAll().Where(o => o.EspecialidadProfesionalId == idEspecialidadProfesional).FirstOrDefault();
			if (agenda != null)
			{
				List<dynamic> listaHora = new List<dynamic>();
				TimeSpan hora = agenda.hora_desde;
				while (hora < agenda.hora_hasta)
				{
					//ver si hay turno con esa hora y fecha
					if (turnoProcess.GetAll().FirstOrDefault(o=> o.Fecha == fecha && o.Hora == hora && o.EspecialidadProfesionalId == idEspecialidadProfesional) == null)
					{ 
						listaHora.Add(new
						{
							HoraText = hora.ToString(@"hh\:mm"),
							HoraValue = hora.ToString(@"hh\:mm")
						});
					}
					hora = hora.Add(new TimeSpan(1, 0, 0));
				}
				return Json(listaHora, JsonRequestBehavior.AllowGet);
			}
			else
				return Json(null, JsonRequestBehavior.AllowGet);
	}
		// GET: Agenda
		[Route("listado-agenda", Name = AgendaControllerRoute.GetIndex)]
		public ActionResult Index()
        {
			var agenda = process.GetAll();
            return View(agenda.ToList());
        }

		// GET: Agenda/Create
		[Route("agregar-agenda", Name = AgendaControllerRoute.GetCreate)]
		public ActionResult Create()
        {
			ViewBag.ProfesionalId = string.Empty;
			ViewBag.EspecialidadTexto = string.Empty;
			ViewBag.ProfesionalTexto = string.Empty;
			ViewBag.TipoDiaId = new SelectList(tipoDiaProcess.GetAll(), "Id", "descripcion");
            return View();
        }

        // POST: Agenda/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
		[Route("agregar-agenda", Name = AgendaControllerRoute.PostCreate)]
		public ActionResult Create([Bind(Include = "Id,EspecialidadProfesionalId,TipoDiaId,fecha_desde,fecha_hasta,hora_desde,hora_hasta")] Agenda agenda, int profesionalId)
        {
            if (ModelState.IsValid)
            {
				process.Add(agenda);
                return RedirectToAction("Index");
            }
			EspecialidadesProfesional especialidadesProfesional = especialidadesProfesionalProcess.GetById(agenda.EspecialidadProfesionalId);
			Profesional profesional = profesionalProcess.GetById(profesionalId);
			string especialidadTexto = string.Empty;
			string profesionalTexto = profesional != null ? string.Format("{0} {1}", profesional.Nombre, profesional.Apellido) : string.Empty;
			ViewBag.ProfesionalId = profesionalId.ToString();
			if (especialidadesProfesional != null)
			{ 
				Especialidad especialidad = especialidadProcess.GetById(especialidadesProfesional.EspecialidadId);
				especialidadTexto = especialidad.descripcion;
			}
			ViewBag.EspecialidadTexto = especialidadTexto;
			ViewBag.ProfesionalTexto = profesionalTexto;

			ViewBag.TipoDiaId = new SelectList(tipoDiaProcess.GetAll(), "Id", "descripcion", agenda.TipoDiaId);
            return View(agenda);
        }

		// GET: Agenda/Edit/5
		[Route("editar-agenda", Name = AgendaControllerRoute.GetEdit)]
		public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agenda agenda = process.GetById(id);
			if (agenda == null)
            {
                return HttpNotFound();
            }
			EspecialidadesProfesional especialidadesProfesional = especialidadesProfesionalProcess.GetById(agenda.EspecialidadProfesionalId);
			Profesional profesional = profesionalProcess.GetById(agenda.EspecialidadesProfesional.ProfesionalId);
			Especialidad especialidad = especialidadProcess.GetById(especialidadesProfesional.EspecialidadId);
			string especialidadTexto = especialidad.descripcion;
			string profesionalTexto = profesional != null ? string.Format("{0} {1}", profesional.Nombre, profesional.Apellido) : string.Empty;
			ViewBag.ProfesionalId = profesional.Id.ToString();
			ViewBag.EspecialidadTexto = especialidadTexto;
			ViewBag.ProfesionalTexto = profesionalTexto;
            ViewBag.TipoDiaId = new SelectList(tipoDiaProcess.GetAll(), "Id", "descripcion", agenda.TipoDiaId);
            return View(agenda);
        }

        // POST: Agenda/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
		[Route("editar-agenda", Name = AgendaControllerRoute.PostEdit)]
		public ActionResult Edit([Bind(Include = "Id,EspecialidadProfesionalId,TipoDiaId,fecha_desde,fecha_hasta,hora_desde,hora_hasta")] Agenda agenda, int profesionalId)
        {
            if (ModelState.IsValid)
            {
				process.Edit(agenda);
                return RedirectToAction("Index");
            }
			EspecialidadesProfesional especialidadesProfesional = especialidadesProfesionalProcess.GetById(agenda.EspecialidadProfesionalId);
			Profesional profesional = profesionalProcess.GetById(profesionalId);
			string especialidadTexto = string.Empty;
			string profesionalTexto = profesional != null ? string.Format("{0} {1}", profesional.Nombre, profesional.Apellido) : string.Empty;
			ViewBag.ProfesionalId = profesionalId.ToString();
			if (especialidadesProfesional != null)
			{
				Especialidad especialidad = especialidadProcess.GetById(especialidadesProfesional.EspecialidadId);
				especialidadTexto = especialidad.descripcion;
			}
			ViewBag.EspecialidadTexto = especialidadTexto;
			ViewBag.ProfesionalTexto = profesionalTexto;
			ViewBag.TipoDiaId = new SelectList(tipoDiaProcess.GetAll(), "Id", "descripcion", agenda.TipoDiaId);
            return View(agenda);
        }

		// GET: Agenda/Delete/5
		[Route("eliminar-agenda", Name = AgendaControllerRoute.GetDelete)]
		public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agenda agenda = process.GetById(id);
			if (agenda == null)
            {
                return HttpNotFound();
            }
            return View(agenda);
        }

        // POST: Agenda/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
		[Route("eliminar-agenda", Name = AgendaControllerRoute.PostDelete)]
		public ActionResult DeleteConfirmed(int id)
        {
            Agenda agenda = process.GetById(id);
			process.Remove(agenda);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
				process.Dispose();
				tipoDiaProcess.Dispose();
				especialidadesProfesionalProcess.Dispose();
				especialidadProcess.Dispose();
				profesionalProcess.Dispose();
				turnoProcess.Dispose();
			}
            base.Dispose(disposing);
        }
    }
}
