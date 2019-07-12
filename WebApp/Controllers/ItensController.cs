using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;
using Dal;
using System.Net;
using System.Data;
using System.Data.Entity;
using WebApp.EF_DataModels;
using Repository;
using System.Data.SqlClient;
using System.IO;

namespace WebApp.Controllers
{
    public class ItensController : Controller
    {
        // GET: Itens
        dalItens _db = new dalItens();
        DB_OS_SISTEMASEntities dbContext = new DB_OS_SISTEMASEntities();

        public ActionResult Index()
        {
            if (Session["NomeLogin"] != null)
            {
                var model = dbContext.TB_PROJETO_ITENS.Include(t => t.TB_DEPARTAMENTO).Include(t => t.TB_DESENVOLVEDORES).Include(t => t.TB_PROJETO_DESENVOLVIMENTO).Include(t => t.TB_STATUS_DESENVOLVIMENTO).Include(t => t.TB_TIPO_DESENVOLVIMENTO).Include(t => t.TB_PRIORIDADE_ITEM);

                return View(model.ToList());
            }
            else
            {
                return RedirectToAction("Logar", "Usuario");
            }
        }

        // GET: Itens/Details/5
        public ActionResult Details(int id)
        {
            if (Session["NomeLogin"] != null)
            {

                ViewBag.Departamento = new SelectList(dbContext.TB_DEPARTAMENTO, "ID_DEPARTAMENTO", "NOME");
                ViewBag.Desenvolvedor = new SelectList(dbContext.TB_DESENVOLVEDORES, "ID_DEV", "NOME_DESENVOLVEDOR");
                ViewBag.TipoDesenvolvimento = new SelectList(dbContext.TB_TIPO_DESENVOLVIMENTO, "ID_TIPO_DESENV", "DESCRICAO");
                ViewBag.Status = new SelectList(dbContext.TB_STATUS_DESENVOLVIMENTO, "ID_STATUS", "DESCRICAO");
                ViewBag.Prioridade = new SelectList(dbContext.TB_PRIORIDADE_ITEM, "ID", "PRIORIDADE_TIPO");

                var model = _db.pubBuscaItemPorId(id);

                return View(model);
            }
            else
            {
                return RedirectToAction("Logar", "Usuario");
            }
        }

        //GET: Detalhe dos itens finalizadas
        public ActionResult ItemFinalizadoDetalhe(int id)
        {
            if (Session["NomeLogin"] != null)
            {
                var model = _db.pubItensFinalizadosDetalhe(id);

                return View(model);
            }
            else
            {
                return RedirectToAction("Logar", "Usuario");
            }

        }

        // GET: Itens/Create
        public ActionResult Create(int id)
        {
            if (Session["NomeLogin"] != null)
            {
                //Aqui efetua a validação para que não deixe efetuar abertura de um item se o id da OS (Build) for igual a 0.
                if (id == 0)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                // Se não for, carrega as combos de preenchimento e a tela de cadastro do novo item.
                    ViewBag.Departamento = new SelectList(dbContext.TB_DEPARTAMENTO, "ID_DEPARTAMENTO", "NOME");
                    ViewBag.Desenvolvedor = new SelectList(dbContext.TB_DESENVOLVEDORES, "ID_DEV", "NOME_DESENVOLVEDOR");
                    ViewBag.TipoDesenvolvimento = new SelectList(dbContext.TB_TIPO_DESENVOLVIMENTO, "ID_TIPO_DESENV", "DESCRICAO");
                    ViewBag.Status = new SelectList(dbContext.TB_STATUS_DESENVOLVIMENTO, "ID_STATUS", "DESCRICAO");
                    ViewBag.Prioridade = new SelectList(dbContext.TB_PRIORIDADE_ITEM, "ID", "PRIORIDADE_TIPO");
                
                    var model = _db.pubRegistraIdNovoItem(id);
                  
                    return View(model); // Carrega em tela combos, id da OS(Build) e o nome do projeto.
            }
            else
            {
                return RedirectToAction("Logar", "Usuario");
            }
        }

