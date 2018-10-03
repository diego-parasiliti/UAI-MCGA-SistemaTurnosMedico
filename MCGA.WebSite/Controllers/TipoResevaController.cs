using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MCGA.Constants.TipoResevaController;
using MCGA.Data;
using MCGA.Entities;
using MCGA.UI.Process;

namespace MCGA.WebSite.Controllers
{
	[Authorize]
	public class TipoResevaController : Controller
    {
		private TipoResevaProcess process = new TipoResevaProcess();

		// GET: TipoReseva
		[Route("listado-tipo-reserva", Name = TipoResevaControllerRoute.GetIndex)]
		[Compress]
		public ActionResult Index()
        {
			return View(TipoResevaControllerAction.Index, process.GetAll());
		}

		// GET: TipoReseva/Create
		[Route("agregar-tipo-reserva", Name = TipoResevaControllerRoute.GetCreate)]
		[Compress]
		public ActionResult Create()
        {
			return View(TipoResevaControllerAction.Create);
		}

        // POST: TipoReseva/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
		[Route("agregar-tipo-reserva", Name = TipoResevaControllerRoute.PostCreate)]
		[Compress]
		public ActionResult Create([Bind(Include = "Id,descripcion")] TipoReseva tipoReseva)
        {
            if (ModelState.IsValid)
            {
				process.Add(tipoReseva);
                return RedirectToAction("Index");
            }
			return View(TipoResevaControllerAction.Create, tipoReseva);
		}

		// GET: TipoReseva/Edit/5
		[Route("editar-tipo-reserva", Name = TipoResevaControllerRoute.GetEdit)]
		[Compress]
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
			return View(TipoResevaControllerAction.Edit, tipoReseva);
        }

        // POST: TipoReseva/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
		[Route("editar-tipo-reserva", Name = TipoResevaControllerRoute.PostEdit)]
		[Compress]
		public ActionResult Edit([Bind(Include = "Id,descripcion")] TipoReseva tipoReseva)
        {
            if (ModelState.IsValid)
            {
				process.Edit(tipoReseva);
                return RedirectToAction("Index");
            }
			return View(TipoResevaControllerAction.Edit, tipoReseva);
		}

		// GET: TipoReseva/Delete/5
		[Route("eliminar-tipo-reserva", Name = TipoResevaControllerRoute.GetDelete)]
		[Compress]
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
			return View(TipoResevaControllerAction.Delete, tipoReseva);
        }

        // POST: TipoReseva/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
		[Route("eliminar-tipo-reserva", Name = TipoResevaControllerRoute.PostDelete)]
		[Compress]
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
