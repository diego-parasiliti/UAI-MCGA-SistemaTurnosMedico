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
	[Authorize]
	public class TipoSexoController : Controller
    {
        private TipoSexoProcess process = new TipoSexoProcess();

		// GET: TipoSexo
		[Route("listado-tipo-sexo", Name = TipoSexoControllerRoute.GetIndex)]
		public ActionResult Index()
        {
			return View(TipoSexoControllerAction.Index, process.GetAll());
		}

		// GET: TipoSexo/Create
		[Route("agregar-tipo-sexo", Name = TipoSexoControllerRoute.GetCreate)]
		public ActionResult Create()
        {
			return View(TipoSexoControllerAction.Create);
		}

        // POST: TipoSexo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
		[Route("agregar-tipo-sexo", Name = TipoSexoControllerRoute.PostCreate)]
		public ActionResult Create([Bind(Include = "Id,descripcion")] TipoSexo tipoSexo)
        {
            if (ModelState.IsValid)
            {
				process.Add(tipoSexo);
				return RedirectToAction("Index");
            }
			return View(TipoSexoControllerAction.Create, tipoSexo);
        }

		// GET: TipoSexo/Edit/5
		[Route("editar-tipo-sexo", Name = TipoSexoControllerRoute.GetEdit)]
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
			return View(TipoSexoControllerAction.Edit, tipoSexo);
		}

        // POST: TipoSexo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
		[Route("editar-tipo-sexo", Name = TipoSexoControllerRoute.PostEdit)]
		public ActionResult Edit([Bind(Include = "Id,descripcion")] TipoSexo tipoSexo)
        {
            if (ModelState.IsValid)
            {
				process.Edit(tipoSexo);
				return RedirectToAction("Index");
            }
			return View(TipoSexoControllerAction.Edit, tipoSexo);
		}

		// GET: TipoSexo/Delete/5
		[Route("eliminar-tipo-sexo", Name = TipoSexoControllerRoute.GetDelete)]
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
			return View(TipoSexoControllerAction.Delete, tipoSexo);
        }

        // POST: TipoSexo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
		[Route("eliminar-tipo-sexo", Name = TipoSexoControllerRoute.PostDelete)]
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
