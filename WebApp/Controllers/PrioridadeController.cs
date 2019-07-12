using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;
using Dal;

namespace WebApp.Controllers
{
    public class PrioridadeController : Controller
    {
        // GET: Prioridade
        dalPrioridade _db = new dalPrioridade();
        public ActionResult Index()
        {
            if (Session["NomeLogin"] != null)
            {
                var model = _db.pubListaPrioridades();

                return View(model);
            }
            else
            {
                return RedirectToAction("Logar", "Usuario");
            }
        }

        // GET: Prioridade/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Prioridade/Create
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

        // POST: Prioridade/Create
        [HttpPost]
        public ActionResult Create(modPrioridade prioridade)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _db.pubCadastraPrioridade(prioridade);

                    return RedirectToAction("Index");
                }
                catch
                {
                    return View(prioridade);
                }
            }
            return View();
        }

        // GET: Prioridade/Edit/5
        public ActionResult Edit(int id)
        {
            if (Session["NomeLogin"] != null)
            {
                var model = _db.pubBuscaPrioridadePorId(id);

                return View(model);
            }
            else
            {
                return RedirectToAction("Logar", "Usuario");
            }
        }

        // POST: Prioridade/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, modPrioridade prioridade)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    prioridade.idPrioridade = id;
                    _db.pubAtualizaPrioridade(prioridade);

                    return RedirectToAction("Index");
                }
                catch
                {
                    return View(prioridade);
                }
            }
            return View();
        }

        // GET: Prioridade/Delete/5
        public ActionResult Delete(int id)
        {
            if (Session["NomeLogin"] != null)
            {
                var model = _db.pubBuscaPrioridadePorId(id);

                return View(model);
            }
            else
            {
                return RedirectToAction("Logar", "Usuario");
            }
        }

        // POST: Prioridade/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, modPrioridade prioridade)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    prioridade.idPrioridade = id;
                    _db.pubRemovePrioridade(prioridade);

                    return RedirectToAction("Index");
                }
                catch
                {
                    return View(prioridade);
                }
            }
            return View();
        }
    }
}
