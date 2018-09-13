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
    public class TipoResevaController : Controller
    {
        private TipoResevaProcess process = new TipoResevaProcess();

        // GET: TipoReseva
        public ActionResult Index()
        {
            return View(process.GetAll());
        }

        // GET: TipoReseva/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoReseva tipoReseva = process.GetById(id);
            if (tipoReseva == null)
            {
                return HttpNotFound();
            }
            return View(tipoReseva);
        }

        // GET: TipoReseva/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoReseva/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,descripcion")] TipoReseva tipoReseva)
        {
            if (ModelState.IsValid)
            {
				process.Add(tipoReseva);
                return RedirectToAction("Index");
            }

            return View(tipoReseva);
        }

        // GET: TipoReseva/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
			TipoReseva tipoReseva = process.GetById(id);
			if (tipoReseva == null)
            {
                return HttpNotFound();
            }
            return View(tipoReseva);
        }

        // POST: TipoReseva/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,descripcion")] TipoReseva tipoReseva)
        {
            if (ModelState.IsValid)
            {
				process.Edit(tipoReseva);
                return RedirectToAction("Index");
            }
            return View(tipoReseva);
        }

        // GET: TipoReseva/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
			TipoReseva tipoReseva = process.GetById(id);
			if (tipoReseva == null)
            {
                return HttpNotFound();
            }
            return View(tipoReseva);
        }

        // POST: TipoReseva/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
			TipoReseva tipoReseva = process.GetById(id);
			process.Remove(tipoReseva);
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