        // POST: Itens/Create
        [HttpPost]
        public ActionResult Create(int id, modItens item)
        {
            if (Session["NomeLogin"] != null)
            {
                try
                {
                    //aqui, seta as variáveis para salvar/gravar um novo item atrelado a uma OS (Build).
                    item.idNmSolicitacao = id;
                    item.dtAbertura = DateTime.Now;
                    item.idUsuarioLogado = Session["NomeLogin"].ToString(); //<-- Utiliza o login do usuário para enviar e-mail de abertura.

                    //anexo de arquivo no sistema
                    int anexo = 0;

                    for (int i = 0; i < Request.Files.Count; i++)
                    {
                        HttpPostedFileBase arquivo = Request.Files[i];

                        if(arquivo.ContentLength > 0)
                        {
                            var uploadPath = Server.MapPath("~/Images_Anexos");
                            string caminhoArq = Path.Combine(@uploadPath, Path.GetFileName(arquivo.FileName));

                            arquivo.SaveAs(caminhoArq);
                            anexo++;
                        }
                    }

                    ViewData["Mensagem"] = String.Format("{0} arquivo salvo com sucesso.", anexo);

                    //Salva no banco de dados o novo item aberto.
                    _db.pubCadastraNovoItem(item);

                    //Monta corpo do e-mail para envio ao desenvolvedor, admins do sistema e usuário abertura.
                    string corpo_email = "<html><head><title>Sistema de OS</title></head>"
                                       + "<body><h2>Item Nº: <b style='color:blue;'>{0}</b></h2> Projeto: <b>{1}</b> - Versão: <b>{2}</b></br><br>"
                                       + "<h3><b style='color:red;'><p>Aviso de novo item</p></b></h3>"
                                       + "</br>"
                                       + "<ul><li><b>Solicitante:</b> <u>{3}</u> - <b>Departamento:</b> {4} </li>"
                                       + "</br>"
                                       + "<li><b>Prioridade:</b> {5} - <b>Desenvolvedor Responsável:</b> {6} </li>"
                                       + "</br></br>"
                                       + "<li><b>Descrição:</b> {7} </li>"                                       
                                       + "</body></html>",
                    //designado = "<li><b>Data: </b>{0}<ul><li><b>Nome: </b>{1}</li></ul></li>", //<--- Ainda não está sendo utilizado.
                    corpo = string.Empty, //<--- Variável utilizada depois para concatenar o corpo do e-mail com os outros valores a serem preenchidos vindos do BD.
                    lst_hist = string.Empty,
                    lst_desi = string.Empty,
                    lst_prod = string.Empty;

                    //Cria variável que realiza consulta no banco, do último item criado para enviar e-mail.
                    var resultado = _db.pubConsultaItemParaEnvioEmail();

                    //Seta variáveis que serão utilizadas para o corpo do e-mail
                    var idItem = 0;
                    var projeto = string.Empty;
                    var versao = string.Empty;
                    var nomeSoli = string.Empty;
                    var depto = string.Empty;
                    var desc = string.Empty;
                    var prior = string.Empty;
                    var emailDesenv = string.Empty;
                    var emailReq = string.Empty;
                    var desenvolv = string.Empty;

                    //saída para o envio da mensagem
                    var msg = "E-mail enviado!";

                    //Busca cada campo a ser preenchido no envio de e-mail (carrega corpo_mail)
                    foreach (var final in resultado)
                    {
                        idItem = final.idItem; //incrementa as variáveis com os valores do select após save do item.
                        projeto = final.nomeProjeto;
                        versao = final.versao;
                        nomeSoli = final.solicitante;
                        depto = final.depto;
                        desc = final.desc;
                        prior = final.prioridade;
                        emailDesenv = final.emailDev;
                        emailReq = final.emailUser;
                        desenvolv = final.desenvolvedor;
                    }

                    //concatena o corpo do e-mail com os valores das variáveis, conforme posição dos campos no texto html acima, exemplo: {0}, {1}, {2}...
                    corpo = string.Format(corpo_email, idItem, projeto, versao, nomeSoli, depto, prior, desenvolv, desc);

                    //chama a classe do repositório onde está setado o envio do e-mail.
                    var envia = new EnviaEmailRepository();

                    //utiliza o método de envio de e-mail e concate o corpo, texto, campos a serem utilizados para envio conforme montado acima
                    envia.MandaEmail(string.Concat(emailReq, ";",emailDesenv),
                    string.Concat("Projeto: ", projeto, " Versão: ",versao," Item: [ ", idItem.ToString()," ]", " - Aviso de novo item"), corpo, out msg);
                    
                    //Após todo o processo, exibe mensagem em tela com número do item e que foi enviado ao desenvolvedor.
                    ViewBag.Mensagem = "Item Nº: " + idItem + " enviado para desenvolvimento!";

                    return ViewBag.Mensagem;                    
                }

                catch
                {
                    ViewBag.Departamento = new SelectList(dbContext.TB_DEPARTAMENTO, "ID_DEPARTAMENTO", "NOME");
                    ViewBag.Desenvolvedor = new SelectList(dbContext.TB_DESENVOLVEDORES, "ID_DEV", "NOME_DESENVOLVEDOR");
                    ViewBag.TipoDesenvolvimento = new SelectList(dbContext.TB_TIPO_DESENVOLVIMENTO, "ID_TIPO_DESENV", "DESCRICAO");
                    ViewBag.Status = new SelectList(dbContext.TB_STATUS_DESENVOLVIMENTO, "ID_STATUS", "DESCRICAO");
                    ViewBag.Prioridade = new SelectList(dbContext.TB_PRIORIDADE_ITEM, "ID", "PRIORIDADE_TIPO");

                    return View();
                }
            }
            else
            {
                return RedirectToAction("Logar", "Usuario");
            }

        }

