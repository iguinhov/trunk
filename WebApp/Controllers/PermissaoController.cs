using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;
using Dal;

namespace WebApp.Controllers
{
    public class PermissaoController : Controller
    {
        // GET: Permissao
        dalTipoUsuario _db = new dalTipoUsuario(); 
        public ActionResult Index()
        {
            if (Session["NomeLogin"] != null)
            {
                var model = _db.pubListaTipoUsuarios();

                return View(model);
            }
            else
            {
                return RedirectToAction("Logar", "Usuario");
            }
        }

        // GET: Permissao/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Permissao/Create
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

        // POST: Permissao/Create
        [HttpPost]
        public ActionResult Create(modTipoUsuario tpUsuario)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _db.pubCadastraTipoUsuario(tpUsuario);

                    return RedirectToAction("Index");
                }

                catch
                {
                    return View(tpUsuario);
                }
            }
            return View();
        }

        // GET: Permissao/Edit/5
        public ActionResult Edit(int id)
        {
            if (Session["NomeLogin"] != null)
            {
                var model = _db.pubTipoUsuarioPorId(id);

                return View(model);
            }
            else
            {
                return RedirectToAction("Logar", "Usuario");
            }
        }

        // POST: Permissao/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, modTipoUsuario tpUsuario)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    tpUsuario.idTipoUsuario = id;
                    _db.pubAtualizaTipoUsuario(tpUsuario);

                    return RedirectToAction("Index");
                }
                catch
                {
                    return View(tpUsuario);
                }
            }

            return View();
        }

        // GET: Permissao/Delete/5
        public ActionResult Delete(int id)
        {
            if (Session["NomeLogin"] != null)
            {
                var model = _db.pubTipoUsuarioPorId(id);

                return View(model);
            }
            else
            {
                return RedirectToAction("Logar", "Usuario");
            }
        }

        // POST: Permissao/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, modTipoUsuario tpUsuario)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    tpUsuario.idTipoUsuario = id;
                    _db.pubRemoveUsuarioPorId(tpUsuario);

                    return RedirectToAction("Index");

                }

                catch
                {
                    return View(tpUsuario);
                }
            }
            return View();
        }
    }
}
