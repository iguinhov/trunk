using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dal;
using Model;

namespace WebApp.Controllers
{
    public class DepartamentoController : Controller
    {
        // GET: Default
        dalDepartamento _db = new dalDepartamento();
        public ActionResult Index()
        {
            if (Session["NomeLogin"] != null)
            {
                var model = _db.pubListaDepartamentos();

                return View(model);
            }
            else
            {
                return RedirectToAction("Logar", "Usuario");
            }
        }

        // GET: Default/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Default/Create
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

        // POST: Default/Create
        [HttpPost]
        public ActionResult Create(modDepartamento departamento)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _db.pubCadastraDepartamento(departamento);
                }           
                
                return RedirectToAction("Index");
            }
            catch
            {
                return View(departamento);
            }
        }

        // GET: Default/Edit/5
        public ActionResult Edit(int id)
        {
            if (Session["NomeLogin"] != null)
            {
                var model = _db.pubBuscaDetalhesPorId(id);           

                return View(model);
            }
            else
            {
                return RedirectToAction("Logar", "Usuario");
            }
        }

        // POST: Default/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, modDepartamento departamento)
        {
            if (ModelState.IsValid)
            {
                try
                {
                     departamento.idDepartamento = id;
                    _db.pubAtualizaDepartamento(departamento);

                    return RedirectToAction("Index");
                }
                catch
                {
                    return View(departamento);
                }               
            }
            return View();
        }

        // GET: Default/Delete/5
        public ActionResult Delete(int id)
        {
            if (Session["NomeLogin"] != null)
            {
                var model = _db.pubBuscaDetalhesPorId(id);

                return View(model);
            }
            else
            {
                return RedirectToAction("Logar", "Usuario");
            }
        }

        // POST: Default/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, modDepartamento departamento)
        {
            if (ModelState.IsValid)
            {
                try
                {                
                    departamento.idDepartamento = id;

                    _db.pubRemoveDepartamentoPorId(id);

                    return RedirectToAction("Index");
                }
                
                catch
                {
                    return View(departamento);
                }            
            }
            return View();
        }
    }
}