        // GET: Itens/Edit/5
        public ActionResult FinalizarItem(int id)
        {
            if (Session["NomeLogin"] != null)
            {

                ViewBag.Departamento = new SelectList(dbContext.TB_DEPARTAMENTO, "ID_DEPARTAMENTO", "NOME");
                ViewBag.Desenvolvedor = new SelectList(dbContext.TB_DESENVOLVEDORES, "ID_DEV", "NOME_DESENVOLVEDOR");
                ViewBag.TipoDesenvolvimento = new SelectList(dbContext.TB_TIPO_DESENVOLVIMENTO, "ID_TIPO_DESENV", "DESCRICAO");
                ViewBag.Status = new SelectList(dbContext.TB_STATUS_DESENVOLVIMENTO, "ID_STATUS", "DESCRICAO");
                ViewBag.Prioridade = new SelectList(dbContext.TB_PRIORIDADE_ITEM, "ID", "PRIORIDADE_TIPO");

                var model = _db.pubBuscaItemPorId(id);

                return View(model);
            }
            else
            {
                return RedirectToAction("Logar", "Usuario");
            }
        }

        // POST: Itens/Edit/5
        [HttpPost]
        public ActionResult FinalizarItem(int id, modItens item)
        {
            if (Session["NomeLogin"] != null)
            {

                try
                {
                    item.idItem = id;
                    item.idUsuarioLogado = Session["NomeLogin"].ToString();
                    _db.pubDesenvolvedorFinalizaItem(item);

                    //Monta corpo do e-mail para envio ao desenvolvedor, admins do sistema e usuário abertura.
                    string corpo_email = "<html><head><title>Sistema de OS</title></head>"
                                       + "<body><h2>Item Nº: <b style='color:blue;'>{0}</b></h2> Projeto: <b>{1}</b> - Versão: <b>{2}</b></br><br>"
                                       + "<h3><b style='color:red;'><p>Item Finalizado</p></b></h3>"
                                       + "</br>"
                                       + "<ul><li><b>Solicitante:</b> <u>{3}</u> - <b>Departamento:</b> {4} </li>"
                                       + "</br>"
                                       + "<li><b>Prioridade:</b> {5} - <b>Desenvolvedor Responsável:</b> {6} , Status: {7} </li>"
                                       + "</br></br>"
                                       + "<li><b>Descrição Desenvolvimento:</b> {8} </li>"
                                       + "</br>"
                                       + "<li><b>Descrição da Solicitação:</b> {9} </li>"
                                       + "</body></html>",                    
                    corpo = string.Empty, //<--- Variável utilizada depois para concatenar o corpo do e-mail com os outros valores a serem preenchidos vindos do BD.
                    lst_hist = string.Empty,
                    lst_desi = string.Empty,
                    lst_prod = string.Empty;

                    //Cria variável que realiza consulta no banco, do último item criado para enviar e-mail.
                    var resultado = _db.pubConsultaPorIdItemEnvioEmail(id);

                    //Seta variáveis que serão utilizadas para o corpo do e-mail
                    var idItem = 0;
                    var projeto = string.Empty;
                    var versao = string.Empty;
                    var nomeSoli = string.Empty;
                    var depto = string.Empty;
                    var desc = string.Empty;
                    var prior = string.Empty;
                    var emailDesenv = string.Empty;
                    var emailReq = string.Empty;
                    var emailAlte = string.Empty;
                    var desenvolv = string.Empty;
                    var descDesen = string.Empty;
                    var tpStatus = string.Empty;

                    //saída para o envio da mensagem
                    var msg = "E-mail enviado!";

                    //Busca cada campo a ser preenchido no envio de e-mail (carrega corpo_mail)
                    foreach (var final in resultado)
                    {
                        idItem = final.idItem; //incrementa as variáveis com os valores do select após save do item.
                        projeto = final.nomeProjeto;
                        versao = final.versao;
                        nomeSoli = final.solicitante;
                        depto = final.depto;
                        desc = final.desc;
                        prior = final.prioridade;
                        emailDesenv = final.emailDev;
                        emailReq = final.emailUser;
                        emailAlte = final.emailAlter;
                        desenvolv = final.desenvolvedor;
                        descDesen = final.descricaoDev;
                        tpStatus = final.statusItem;
                    }

                    //concatena o corpo do e-mail com os valores das variáveis, conforme posição dos campos no texto html acima, exemplo: {0}, {1}, {2}...
                    corpo = string.Format(corpo_email, idItem, projeto, versao, nomeSoli, depto, prior, desenvolv, tpStatus, descDesen, desc);

                    //chama a classe do repositório onde está setado o envio do e-mail.
                    var envia = new EnviaEmailRepository();

                    if(emailReq == string.Empty || emailAlte == string.Empty)
                    {
                        //utiliza o método de envio de e-mail e concate o corpo, texto, campos a serem utilizados para envio conforme montado acima.
                        envia.MandaEmail(string.Concat(emailDesenv),
                        string.Concat("Projeto: ", projeto, " Versão: ", versao, " Item: [ ", idItem.ToString(), " ]", " - Finalizado :"), corpo, out msg);
                    }

                    else if (emailReq == string.Empty)
                    {                       
                        envia.MandaEmail(string.Concat(emailDesenv, "; ", emailAlte),
                        string.Concat("Projeto: ", projeto, " Versão: ", versao, " Item: [ ", idItem.ToString(), " ]", " - Finalizado :"), corpo, out msg);
                    }
                    else if(emailAlte == string.Empty)
                    {                        
                        envia.MandaEmail(string.Concat(emailDesenv, "; ", emailReq),
                        string.Concat("Projeto: ", projeto, " Versão: ", versao, " Item: [ ", idItem.ToString(), " ]", " - Finalizado :"), corpo, out msg);
                    }
                    else
                    {                        
                        envia.MandaEmail(string.Concat(emailDesenv, "; ",emailReq, "; ", emailAlte),
                        string.Concat("Projeto: ", projeto, " Versão: ", versao, " Item: [ ", idItem.ToString(), " ]", " - Finalizado :"), corpo, out msg);
                    }                 

                    //Após todo o processo, exibe mensagem em tela com número do item e que foi enviado ao desenvolvedor.
                    ViewBag.Mensagem = " Item Nº: "+ idItem + " Finalizado!";

                    return ViewBag.Mensagem;
                }
                catch
                {
                    ViewBag.Departamento = new SelectList(dbContext.TB_DEPARTAMENTO, "ID_DEPARTAMENTO", "NOME", item.idDepartamento);
                    ViewBag.Desenvolvedor = new SelectList(dbContext.TB_DESENVOLVEDORES, "ID_DEV", "NOME_DESENVOLVEDOR", item.idDev);
                    ViewBag.TipoDesenvolvimento = new SelectList(dbContext.TB_TIPO_DESENVOLVIMENTO, "ID_TIPO_DESENV", "DESCRICAO", item.idTipoDesenv);
                    ViewBag.Status = new SelectList(dbContext.TB_STATUS_DESENVOLVIMENTO, "ID_STATUS", "DESCRICAO", item.idStatus);
                    ViewBag.Prioridade = new SelectList(dbContext.TB_PRIORIDADE_ITEM, "ID", "PRIORIDADE_TIPO", item.idPrioridade);

                    return View();
                }
            }
            else
            {
                return RedirectToAction("Logar", "Usuario");
            }
        }

