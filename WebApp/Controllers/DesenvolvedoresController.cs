using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;
using Dal;

namespace WebApp.Controllers
{
    public class DesenvolvedoresController : Controller
    {
        // GET: Desenvolvedores
        dalDesenvolvedores _db = new dalDesenvolvedores();
        public ActionResult Index()
        {
            if (Session["NomeLogin"] != null)
            {
                if (ModelState.IsValid)
                {
                    var model = _db.pubListaDesenvolvedores();

                    return View(model);
                }

                return View();
            }
            else
            {
                return RedirectToAction("Logar", "Usuario");
            }
        }

        // GET: Desenvolvedores/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Desenvolvedores/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Desenvolvedores/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Desenvolvedores/Edit/5
        public ActionResult Edit(int id)
        {
            if (Session["NomeLogin"] != null)
            {
                var model = _db.pubBuscaDesenvolvedorPorId(id);

                return View(model);
            }
            else
            {
                return RedirectToAction("Logar", "Usuario");
            }
        }

        // POST: Desenvolvedores/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, modDesenvolvedores dev)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    dev.idDev = id;
                    
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View(dev);
                }
            }

            return View();
            
        }

        // GET: Desenvolvedores/Delete/5
        public ActionResult Delete(int id)
        {
            var model = _db.pubBuscaDesenvolvedorPorId(id);

            return View(model);
        }

        // POST: Desenvolvedores/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, modDesenvolvedores dev)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    dev.idDev = id;
                    _db.pubRemoveDesenvolvedor(dev);

                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            }
            return View();
        }
    }
}
