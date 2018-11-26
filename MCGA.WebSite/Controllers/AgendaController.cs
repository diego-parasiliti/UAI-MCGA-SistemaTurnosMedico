using System;
using System.Collections.Generic;
using System.Configuration;
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
using MCGA.WebSite.Models;
using PagedList;

namespace MCGA.WebSite.Controllers
{
	[Authorize]
	public class AgendaController : Controller
	{
		private AgendaProcess process = new AgendaProcess();
		private TipoDiaProcess tipoDiaProcess = new TipoDiaProcess();
		private EspecialidadesProfesionalProcess especialidadesProfesionalProcess = new EspecialidadesProfesionalProcess();
		private EspecialidadProcess especialidadProcess = new EspecialidadProcess();
		private ProfesionalProcess profesionalProcess = new ProfesionalProcess();
		private TurnoProcess turnoProcess = new TurnoProcess();
		private AtencionProcess atencionProcess = new AtencionProcess();
		private AgendaCancelacionProcess agendaCancelacionProcess = new AgendaCancelacionProcess();

		public FileResult ExportExcel()
		{
			string[] aColumnas = { "Profesional", "Especialidad", "Día", "F. desde", "F. hasta", "H. desde", "H. hasta" };
			List<dynamic> lstDatos = process.GetAll().Select(o => new { Profesional = string.Format("{0} {1}", o.EspecialidadesProfesional.Profesional.Nombre, o.EspecialidadesProfesional.Profesional.Apellido), Especialidad = o.EspecialidadesProfesional.Especialidad.descripcion, o.TipoDia.descripcion, FechaDesde = o.fecha_desde.ToShortDateString(), FechaHasta = o.fecha_hasta.ToShortDateString(), HoraDesde = o.hora_desde.ToString(), HoraHasta = o.hora_hasta.ToString() }).ToList<dynamic>();
			return File(new Framework.ExportExcel().ExportarExcel(aColumnas, lstDatos), "application/vnd.ms-excel", "Listado de agendas.xls");
		}

		//public JsonResult GetCalendarioByIdEspecialidadProfesional(DateTime fechaDesde, DateTime fechaHasta, int idEspecialidadProfesional = 0)
		//{
		//	if (fechaDesde == null)
		//		fechaDesde = new DateTime(1800, 1, 1);

		//	if (fechaHasta == null)
		//		fechaHasta = new DateTime(1800, 1, 1);

		//	//Reviso si tiene alguna agenda
		//	List<Agenda> listAtencion = process.GetAll().Where(o => o.EspecialidadProfesionalId == idEspecialidadProfesional && fechaDesde >= o.fecha_desde && fechaHasta <= o.fecha_hasta).ToList();
		//	if (listAtencion.Count > 0)
		//	{
		//		List<CalendarioEspecialidadProfesionalViewModel> listCalendario = new List<CalendarioEspecialidadProfesionalViewModel>();
		//		foreach (Agenda agenda in listAtencion)
		//		{ 
		//			DateTime fecha = fechaDesde;
		//			while (fecha <= fechaHasta)
		//			{
		//				//Ver si atiende ese dia
		//				if (fecha.DayOfWeek.ToString().ToUpper() == agenda.TipoDia.descripcion.ToUpper())
		//				{
		//					TimeSpan hora = agenda.hora_desde;
		//					while (hora <= agenda.hora_hasta)
		//					{
		//						CalendarioEspecialidadProfesionalViewModel calendario = new CalendarioEspecialidadProfesionalViewModel();
		//						Turno turno = turnoProcess.GetAll().Where(o => o.Fecha == fecha && o.Hora == hora && o.EspecialidadProfesionalId == idEspecialidadProfesional).FirstOrDefault();

		//						//Cargo variables generales
		//						calendario.FechaInicio = (new DateTime(fecha.Year, fecha.Month, fecha.Day, hora.Hours, hora.Minutes, hora.Seconds)).ToString("yyyy-MM-dd HH:mm"); //"2018-10-29 17:00";
		//						calendario.FechaFin = (new DateTime(fecha.Year, fecha.Month, fecha.Day, (hora.Hours + 1), hora.Minutes, hora.Seconds)).ToString("yyyy-MM-dd HH:mm");
		//						calendario.FontColor = Framework.ColorEvento.BLANCO;
								
