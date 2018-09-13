using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MCGA.Data;
using MCGA.Entities;
using MCGA.UI.Process;

namespace MCGA.WebSite.Controllers
{
    public class TipoEspecialidadController : Controller
    {
        private TipoEspecialidadProcess process = new TipoEspecialidadProcess();

        // GET: TipoEspecialidad
        public ActionResult Index()
        {
            return View(process.GetAll());
        }

        // GET: TipoEspecialidad/Details/5
        public ActionResult Details(int? id)
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
            return View(tipoEspecialidad);
        }

        // GET: TipoEspecialidad/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoEspecialidad/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,descripcion")] TipoEspecialidad tipoEspecialidad)
        {
            if (ModelState.IsValid)
            {
				process.Add(tipoEspecialidad);
                return RedirectToAction("Index");
            }

            return View(tipoEspecialidad);
        }

        // GET: TipoEspecialidad/Edit/5
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
            return View(tipoEspecialidad);
        }

        // POST: TipoEspecialidad/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,descripcion")] TipoEspecialidad tipoEspecialidad)
        {
            if (ModelState.IsValid)
            {
				process.Edit(tipoEspecialidad);
                return RedirectToAction("Index");
            }
            return View(tipoEspecialidad);
        }

        // GET: TipoEspecialidad/Delete/5
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
            return View(tipoEspecialidad);
        }

        // POST: TipoEspecialidad/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
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
