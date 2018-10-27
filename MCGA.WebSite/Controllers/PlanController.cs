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
    public class PlanController : Controller
    {
        private PlanProcess process = new PlanProcess();

		// GET: Plan
		[Route("listado-plan", Name = PlanControllerRoute.GetIndex)]
		public ActionResult Index()
        {
			return View(process.GetAll());
		}

		// GET: Plan/Create
		[Route("agregar-plan", Name = PlanControllerRoute.GetCreate)]
		public ActionResult Create()
        {
            return View();
        }

        // POST: Plan/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
		[Route("agregar-plan", Name = PlanControllerRoute.PostCreate)]
		public ActionResult Create([Bind(Include = "Id,descripcion,precio_bono_consulta,precio_bono_farmacia")] Plan plan)
        {
            if (ModelState.IsValid)
            {
				process.Add(plan);
                return RedirectToAction("Index");
            }

            return View(plan);
        }

		// GET: Plan/Edit/5
		[Route("editar-plan", Name = PlanControllerRoute.GetEdit)]
		public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Plan plan = process.GetById(id);
            if (plan == null)
            {
                return HttpNotFound();
            }
            return View(plan);
        }

        // POST: Plan/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
		[Route("editar-plan", Name = PlanControllerRoute.PostEdit)]
		public ActionResult Edit([Bind(Include = "Id,descripcion,precio_bono_consulta,precio_bono_farmacia")] Plan plan)
        {
            if (ModelState.IsValid)
            {
				process.Edit(plan); 
				return RedirectToAction("Index");
            }
            return View(plan);
        }

		// GET: Plan/Delete/5
		[Route("eliminar-plan", Name = PlanControllerRoute.GetDelete)]
		public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Plan plan = process.GetById(id);
            if (plan == null)
            {
                return HttpNotFound();
            }
            return View(plan);
        }

        // POST: Plan/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
		[Route("eliminar-plan", Name = PlanControllerRoute.PostDelete)]
		public ActionResult DeleteConfirmed(int id)
        {
            Plan plan = process.GetById(id);
			process.Remove(plan);
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