		//						//Verificar si ese día y horario tiene turno
		//						if (turno != null)
		//						{
		//							//Ver si esta atendido 
		//							if (atencionProcess.GetAll().Where(o => o.turno_id == turno.Id).FirstOrDefault() != null)
		//							{
		//								//Lo atendio
		//								//Tiene turno
		//								calendario.Atendido = true;
		//								calendario.IdTurno = turno.Id;
		//								calendario.Titulo = string.Format("{0} {1}", turno.Afiliado.Nombre, turno.Afiliado.Apellido);
		//								calendario.Descripcion = string.Format("{0} {1}", turno.Afiliado.Nombre, turno.Afiliado.Apellido);
		//								calendario.BackgroundColor = Framework.ColorEvento.ATENDIDO;
		//							}
		//							else
		//							{
		//								//Tiene turno
		//								calendario.Atendido = false;
		//								calendario.IdTurno = turno.Id;
		//								calendario.Titulo = string.Format("{0} {1}", turno.Afiliado.Nombre, turno.Afiliado.Apellido);
		//								calendario.Descripcion = string.Format("{0} {1}", turno.Afiliado.Nombre, turno.Afiliado.Apellido);
		//								if (fecha.Date == DateTime.Now.Date)
		//									//Pendiente de atender
		//									calendario.BackgroundColor = Framework.ColorEvento.PENDIENTE_ATENCION;
		//								else
		//									//Turno ocupado
		//									calendario.BackgroundColor = Framework.ColorEvento.OCUPADO;
										
										
		//							}									
		//						}
		//						else
		//						{
		//							//No tiene turno
		//							calendario.Atendido = false;
		//							calendario.IdTurno = -1;
		//							calendario.Titulo= "Disponible";
		//							calendario.Descripcion = "Disponible";
		//							calendario.BackgroundColor = Framework.ColorEvento.DISPONIBLE;
		//						}
		//						listCalendario.Add(calendario);
		//						hora = hora.Add(new TimeSpan(1, 0, 0));
		//					}
		//				}
		//				fecha = fecha.AddDays(1);
		//			}
		//		}
		//		return Json(listCalendario, JsonRequestBehavior.AllowGet);
		//	}
		//	else
		//		return Json(null, JsonRequestBehavior.AllowGet);
		//}

		public JsonResult GetFechasDisponiblesByIdEspecialidadProfesional(int idEspecialidadProfesional = 0)
		{
			List<Agenda> listAgenda = process.GetAll().Where(o => o.EspecialidadProfesionalId == idEspecialidadProfesional && o.AgendaCancelacion.Count == 0).ToList();
			if (listAgenda.Count > 0)
			{
				List<string> listaFecha = new List<string>();
				foreach (Agenda agenda in listAgenda)
				{
					DateTime fecha = DateTime.Now.Date;// agenda.fecha_desde;

					while (fecha <= agenda.fecha_hasta)
					{
						//Ver si atiende ese dia
						if (fecha.DayOfWeek.ToString().ToUpper() == agenda.TipoDia.descripcion.ToUpper())
						{
							//Cantidad de horas que atiende en el día
							int cantidadHoras =
								(
									new DateTime(fecha.Year, fecha.Month, fecha.Day, agenda.hora_hasta.Hours, agenda.hora_hasta.Minutes, 0)
									-
									new DateTime(fecha.Year, fecha.Month, fecha.Day, agenda.hora_desde.Hours, agenda.hora_desde.Minutes, 0)
								).Hours;

							//Verificar si ese día ya tiene todo agendado
							if (turnoProcess.GetAll().Where(o => o.Fecha == fecha && o.EspecialidadProfesionalId == idEspecialidadProfesional).Count() != cantidadHoras)
							{
								listaFecha.Add(fecha.ToString("yyyy-MM-dd"));
							}
						}
						fecha = fecha.AddDays(1);
					}
				}
				return Json(listaFecha, JsonRequestBehavior.AllowGet);
			}
			else
				return Json(null, JsonRequestBehavior.AllowGet);
		}

		public JsonResult GetHorarioDisponiblesByIdEspecialidadProfesionalFecha(int idEspecialidadProfesional, DateTime fecha)
		{
			int tipoDiaId = tipoDiaProcess.GetAll().Where(o => o.descripcion.ToUpper() == fecha.DayOfWeek.ToString().ToUpper()).FirstOrDefault().Id;
			Agenda agenda = process.GetAll().Where(o => o.EspecialidadProfesionalId == idEspecialidadProfesional && o.TipoDiaId == tipoDiaId && fecha >= o.fecha_desde && fecha <= o.fecha_hasta && o.AgendaCancelacion.Count == 0).FirstOrDefault();
			if (agenda != null)
			{
				List<dynamic> listaHora = new List<dynamic>();
				TimeSpan hora =  agenda.hora_desde;
				while (hora <= agenda.hora_hasta)
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
		public ActionResult Index(int? page)
        {
			var agenda = process.GetAll();
			int pageSize = int.Parse(ConfigurationManager.AppSettings.Get("CantidadFilasPagina"));
			int pageNumber = (page ?? 1);
			return View(agenda.ToPagedList(pageNumber, pageSize));
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

		
		[Route("cancelar-agenda", Name = AgendaControllerRoute.GetCancelar)]
		public ActionResult Cancelar(int? id)
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

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Route("cancelar-agenda", Name = AgendaControllerRoute.PostCancelar)]
		public ActionResult Cancelar(int id, string detalleCancelacion)
		{
			AgendaCancelacion agendaCancelacion = new AgendaCancelacion();
			agendaCancelacion.agenda_id = id;
			agendaCancelacion.detalle_cancel = detalleCancelacion;
			agendaCancelacion.fecha_cancel = DateTime.Now;
			agendaCancelacion.createdon = DateTime.Now;
			agendaCancelacion.createdby = "";
			agendaCancelacion.changedon = DateTime.Now;
			agendaCancelacion.changedby = "";

			agendaCancelacionProcess.Add(agendaCancelacion);
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
				atencionProcess.Dispose();
			}
            base.Dispose(disposing);
        }
    }
}
