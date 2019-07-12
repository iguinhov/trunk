using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;
using Dal;

namespace WebApp.Controllers
{
    public class StatusController : Controller
    {
        // GET: Status
        dalStatus _db = new dalStatus();
        public ActionResult Index()
        {
            if (Session["NomeLogin"] != null)
            {
                var model = _db.pubListaStatus();

                return View(model);
            }
            else
            {
                return RedirectToAction("Logar", "Usuario");
            }
        }

        // GET: Status/Details/5
        public ActionResult Details(int id)
        {
            if (Session["NomeLogin"] != null)
            {
                var model = _db.pubBuscaStatusPorId(id);

                return View(model);
            }
            else
            {
                return RedirectToAction("Logar", "Usuario");
            }
        }

        // GET: Status/Create
        public ActionResult Create()
        {
            if (Session["NomeLogin"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Logar", "Usuario");
            }
        }

        // POST: Status/Create
        [HttpPost]
        public ActionResult Create(modStatus status)
        {
            if (ModelState.IsValid)
            {
                try
                {                    
                    _db.pubCadastraStatus(status);

                    return RedirectToAction("Index");
                }
                catch
                {
                    return View(status);
                }
            }
            return View();
            
        }

        // GET: Status/Edit/5
        public ActionResult Edit(int id)
        {
            if (Session["NomeLogin"] != null)
            {
                var model = _db.pubBuscaStatusPorId(id);

                return View(model);
            }
            else
            {
                return RedirectToAction("Logar", "Usuario");
            }
        }

        // POST: Status/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, modStatus status)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    status.idStatus = id;
                    _db.pubAtualizaStatus(status);

                    return RedirectToAction("Index");
                }
                catch
                {
                    return View(status);
                }
            }
            return View();
            
        }

        // GET: Status/Delete/5
        public ActionResult Delete(int id)
        {
            if (Session["NomeLogin"] != null)
            {
                var model = _db.pubBuscaStatusPorId(id);

                return View(model);
            }
            else
            {
                return RedirectToAction("Logar", "Usuario");
            }
        }

        // POST: Status/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, modStatus status)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    status.idStatus = id;

                    _db.pubRemoveStatusPorId(status);

                    return RedirectToAction("Index");
                }
                catch
                {
                    return View(status);
                }
            }
            return View();
           
        }
    }
}
