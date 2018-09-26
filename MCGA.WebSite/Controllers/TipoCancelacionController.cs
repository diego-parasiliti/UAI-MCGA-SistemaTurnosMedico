﻿using System;
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
using MCGA.Constants.TipoCancelacionController;

namespace MCGA.WebSite.Controllers
{
	[Authorize]
	public class TipoCancelacionController : Controller
    {
        private TipoCancelacionProcess process = new TipoCancelacionProcess();

		// GET: TipoCancelacion
		[Route("listado-tipo-cancelacion", Name = TipoCancelacionControllerRoute.GetIndex)]
		public ActionResult Index()
        {
			return View(TipoCancelacionControllerAction.Index, process.GetAll());
		}

		// GET: TipoCancelacion/Create
		[Route("agregar-tipo-cancelacion", Name = TipoCancelacionControllerRoute.GetCreate)]
		public ActionResult Create()
        {
			return View(TipoCancelacionControllerAction.Create);
		}

        // POST: TipoCancelacion/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
		[Route("agregar-tipo-cancelacion", Name = TipoCancelacionControllerRoute.PostCreate)]
		public ActionResult Create([Bind(Include = "Id,descripcion")] TipoCancelacion tipoCancelacion)
        {
            if (ModelState.IsValid)
            {
				process.Add(tipoCancelacion);
                return RedirectToAction("Index");
            }

            return View(TipoCancelacionControllerAction.Create, tipoCancelacion);
        }

		// GET: TipoCancelacion/Edit/5
		[Route("editar-tipo-cancelacion", Name = TipoCancelacionControllerRoute.GetEdit)]
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
			return View(TipoCancelacionControllerAction.Edit, tipoCancelacion);
		}

        // POST: TipoCancelacion/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
		[Route("editar-tipo-cancelacion", Name = TipoCancelacionControllerRoute.PostEdit)]
		public ActionResult Edit([Bind(Include = "Id,descripcion")] TipoCancelacion tipoCancelacion)
        {
            if (ModelState.IsValid)
            {
				process.Edit(tipoCancelacion);
                return RedirectToAction("Index");
            }
			return View(TipoCancelacionControllerAction.Edit, tipoCancelacion);
        }

		// GET: TipoCancelacion/Delete/5
		[Route("eliminar-tipo-cancelacion", Name = TipoCancelacionControllerRoute.GetDelete)]
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
			return View(TipoCancelacionControllerAction.Delete, tipoCancelacion);
        }

        // POST: TipoCancelacion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
		[Route("eliminar-tipo-cancelacion", Name = TipoCancelacionControllerRoute.PostDelete)]
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
