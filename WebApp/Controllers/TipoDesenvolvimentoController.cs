using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;
using Dal;

namespace WebApp.Controllers
{
    public class TipoDesenvolvimentoController : Controller
    {
        #region Lista todos os tipos de desenvolvimento cadastrados (View)
        // GET: TipoDesenvolvimento
        dalTipoDesenvolvimento _db = new dalTipoDesenvolvimento();
        public ActionResult Index()
        {
            if (Session["NomeLogin"] != null)
            {
                var model = _db.pubListaTiposDesenvolvimento();

                return View(model);
            }
            else
            {
                return RedirectToAction("Logar", "Usuario");
            }
        }
        #endregion

        // GET: TipoDesenvolvimento/Details/5
        public ActionResult Details(int id)
        {
            if (Session["NomeLogin"] != null)
            {
                var model = _db.pubTipoDesenvolvimentoPorId(id);

                return View(model);
            }
            else
            {
                return RedirectToAction("Logar", "Usuario");
            }
        }

        // GET: TipoDesenvolvimento/Create
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

        // POST: TipoDesenvolvimento/Create
        [HttpPost]
        public ActionResult Create(modTipoDesenvolvimento tpDesenvolvimento)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _db.pubCadastraTipoDesenvolvimento(tpDesenvolvimento);

                    return RedirectToAction("Index");
                }
                catch
                {
                    return View(tpDesenvolvimento);
                }
            }
            return View();
            
        }

        // GET: TipoDesenvolvimento/Edit/5
        public ActionResult Edit(int id)
        {
            if (Session["NomeLogin"] != null)
            {
                var model = _db.pubTipoDesenvolvimentoPorId(id);

                return View(model);
            }
            else
            {
                return RedirectToAction("Logar", "Usuario");
            }
        }

        // POST: TipoDesenvolvimento/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, modTipoDesenvolvimento tpDesenvolvimento)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    tpDesenvolvimento.idTipoDesenvolvimento = id;

                    _db.pubAtualizaTipoDesenvolvimento(tpDesenvolvimento);

                    return RedirectToAction("Index");
                }
                catch
                {
                    return View(tpDesenvolvimento);
                }
            }

            return View();           
        }

        // GET: TipoDesenvolvimento/Delete/5
        public ActionResult Delete(int id)
        {
            if (Session["NomeLogin"] != null)
            {
                var model = _db.pubTipoDesenvolvimentoPorId(id);

                return View(model);
            }
            else
            {
                return RedirectToAction("Logar", "Usuario");
            }
        }

        // POST: TipoDesenvolvimento/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, modTipoDesenvolvimento tpDesenvolvimento)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    tpDesenvolvimento.idTipoDesenvolvimento = id;

                    _db.pubRemoveTipoDesenvolvimentoPorId(tpDesenvolvimento);

                    return RedirectToAction("Index");
                }
                catch
                {
                    return View(tpDesenvolvimento);
                }
            }

            return View();          
        }
    }
}
