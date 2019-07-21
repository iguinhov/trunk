using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.EF_DataModels;
using Model;
using Dal;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        private DB_OS_SISTEMASEntities _db = new DB_OS_SISTEMASEntities();
        dalItens _dbItem = new dalItens();
        modItens mdItem = new modItens();

        public ActionResult Index()
        {
            if(Session["NomeLogin"] != null)
            {
                try
                {
                    var graf1 = (from desenv in _db.TB_DESENVOLVEDORES
                                 join item in _db.TB_PROJETO_ITENS on desenv.ID_DEV equals item.ID_DEV
                                 where item.FL_ITEM_ENCERRADO == false 
                                 group desenv by desenv.NOME_DESENVOLVEDOR into j
                                 select new
                                 {
                                     desenvolvedor = j.Key,
                                     quantidade = j.Count()

                                 }).ToList();
                    ViewBag.GrafItensDesenvolvedor = JsonConvert.SerializeObject(graf1, _jsonSetting);


                    var graf2 = (from desenv in _db.TB_DESENVOLVEDORES
                                 join item in _db.TB_PROJETO_ITENS on desenv.ID_DEV equals item.ID_DEV
                                 where item.FL_ITEM_ENCERRADO == true
                                 group desenv by desenv.NOME_DESENVOLVEDOR into j
                                 select new
                                 {
                                     desenvolvedor = j.Key,
                                     quantidade = j.Count()

                                 }).ToList();
                    ViewBag.itensEncerradosDesenvolvedor = JsonConvert.SerializeObject(graf2, _jsonSetting);


                    var graf3 = (from status in _db.TB_STATUS_DESENVOLVIMENTO
                                 join item in _db.TB_PROJETO_ITENS on status.ID_STATUS equals item.ID_STATUS                                 
                                 group status by status.DESCRICAO into j
                                 select new
                                 {
                                     status = j.Key,
                                     quantidade = j.Count()

                                 }).ToList();
                    ViewBag.qntdStatus = JsonConvert.SerializeObject(graf3, _jsonSetting);

                    string login = Session["NomeLogin"].ToString();

                    var model = _dbItem.pubListaItensEmAbertoParaDesenvolvedor(login);

                    //Retorna usuários logados Chat
                    List<string> logins = new List<string>();

                    var query = from l in _db.TB_LOGON_SISTEMA
                                join u in _db.TB_USUARIOS on l.ID_USUARIO equals u.ID_USUARIO
                                where l.FL_USUARIO_LOGADO == true
                                group u by u.LOGIN;

                    foreach (var item in query)
                    {
                        logins.Add(item.Key.ToString());
                    }

                    ViewBag.usuLogados = logins.ToList();

                    return View(model);   
                }

                catch (EntityException ex)
                {
                    return View("Erro : - " + ex.Message);   
                }

                catch (SqlException)
                {
                    return View("Erro SQL : - ");
                }
            }
            else
            {
                return RedirectToAction("Logar", "Usuario");
            }            
        }
        JsonSerializerSettings _jsonSetting = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };

        public ActionResult TestChat()
        {
            return View();
        }
    }   
}