using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MCGA.Constants.TipoEspecialidadController;
using MCGA.Data;
using MCGA.Entities;
using MCGA.UI.Process;

namespace MCGA.WebSite.Controllers
{
	[Authorize]
	public class TipoEspecialidadController : Controller
    {
        private TipoEspecialidadProcess process = new TipoEspecialidadProcess();

		// GET: TipoEspecialidad
		[Route("listado-tipo-especialidad", Name = TipoEspecialidadControllerRoute.GetIndex)]
		[Compress]
		public ActionResult Index()
        {
			return View(TipoEspecialidadControllerAction.Index, process.GetAll());
        }

		// GET: TipoEspecialidad/Create
		[Route("agregar-tipo-especialidad", Name = TipoEspecialidadControllerRoute.GetCreate)]
		[Compress]
		public ActionResult Create()
        {
            return View(TipoEspecialidadControllerAction.Create);
        }

        // POST: TipoEspecialidad/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
		[Route("agregar-tipo-especialidad", Name = TipoEspecialidadControllerRoute.PostCreate)]
		[Compress]
		public ActionResult Create([Bind(Include = "Id,descripcion")] TipoEspecialidad tipoEspecialidad)
        {
            if (ModelState.IsValid)
            {
				process.Add(tipoEspecialidad);
                return RedirectToAction("Index");
            }
			return View(TipoEspecialidadControllerAction.Create, tipoEspecialidad);
		}

		// GET: TipoEspecialidad/Edit/5
		[Route("editar-tipo-especialidad", Name = TipoEspecialidadControllerRoute.GetEdit)]
		[Compress]
		public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
			TipoEspecialidad tipoEspecialidad = process.GetById(id);
			if (tipoEspecialidad == null)
            {
                return HttpNotFound();
            }
			return View(TipoEspecialidadControllerAction.Edit, tipoEspecialidad);
        }

        // POST: TipoEspecialidad/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
		[Route("editar-tipo-especialidad", Name = TipoEspecialidadControllerRoute.PostEdit)]
		[Compress]
		public ActionResult Edit([Bind(Include = "Id,descripcion")] TipoEspecialidad tipoEspecialidad)
        {
            if (ModelState.IsValid)
            {
				process.Edit(tipoEspecialidad);
                return RedirectToAction("Index");
            }
			return View(TipoEspecialidadControllerAction.Edit, tipoEspecialidad);
		}

		// GET: TipoEspecialidad/Delete/5
		[Route("eliminar-tipo-especialidad", Name = TipoEspecialidadControllerRoute.GetDelete)]
		[Compress]
		public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
			TipoEspecialidad tipoEspecialidad = process.GetById(id);
			if (tipoEspecialidad == null)
            {
                return HttpNotFound();
            }
			return View(TipoEspecialidadControllerAction.Delete, tipoEspecialidad);
        }

        // POST: TipoEspecialidad/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
		[Route("eliminar-tipo-especialidad", Name = TipoEspecialidadControllerRoute.PostDelete)]
		[Compress]
		public ActionResult DeleteConfirmed(int id)
        {
			TipoEspecialidad tipoEspecialidad = process.GetById(id);
			process.Remove(tipoEspecialidad);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
				process.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
