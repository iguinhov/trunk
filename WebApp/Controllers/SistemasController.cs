using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;
using Dal;

namespace WebApp.Controllers
{
    public class SistemasController : Controller
    {
        // GET: Sistemas
        dalSistemasProjetos _db = new dalSistemasProjetos();
        public ActionResult Index()
        {
            if (Session["NomeLogin"] != null)
            {
                var model = _db.pubListaProjetos();

                return View(model);
            }
            else
            {
                return RedirectToAction("Logar", "Usuario");
            }
        }

        // GET: Sistemas/Details/5
        public ActionResult Details(int id)
        {
            if (Session["NomeLogin"] != null)
            {
                var model = _db.pubBuscaProjetoPorId(id);
                return View(model);
            }
            else
            {
                return RedirectToAction("Logar", "Usuario");
            }
        }

        // GET: Sistemas/Create
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

        // POST: Sistemas/Create
        [HttpPost]
        public ActionResult Create(modSistemasProjeto projetos)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _db.pubCadastraProjeto(projetos);

                    return RedirectToAction("Index");
                }
                catch
                {
                    return View(projetos);
                }
            }
            return View();           
        }

        // GET: Sistemas/Edit/5
        public ActionResult Edit(int id)
        {
            if (Session["NomeLogin"] != null)
            {
                var model = _db.pubBuscaProjetoPorId(id);

                return View(model);
            }
            else
            {
                return RedirectToAction("Logar", "Usuario");
            }
        }

        // POST: Sistemas/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, modSistemasProjeto projetos)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    projetos.idProjeto = id;
                    _db.pubAtualizaProjeto(projetos);

                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            }

            return View();
           
        }

        // GET: Sistemas/Delete/5
        public ActionResult Delete(int id)
        {
            if (Session["NomeLogin"] != null)
            {
                var model = _db.pubBuscaProjetoPorId(id);

                return View(model);
            }
            else
            {
                return RedirectToAction("Logar", "Usuario");
            }
        }

        // POST: Sistemas/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, modSistemasProjeto projetos)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    projetos.idProjeto = id;
                    _db.pubRemoveProjeto(projetos);

                    return RedirectToAction("Index");
                }
                catch
                {
                    return View(projetos);
                }
            }
            return View();           
        }
    }
}
