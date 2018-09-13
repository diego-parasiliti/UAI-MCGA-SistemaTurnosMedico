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
    public class TipoSexoController : Controller
    {
        private TipoSexoProcess process = new TipoSexoProcess();

        // GET: TipoSexo
        public ActionResult Index()
        {
            return View(process.GetAll());
        }

        // GET: TipoSexo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
			TipoSexo tipoSexo = process.GetById(id);
			if (tipoSexo == null)
            {
                return HttpNotFound();
            }
            return View(tipoSexo);
        }

        // GET: TipoSexo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoSexo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,descripcion")] TipoSexo tipoSexo)
        {
            if (ModelState.IsValid)
            {
				process.Add(tipoSexo);
				return RedirectToAction("Index");
            }

            return View(tipoSexo);
        }

        // GET: TipoSexo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
			TipoSexo tipoSexo = process.GetById(id);
			if (tipoSexo == null)
            {
                return HttpNotFound();
            }
            return View(tipoSexo);
        }

        // POST: TipoSexo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,descripcion")] TipoSexo tipoSexo)
        {
            if (ModelState.IsValid)
            {
				process.Edit(tipoSexo);
				return RedirectToAction("Index");
            }
            return View(tipoSexo);
        }

        // GET: TipoSexo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
			TipoSexo tipoSexo = process.GetById(id);
			if (tipoSexo == null)
            {
                return HttpNotFound();
            }
            return View(tipoSexo);
        }

        // POST: TipoSexo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
			TipoSexo tipoSexo = process.GetById(id);
			process.Remove(tipoSexo);
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