        // GET: Itens/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Itens/Delete/5
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

        //GET: Desenvolvedor atualiza status dos itens em desenvolvimento
        public ActionResult AtualizaStatus(int id)
        {
            if (Session["NomeLogin"] != null)
            {

                ViewBag.Departamento = new SelectList(dbContext.TB_DEPARTAMENTO, "ID_DEPARTAMENTO", "NOME");
                ViewBag.Desenvolvedor = new SelectList(dbContext.TB_DESENVOLVEDORES, "ID_DEV", "NOME_DESENVOLVEDOR");
                ViewBag.TipoDesenvolvimento = new SelectList(dbContext.TB_TIPO_DESENVOLVIMENTO, "ID_TIPO_DESENV", "DESCRICAO");
                ViewBag.Status = new SelectList(dbContext.TB_STATUS_DESENVOLVIMENTO, "ID_STATUS", "DESCRICAO");
                ViewBag.Prioridade = new SelectList(dbContext.TB_PRIORIDADE_ITEM, "ID", "PRIORIDADE_TIPO");

                var model = _db.pubBuscaItemPorId(id);

                return View(model);
            }
            else
            {
                return RedirectToAction("Logar", "Usuario");
            }

        }

        //POST: Desenvolvedor atualiza status dos itens em desenvolvimento
        [HttpPost]
        public ActionResult AtualizaStatus(int id, modItens item)
        {
            if (Session["NomeLogin"] != null)
            {
                try
                {
                    item.idItem = id;
                    _db.pubDesenvolvedorAtualizaStatusItem(item);

                    string corpo_email = "<html><head><title>Sistema de OS</title></head>"
                                      + "<body><h2>Item Nº: <b style='color:blue;'>{0}</b></h2> Projeto: <b>{1}</b> - Versão: <b>{2}</b></br><br>"
                                      + "<h3><b style='color:red;'><p>Status Alterado</p></b></h3>"
                                      + "</br>"
                                      + "<ul><li><b>Solicitante:</b> <u>{3}</u> - <b>Departamento:</b> {4} </li>"
                                      + "</br>"
                                      + "<li><b>Prioridade:</b> {5} - <b>Desenvolvedor Responsável:</b> {6} , Status do Item: <b style='color: red;'>{7}</b> </li>"
                                      + "</br></br>"                             
                                      + "<li><b>Descrição da Solicitação:</b> {8} </li>"
                                      + "</body></html>",
                   corpo = string.Empty, //<--- Variável utilizada depois para concatenar o corpo do e-mail com os outros valores a serem preenchidos vindos do BD.
                   lst_hist = string.Empty,
                   lst_desi = string.Empty,
                   lst_prod = string.Empty;

                    //Cria variável que realiza consulta no banco, do último item criado para enviar e-mail.
                    var resultado = _db.pubConsultaPorIdItemEnvioEmail(id);

                    //Seta variáveis que serão utilizadas para o corpo do e-mail
                    var idItem = 0;
                    var projeto = string.Empty;
                    var versao = string.Empty;
                    var nomeSoli = string.Empty;
                    var depto = string.Empty;
                    var desc = string.Empty;
                    var prior = string.Empty;
                    var emailDesenv = string.Empty;
                    var emailReq = string.Empty;
                    var emailAlte = string.Empty;
                    var desenvolv = string.Empty;
                    var descDesen = string.Empty;
                    var tpStatus = string.Empty;

                    //saída para o envio da mensagem
                    var msg = "E-mail enviado!";

                    //Busca cada campo a ser preenchido no envio de e-mail (carrega corpo_mail)
                    foreach (var final in resultado)
                    {
                        idItem = final.idItem; //incrementa as variáveis com os valores do select após save do item.
                        projeto = final.nomeProjeto;
                        versao = final.versao;
                        nomeSoli = final.solicitante;
                        depto = final.depto;
                        desc = final.desc;
                        prior = final.prioridade;
                        emailDesenv = final.emailDev;
                        emailReq = final.emailUser;
                        emailAlte = final.emailAlter;
                        desenvolv = final.desenvolvedor;
                        descDesen = final.descricaoDev;
                        tpStatus = final.statusItem;
                    }

                    //concatena o corpo do e-mail com os valores das variáveis, conforme posição dos campos no texto html acima, exemplo: {0}, {1}, {2}...
                    corpo = string.Format(corpo_email, idItem, projeto, versao, nomeSoli, depto, prior, desenvolv, tpStatus, desc);

                    //chama a classe do repositório onde está setado o envio do e-mail.
                    var envia = new EnviaEmailRepository();

                    if (emailReq == string.Empty || emailAlte == string.Empty)
                    {
                        //utiliza o método de envio de e-mail e concate o corpo, texto, campos a serem utilizados para envio conforme montado acima.
                        envia.MandaEmail(string.Concat(emailDesenv),
                        string.Concat("Projeto: ", projeto, " Versão: ", versao, " Item: [ ", idItem.ToString(), " ]", " - Status Alterado :"), corpo, out msg);
                    }

                    else if (emailReq == string.Empty)
                    {
                        //utiliza o método de envio de e-mail e concate o corpo, texto, campos a serem utilizados para envio conforme montado acima.
                        envia.MandaEmail(string.Concat(emailDesenv, "; ", emailAlte),
                        string.Concat("Projeto: ", projeto, " Versão: ", versao, " Item: [ ", idItem.ToString(), " ]", " - Status Alterado :"), corpo, out msg);
                    }
                    else if (emailAlte == string.Empty)
                    {
                        envia.MandaEmail(string.Concat(emailDesenv, "; ", emailReq),
                        string.Concat("Projeto: ", projeto, " Versão: ", versao, " Item: [ ", idItem.ToString(), " ]", " - Status Alterado :"), corpo, out msg);
                    }
                    else
                    {
                        envia.MandaEmail(string.Concat(emailDesenv, "; ", emailReq, "; ", emailAlte),
                        string.Concat("Projeto: ", projeto, " Versão: ", versao, " Item: [ ", idItem.ToString(), " ]", " - Status Alterado :"), corpo, out msg);
                    }                   

                    //Após todo o processo, exibe mensagem em tela com número do item e que foi enviado ao desenvolvedor.
                    ViewBag.Mensagem = "Status do Item Nº: " + idItem + " Alterado com sucesso!";

                    return ViewBag.Mensagem;
                }
                catch 
                {
                    ViewBag.Departamento = new SelectList(dbContext.TB_DEPARTAMENTO, "ID_DEPARTAMENTO", "NOME");
                    ViewBag.Desenvolvedor = new SelectList(dbContext.TB_DESENVOLVEDORES, "ID_DEV", "NOME_DESENVOLVEDOR");
                    ViewBag.TipoDesenvolvimento = new SelectList(dbContext.TB_TIPO_DESENVOLVIMENTO, "ID_TIPO_DESENV", "DESCRICAO");
                    ViewBag.Status = new SelectList(dbContext.TB_STATUS_DESENVOLVIMENTO, "ID_STATUS", "DESCRICAO");
                    ViewBag.Prioridade = new SelectList(dbContext.TB_PRIORIDADE_ITEM, "ID", "PRIORIDADE_TIPO");

                    return View();
                }
            }
            else
            {
                return RedirectToAction("Logar", "Usuario");
            }
        }

