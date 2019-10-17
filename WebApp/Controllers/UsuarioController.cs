using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dal;
using Model;
using WebApp.EF_DataModels;
using System.IO;
using System.Drawing;

namespace WebApp.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario
        dalUsuarios _db = new dalUsuarios();
        DB_OS_SISTEMASEntities dbContext = new DB_OS_SISTEMASEntities();

        public ActionResult Index()
        {
            if (Session["NomeLogin"] != null)
            {
                var model = _db.pubListaTodosOsUsuarios();

                return View(model);
            }
            else
            {
                return RedirectToAction("Logar", "Usuario");
            }
        }

        // GET: Usuario/
        public ActionResult Details(int id)
        {
            return View();
        }

        //Método para utilização de imagem de perfil dos usuários.
        public ActionResult GetImage()
        {
            byte[] imageBytes = (byte[])Session["ImagePerfil"];
            MemoryStream ms = new MemoryStream(imageBytes);
            return File(ms, "image/png", "myimage.png");
        }


        // GET: Usuario
        public ActionResult Create()
        {
            if (Session["NomeLogin"] != null)
            {
                ViewBag.Departamento = new SelectList(dbContext.TB_DEPARTAMENTO, "ID_DEPARTAMENTO", "NOME");
                ViewBag.TipoUsuario = new SelectList(dbContext.TB_TIPO_USUARIO, "ID_TIPO_USUARIO", "DESCRICAO");            

                return View();
            }
            else
            {
                return RedirectToAction("Logar", "Usuario");
            }
        }

        // POST: Usuario
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(modUsuarios usuarios)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    int anexo = 0;

                    for (int i = 0; i < Request.Files.Count; i++)
                    {
                        HttpPostedFileBase arquivo = Request.Files[i];
                       
                        if (arquivo.ContentLength > 0)
                        {
                            var imagem = Image.FromStream(arquivo.InputStream, true, true);
                            MemoryStream ms = new MemoryStream();
                            imagem.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                            anexo++;
                            usuarios.imagem = ms.ToArray();

                        }
                    }

                    usuarios.dtCadastro = DateTime.Now;
                    _db.pubCadastraNovoUsuario(usuarios);

                    return RedirectToAction("Index");

                }
                catch
                {
                    return View();
                }
            }
            else
            {
                ViewBag.Departamento = new SelectList(dbContext.TB_DEPARTAMENTO, "ID_DEPARTAMENTO", "NOME", usuarios.idDepartamento);
                ViewBag.TipoUsuario = new SelectList(dbContext.TB_TIPO_USUARIO, "ID_TIPO_USUARIO", "DESCRICAO", usuarios.idTipoUsuario);

                return View(usuarios);
            }           
        }

        // GET: Usuario/Edit/5
        public ActionResult Edit(int id)
        {
            if (Session["NomeLogin"] != null)
            {
                modUsuarios usuario = new modUsuarios();

                ViewBag.Departamento = new SelectList(dbContext.TB_DEPARTAMENTO, "ID_DEPARTAMENTO", "NOME");
                ViewBag.TipoUsuario = new SelectList(dbContext.TB_TIPO_USUARIO, "ID_TIPO_USUARIO", "DESCRICAO");

                var model = _db.pubBuscaUsuarioPorId(id);

                return View(model);
            }
            else
            {
                return RedirectToAction("Logar", "Usuario");
            }
        }

        // POST: Usuario/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, modUsuarios usuario)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    int anexo = 0;

                    for (int i = 0; i < Request.Files.Count; i++)
                    {
                        HttpPostedFileBase arquivo = Request.Files[i];
                        var imagem = Image.FromStream(arquivo.InputStream, true, true);
                        if (arquivo.ContentLength > 0)
                        {
                            MemoryStream ms = new MemoryStream();
                            imagem.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                            anexo++;
                            usuario.imagem = ms.ToArray();

                        }
                    }

                    usuario.idUsuario = id;              
                    usuario.dtAlteracao = DateTime.Now;

                    _db.pubAtualizaUsuario(usuario);

                    return RedirectToAction("Index");                             
                }
                catch
                {
                    return View(usuario);
                }
            }            
                ViewBag.Departamento = new SelectList(dbContext.TB_DEPARTAMENTO, "ID_DEPARTAMENTO", "NOME", usuario.idDepartamento);
                ViewBag.TipoUsuario = new SelectList(dbContext.TB_TIPO_USUARIO, "ID_TIPO_USUARIO", "DESCRICAO", usuario.idTipoUsuario);

                return View();            
        }

        // GET: Usuario/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Usuario/Delete/5
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

        [AllowAnonymous]
        public ActionResult Logar(string login, string senha)
        {
            modUsuarios usuarios = new modUsuarios();

            //TESTE IGOR NOTEBOOK

            if(login == null || senha == null)
            {
                return View();
            }
            else { 
                    if (ModelState.IsValid) {

                    var model = _db.pubUsuarioLogar(login, senha);

                    if (model != null)
                        {
                            Session["idUsuario"] = model.idUsuario;                       
                            Session["NomeLogin"] = login;
                            Session["ImagePerfil"] = model.imagem;                          
                            

                            return RedirectToAction("Index", "Home");
                        }

                        else
                        {                        
                            ViewBag.Erro = "Usuário ou senha inválidos, tente novamente!";
                            return View();
                        }
                }
                return View(usuarios);
            }
        }
        //Desloga usuário
        public ActionResult Deslogar()
        {
            int id = 0;

            id = Convert.ToInt32(Session["idUsuario"]);

            _db.pubUsuarioDeslogar(id);

            Session["idUsuario"] = null;
            Session["NomeLogin"] = null;
            Session["ImagePerfil"] = null;

            return RedirectToAction("Logar");
        }

        //Compara a sessão do login para identificar se está valida e usuário pode alterar nome/senha.
        public ActionResult PerfilUsuario(string login)
        {
            if (Session["NomeLogin"] != null)
            {
                login = Session["NomeLogin"].ToString();

                var model = _db.pubUsuarioPropriedadesPerfil(login);

                return View(model);
            }
            else
            {
                return RedirectToAction("Logar", "Usuario");
            }
        }

        //Utiliza o perfil para alterar o nome ou a senha da conta.
        [HttpPost]
        public ActionResult PerfilUsuario(string login, modUsuarios usuario)
        {
            int anexo = 0;

            for (int i = 0; i < Request.Files.Count; i++)
            {
                HttpPostedFileBase arquivo = Request.Files[i];
                var imagem = Image.FromStream(arquivo.InputStream, true, true);
                if (arquivo.ContentLength > 0)
                {
                    MemoryStream ms = new MemoryStream();
                    imagem.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    anexo++;
                    usuario.imagem = ms.ToArray();
                }
            }

            login = Session["NomeLogin"].ToString();

                usuario.login = login;

                _db.pubAtualizaPerfilUsuarioNomeSenha(usuario);

                return RedirectToAction("Index", "Home");          
        }

        public ActionResult RedefinirSenhaLogon()
        {
            return View();
        }
    }
}
