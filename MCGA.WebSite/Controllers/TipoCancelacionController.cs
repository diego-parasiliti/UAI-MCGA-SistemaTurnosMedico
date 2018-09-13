using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MCGA.Data;
using MCGA.UI.Process;
using MCGA.Entities;

namespace MCGA.WebSite.Controllers
{
    public class TipoCancelacionController : Controller
    {
        private TipoCancelacionProcess process = new TipoCancelacionProcess();

        // GET: TipoCancelacion
        public ActionResult Index()
        {
			return View(process.GetAll());
        }

        // GET: TipoCancelacion/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoCancelacion tipoCancelacion = process.GetById(id);
            if (tipoCancelacion == null)
            {
                return HttpNotFound();
            }
            return View(tipoCancelacion);
        }

        // GET: TipoCancelacion/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoCancelacion/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,descripcion")] TipoCancelacion tipoCancelacion)
        {
            if (ModelState.IsValid)
            {
				process.Add(tipoCancelacion);
                return RedirectToAction("Index");
            }

            return View(tipoCancelacion);
        }

        // GET: TipoCancelacion/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
			TipoCancelacion tipoCancelacion = process.GetById(id);
			if (tipoCancelacion == null)
            {
                return HttpNotFound();
            }
            return View(tipoCancelacion);
        }

        // POST: TipoCancelacion/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,descripcion")] TipoCancelacion tipoCancelacion)
        {
            if (ModelState.IsValid)
            {
				process.Edit(tipoCancelacion);
                return RedirectToAction("Index");
            }
            return View(tipoCancelacion);
        }

        // GET: TipoCancelacion/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
			TipoCancelacion tipoCancelacion = process.GetById(id);
			if (tipoCancelacion == null)
            {
                return HttpNotFound();
            }
            return View(tipoCancelacion);
        }

        // POST: TipoCancelacion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
			TipoCancelacion tipoCancelacion = process.GetById(id);
			process.Remove(tipoCancelacion);
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
