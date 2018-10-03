using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MCGA.Constants.TipoDiaController;
using MCGA.Entities;
using MCGA.UI.Process;

namespace MCGA.WebSite.Controllers
{
	[Authorize]
    public class TipoDiaController : Controller
    {
        private TipoDiaProcess process = new TipoDiaProcess();

		// GET: TipoDia
		[Route("listado-tipo-dia", Name = TipoDiaControllerRoute.GetIndex)]
		[Compress]
		public ActionResult Index()
        {
			return View(TipoDiaControllerAction.Index, process.GetAll());
        }

		// GET: TipoDia/Create
		[Route("agregar-tipo-dia", Name = TipoDiaControllerRoute.GetCreate)]
		[Compress]
		public ActionResult Create()
        {
			return View(TipoDiaControllerAction.Create);
        }

        // POST: TipoDia/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
		[Route("agregar-tipo-dia", Name = TipoDiaControllerRoute.PostCreate)]
		[Compress]
		public ActionResult Create([Bind(Include = "Id,descripcion")] TipoDia tipoDia)
        {
            if (ModelState.IsValid)
            {
                process.Add(tipoDia);
                return RedirectToAction("Index");
            }

			return View(TipoDiaControllerAction.Create, tipoDia);
        }

		// GET: TipoDia/Edit/5
		[Route("editar-tipo-dia", Name = TipoDiaControllerRoute.GetEdit)]
		[Compress]
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
            return View(TipoDiaControllerAction.Edit, tipoDia);
        }

        // POST: TipoDia/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
		[Route("editar-tipo-dia", Name = TipoDiaControllerRoute.PostEdit)]
		[Compress]
		public ActionResult Edit([Bind(Include = "Id,descripcion")] TipoDia tipoDia)
        {
            if (ModelState.IsValid)
            {
				process.Edit(tipoDia);
                return RedirectToAction("Index");
            }
			return View(TipoDiaControllerAction.Edit, tipoDia);
		}

		// GET: TipoDia/Delete/5
		[Route("eliminar-tipo-dia", Name = TipoDiaControllerRoute.GetDelete)]
		[Compress]
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
			return View(TipoDiaControllerAction.Delete, tipoDia);
        }

        // POST: TipoDia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
		[Route("eliminar-tipo-dia", Name = TipoDiaControllerRoute.PostDelete)]
		[Compress]
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
