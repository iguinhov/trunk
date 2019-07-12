using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class modUsuarios
    {
        
        private Int32 _idUsuario;        
        private string _nome;      
        private Int32 _idDepartamento;               
        private string _email;       
        private string _login;  
        private string _senha;        
        private DateTime _dtCadastro;         
        private Int32 _idTipoUsuario;        
        private bool? _flAtivo;        
        private bool? _flDev;        
        private DateTime _dtAlteracao;       
        private byte[] _imagem;

        //Usuário retorno string <LIST>
        private string _departamentoRetorno;
        private string _tipoUsuarioRetorno;
        private string _flAtivoRetorno;
        private string _flDesenvolvedorRetorno;

        public modUsuarios()
        {

        }

        [Display(Name = "Id Usuário")]
        public Int32 idUsuario {
            get { return _idUsuario; }
            set { _idUsuario = value; }
        }
        [Display(Name = "Nome")]
        [Required(ErrorMessage = "O nome do usuário é obrigatório", AllowEmptyStrings = false)]
        [MinLength(4, ErrorMessage = "O tamanho mínimo do nome são 4 caracteres.")]
        [StringLength(200, ErrorMessage = "O tamanho máximo são 200 caracteres.")]
        public string nome {
            get { return _nome; }
            set { _nome = value; }
        }
        [Display(Name = "Departamento")]
        public Int32 idDepartamento {
            get { return _idDepartamento; }
            set { _idDepartamento = value; }
        }
        [Display(Name = "E-mail")]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Informe um email válido...")]
        public string email {
            get { return _email; }
            set { _email = value; }
        }
        [Display(Name = "Login")]
        [Required(ErrorMessage = "O campo LOGIN é obrigatório.")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "O login deve conter ao menos 5 letras.")]
        public string login {
            get { return _login; }
            set { _login = value; }
        }
        [Display(Name = "Senha")]
        [Required(ErrorMessage = "Informe a senha.", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        [StringLength(10, MinimumLength = 6, ErrorMessage = "A senha deve conter entre 6 e 10 caracteres.")]        
        public string senha {
            get { return _senha; }
            set { _senha = value; }
        }
        [Display(Name = "Data de Criação")]
        public DateTime dtCadastro {
            get { return _dtCadastro; }
            set { _dtCadastro = value; }
        }
        [Display(Name = "Tipo de usuário")]
        [Required(ErrorMessage = "O tipo do usuário é obrigatório!")]
        public Int32 idTipoUsuario {
            get { return _idTipoUsuario; }
            set { _idTipoUsuario = value; }
        }
        [Display(Name = "Ativo")]
        public bool? flAtivo {
            get { return _flAtivo; }
            set { _flAtivo = value; }
        }
        [Display(Name = "Desenvolvedor")]
        public bool? flDev {
            get { return _flDev; }
            set { _flDev = value; }
        }
        [Display(Name = "Data Alteração")]
        public DateTime dtAlteracao {
            get { return _dtAlteracao; }
            set { _dtAlteracao = value; }
        }
        [Display(Name = "Imagem Perfil")]
        public byte[] imagem {
            get { return _imagem; }
            set { _imagem = value; }
        }


        //Retorno Strings List
        [Display(Name ="Departamento")]
        public string departamentoRetorno {
            get { return _departamentoRetorno; }
            set { _departamentoRetorno = value; }
        }

        [Display(Name = "Permissão")]
        public string tipoUsuarioRetorno
        {
            get { return _tipoUsuarioRetorno; }
            set { _tipoUsuarioRetorno = value; }
        }

        [Display(Name = "Ativo")]
        public string flAtivoRetorno
        {
            get { return _flAtivoRetorno; }
            set { _flAtivoRetorno = value; }
        }

        [Display(Name = "Desenvolvedor")]
        public string flDesenvolvedorRetorno
        {
            get { return _flDesenvolvedorRetorno; }
            set { _flDesenvolvedorRetorno = value; }
        }

    }
}
