using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;
using Dal;
using WebApp.EF_DataModels;

namespace WebApp.Controllers
{
    public class OsProjetosController : Controller
    {
        // GET: OsProjetos
        dalProjetoEmDesenvolvimento _db = new dalProjetoEmDesenvolvimento();
        private DB_OS_SISTEMASEntities dbContext = new DB_OS_SISTEMASEntities();

        public ActionResult Index()
        {
            if (Session["NomeLogin"] != null)
            {
                var model = _db.pubListaTodosOsDesenvolvimento();

                return View(model);
            }
            else
            {
                return RedirectToAction("Logar", "Usuario");
            }
        }

        //GET: Detalhes de itens por Ordem de serviço / Lista (Collection)
        public ActionResult Details(int id)
        {
            if (Session["NomeLogin"] != null)
            {

                var model = _db.pubListaItensAtreladosNaOrdemdeServico(id);

                return View(model);
            }
            else
            {
                return RedirectToAction("Logar", "Usuario");
            }
        }    

        // GET: OsProjetos/Create
        public ActionResult Create()
        {
            if (Session["NomeLogin"] != null)
            {
                ViewBag.Projeto = new SelectList(dbContext.TB_PROJETOS_SISTEMAS, "ID_PROJETO", "NOME_PROJETO");

                return View();
            }
            else
            {
                return RedirectToAction("Logar", "Usuario");
            }
        }

        // POST: OsProjetos/Create
        [HttpPost]
        public ActionResult Create(modProjetoEmDesenvolvimento ordemServico)
        {           
               if (ModelState.IsValid)
                {
                    try
                    {
                        ordemServico.dtCadastro = DateTime.Now;
                        _db.pubCadastraNovoProjeto(ordemServico);

                        return RedirectToAction("Index");
                    }
                    catch
                    {
                        ViewBag.Projeto = new SelectList(dbContext.TB_PROJETOS_SISTEMAS, "ID_PROJETO", "NOME_PROJETO", ordemServico.idProjeto);
                        return View(ordemServico);
                    }
                }
                return View();           
        }

        // GET: OsProjetos/Edit/5
        public ActionResult Edit(int id)
        {
            if (Session["NomeLogin"] != null)
            {
                ViewBag.Projeto = new SelectList(dbContext.TB_PROJETOS_SISTEMAS, "ID_PROJETO", "NOME_PROJETO");

                var model = _db.pubBuscaOsProjetoPorId(id);            

                return View(model);
            }
            else
            {
                return RedirectToAction("Logar", "Usuario");
            }
        }

        // POST: OsProjetos/Edit/5
        [HttpPost]
        public ActionResult Edit(int id,modProjetoEmDesenvolvimento ordemServico)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ordemServico.idNmSolitacao = id;
                    ordemServico.idUsuarioLogado = Session["NomeLogin"].ToString();

                    _db.pubAtualizaFinalizaProjetoEmDesenvolvimento(ordemServico);

                    return RedirectToAction("Index");
                }
                catch
                {
                    ViewBag.Projeto = new SelectList(dbContext.TB_PROJETOS_SISTEMAS, "ID_PROJETO", "NOME_PROJETO", ordemServico.idProjeto);

                    return View(ordemServico);
                }
            }
            return View();
            
        }

        // GET: OsProjetos/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: OsProjetos/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult ListaItensDaOs(int id)
        {
            if (Session["NomeLogin"] != null)
            {
                var model = _db.pubListaItensAtreladosNaOrdemdeServico(id);              

                var combo = _db.pubComboListaProjetosEmDesenvolvimento().ToList();        

                ViewBag.ComboSeleciona = new SelectList(combo, "idNmSolitacao", "nomeProjeto");

                return View(model);
            }
            else
            {
                return RedirectToAction("Logar", "Usuario");
            }
        }

        //LISTA TODOS OS ITENS DE PROJETOS FINALIZADOS PARA VER DETALHES
        public ActionResult ListaItensDaOsFinalizada(int id)
        {
            if (Session["NomeLogin"] != null)
            {
                var model = _db.pubListaItensAtreladosNaOrdemdeServico(id);           

                return View(model);
            }
            else
            {
                return RedirectToAction("Logar", "Usuario");
            }
        }
    }
}