        //GET/POST: Pesquisa dos itens por status
        public ActionResult PesquisaItemPorStatus()
        {
            if (Session["NomeLogin"] != null)
            {
                ViewBag.Status = new SelectList(dbContext.TB_STATUS_DESENVOLVIMENTO, "ID_STATUS", "DESCRICAO");

                return View();
            }
            else
            {
                return RedirectToAction("Logar", "Usuario");
            }
        }

        [HttpPost]
        public ActionResult PesquisaItemPorStatus(int id)
        {            
            return RedirectToAction("ListaItemPorStatus", new { id = id});
        }

        //POST: Retorno da pesquisa dos itens por status       
        public ViewResult ListaItemPorStatus(int id)
        {          
                try
                {     
                    List<modItensListasPorStatus> ls = _db.pubListaOsStatusdosItensEmAberto(id).ToList();

                    return View(ls);
                }
                catch 
                {
                    return View();
                }            
        }

        //GET/POST: Lista todos os itens finalizados, localizando-os por desenvolvedor e com a data de finalização.
        public ActionResult PesquisaItensFinalizadosDesenvolvedor()
        {
            if (Session["NomeLogin"] != null)
            {
                ViewBag.Desenvolvedor = new SelectList(dbContext.TB_DESENVOLVEDORES, "ID_DEV", "NOME_DESENVOLVEDOR");

                return View();
            }
            else
            {
                return RedirectToAction("Logar", "Usuario");
            }
        }

