using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MCGA.Entities;
using MCGA.UI.Process;

namespace MCGA.WebSite.Controllers
{
	[Authorize]
    public class TipoDiaController : Controller
    {
        private TipoDiaProcess process = new TipoDiaProcess();

        // GET: TipoDia
        public ActionResult Index()
        {
			return View(process.GetAll());
        }

        // GET: TipoDia/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
			TipoDia tipoDia = process.GetById(id);
            if (tipoDia == null)
            {
                return HttpNotFound();
            }
            return View(tipoDia);
        }

        // GET: TipoDia/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoDia/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,descripcion")] TipoDia tipoDia)
        {
            if (ModelState.IsValid)
            {
                process.Add(tipoDia);
                return RedirectToAction("Index");
            }

            return View(tipoDia);
        }

        // GET: TipoDia/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoDia tipoDia = process.GetById(id);
            if (tipoDia == null)
            {
                return HttpNotFound();
            }
            return View(tipoDia);
        }

        // POST: TipoDia/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,descripcion")] TipoDia tipoDia)
        {
            if (ModelState.IsValid)
            {
				process.Edit(tipoDia);
                return RedirectToAction("Index");
            }
            return View(tipoDia);
        }

        // GET: TipoDia/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoDia tipoDia = process.GetById(id);
            if (tipoDia == null)
            {
                return HttpNotFound();
            }
            return View(tipoDia);
        }

        // POST: TipoDia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TipoDia tipoDia = process.GetById(id);
			process.Remove(tipoDia);
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