        [HttpPost]
        public ActionResult PesquisaItensFinalizadosDesenvolvedor(int id)
        {
            ViewBag.Desenvolvedor = new SelectList(dbContext.TB_DESENVOLVEDORES, "ID_DEV", "NOME_DESENVOLVEDOR");

            return RedirectToAction("ListaItensFinalizadosDesenvolvedor", new { id = id });
        }

        //POST: Lista todos os itens finalizados, localizando-os por desenvolvedor e com a data de finalização.       
        public ViewResult ListaItensFinalizadosDesenvolvedor(int id)
        {            
                try
                {                 
                    List <modItensListasPorStatus> itens = _db.pubListaTodosItensEncerradosPorDesenvolvedor(id).ToList();

                    return View(itens);
                }
                catch
                {
                    return View();
                }          
        }

        public ActionResult AdminsAtualizaItemAberto(int id)
        {
            if (Session["NomeLogin"] != null)
            {

                ViewBag.Departamento = new SelectList(dbContext.TB_DEPARTAMENTO, "ID_DEPARTAMENTO", "NOME");
                ViewBag.Desenvolvedor = new SelectList(dbContext.TB_DESENVOLVEDORES, "ID_DEV", "NOME_DESENVOLVEDOR");
                ViewBag.TipoDesenvolvimento = new SelectList(dbContext.TB_TIPO_DESENVOLVIMENTO, "ID_TIPO_DESENV", "DESCRICAO");
                ViewBag.Status = new SelectList(dbContext.TB_STATUS_DESENVOLVIMENTO, "ID_STATUS", "DESCRICAO");
                ViewBag.Prioridade = new SelectList(dbContext.TB_PRIORIDADE_ITEM, "ID", "PRIORIDADE_TIPO");



                var model = _db.pubBuscaItemPorId(id);

                return View(model);
            }
            else
            {
                return RedirectToAction("Logar", "Usuario");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AdminsAtualizaItemAberto(int id, modItens item)
        {            
                try
                {
                    string login = Session["NomeLogin"].ToString();

                    item.idItem = id;
                    item.idUsuarioLogado = login;

                    _db.pubAdminAtualizaItensEmAberto(item);

                    //Monta corpo do e-mail para envio ao desenvolvedor, admins do sistema e usuário abertura.
                    string corpo_email = "<html><head><title>Sistema de OS</title></head>"
                                       + "<body><h2>Item Nº: <b style='color:blue;'>{0}</b></h2> Projeto: <b>{1}</b> - Versão: <b>{2}</b></br><br>"
                                       + "<h3><b style='color:red;'><p>Item Alterado</p></b></h3>"
                                       + "</br>"
                                       + "<ul><li><b>Solicitante:</b> <u>{3}</u> - <b>Departamento:</b> {4} </li>"
                                       + "</br>"
                                       + "<li><b>Prioridade:</b> {5} - <b>Desenvolvedor designado:</b> {6} , Status do Item: {7} </li>"
                                       + "</br></br>"
                                       + "<li><b>Descrição:</b> {8} </li>"
                                       + "</body></html>",                    
                    corpo = string.Empty, //<--- Variável utilizada depois para concatenar o corpo do e-mail com os outros valores a serem preenchidos vindos do BD.
                    lst_hist = string.Empty,
                    lst_desi = string.Empty,
                    lst_prod = string.Empty;

                    //Cria variável que realiza consulta no banco, do último item criado para enviar e-mail.
                    var resultado = _db.pubConsultaPorIdItemEnvioEmail(id);

                    //Seta variáveis que serão utilizadas para o corpo do e-mail
                    var idItem = 0;
                    var projeto = string.Empty;
                    var versao = string.Empty;
                    var nomeSoli = string.Empty;
                    var depto = string.Empty;
                    var desc = string.Empty;
                    var prior = string.Empty;
                    var emailDesenv = string.Empty;
                    var emailReq = string.Empty;
                    var desenvolv = string.Empty;
                    var emailAlter = string.Empty;
                    var statusIt = string.Empty;

                    //saída para o envio da mensagem
                    var msg = "E-mail enviado!";

                    //Busca cada campo a ser preenchido no envio de e-mail (carrega corpo_mail)
                    foreach (var final in resultado)
                    {
                        idItem = final.idItem; //incrementa as variáveis com os valores do select após save do item.
                        projeto = final.nomeProjeto;
                        versao = final.versao;
                        nomeSoli = final.solicitante;
                        depto = final.depto;
                        desc = final.desc;
                        prior = final.prioridade;
                        emailDesenv = final.emailDev;
                        emailReq = final.emailUser;
                        emailAlter = final.emailAlter;
                        desenvolv = final.desenvolvedor;
                        statusIt = final.statusItem;
                    }

                    //concatena o corpo do e-mail com os valores das variáveis, conforme posição dos campos no texto html acima, exemplo: {0}, {1}, {2}...
                    corpo = string.Format(corpo_email, idItem, projeto, versao, nomeSoli, depto, prior, desenvolv, statusIt, desc);

                    //chama a classe do repositório onde está setado o envio do e-mail.
                    var envia = new EnviaEmailRepository();

                    if (emailReq == string.Empty || emailAlter == string.Empty)
                    {
                        //utiliza o método de envio de e-mail e concate o corpo, texto, campos a serem utilizados para envio conforme montado acima.
                        envia.MandaEmail(string.Concat(emailDesenv),
                        string.Concat("Projeto: ", projeto, " Versão: ", versao, " Item: [ ", idItem.ToString(), " ]", " - Alterado :"), corpo, out msg);
                    }

                    else if (emailReq == string.Empty)
                    {                        
                        envia.MandaEmail(string.Concat(emailDesenv, "; ", emailAlter),
                        string.Concat("Projeto: ", projeto, " Versão: ", versao, " Item: [ ", idItem.ToString(), " ]", " - Alterado :"), corpo, out msg);
                    }
                    else if (emailAlter == string.Empty)
                    {
                        envia.MandaEmail(string.Concat(emailDesenv, "; ", emailReq),
                        string.Concat("Projeto: ", projeto, " Versão: ", versao, " Item: [ ", idItem.ToString(), " ]", " - Alterado :"), corpo, out msg);
                    }
                    else
                    {
                        envia.MandaEmail(string.Concat(emailDesenv, "; ", emailReq, "; ", emailAlter),
                        string.Concat("Projeto: ", projeto, " Versão: ", versao, " Item: [ ", idItem.ToString(), " ]", " - Alterado :"), corpo, out msg);
                    }             

                    //Após todo o processo, exibe mensagem em tela com número do item e que foi enviado ao desenvolvedor.
                    ViewBag.Mensagem = "Item Nº: " + idItem + " alterado com sucesso!";

                    return ViewBag.Mensagem;
                }
                catch 
                {
                    ViewBag.Departamento = new SelectList(dbContext.TB_DEPARTAMENTO, "ID_DEPARTAMENTO", "NOME");
                    ViewBag.Desenvolvedor = new SelectList(dbContext.TB_DESENVOLVEDORES, "ID_DEV", "NOME_DESENVOLVEDOR");
                    ViewBag.TipoDesenvolvimento = new SelectList(dbContext.TB_TIPO_DESENVOLVIMENTO, "ID_TIPO_DESENV", "DESCRICAO");
                    ViewBag.Status = new SelectList(dbContext.TB_STATUS_DESENVOLVIMENTO, "ID_STATUS", "DESCRICAO");
                    ViewBag.Prioridade = new SelectList(dbContext.TB_PRIORIDADE_ITEM, "ID", "PRIORIDADE_TIPO");

                    ViewBag.Erro = "Problema ao tentar gerar solicitação ! ";

                    return View(item);
                }           
        }

     }
}
