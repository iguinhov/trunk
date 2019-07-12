using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class modItens
    {
        
        private int _idItem;        
        private int _idNmSolicitacao;         
        private string _NomeSolicitante;        
        private int _idDepartamento;       
        private DateTime _dtAbertura;             
        private string _descicao;       
        private int _idDev;                
        private int? _idTipoDesenv;        
        private int? _idStatus;       
        private string _camadasMetodos;        
        private string _proceduresNomes;        
        private string _descDesenvolvimento;       
        private DateTime _dtTermino;       
        private bool? _flCommit;       
        private TimeSpan _totalHoras;        
        private bool? _flItemEncerrado;       
        private DateTime _dtPublicacao;       
        private int _idPrioridade;      
        private string _idUsuarioLogado;
        private string _nomeProjeto;
        private DateTime _dtProgramada;
        private string _tituloItem;       

        //Método construtor da classe
        public modItens() { }

        [Display(Name = "Id item")]
        public int idItem {
            get { return _idItem; }
            set { _idItem = value; }
        }
        [Display(Name = "Número da ordem de serviço")]
        [Required(ErrorMessage = "O número da ordem de serviço é obrigatorio.", AllowEmptyStrings = false)]
        public int idNmSolicitacao {
            get { return _idNmSolicitacao; }
            set { _idNmSolicitacao = value; }
        }
        [Display(Name = "Nome do solicitante")]
        [RegularExpression("^([^0-9])*$", ErrorMessage = "Esse campo não deve conter números")]
        [Required(ErrorMessage = "O nome do solicitante é obrigatorio.", AllowEmptyStrings = false)]
        public string NomeSolicitante {
            get { return _NomeSolicitante; }
            set { _NomeSolicitante = value; }
        }
        [Display(Name = "Departamento")]
        public int idDepartamento {
            get { return _idDepartamento; }
            set { _idDepartamento = value; }
        }
        [Display(Name = "Data de Abertura")]
        [DisplayFormat(ApplyFormatInEditMode = true)]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido.")]
        public DateTime dtAbertura {
            get { return _dtAbertura; }
            set { _dtAbertura = value; }
        }
        [Display(Name = "Descrição")]
        [StringLength(5000, MinimumLength = 5, ErrorMessage = "A descrição deve conter ao menos {1} letras e no máximo {2}.")]
        public string descicao {
            get { return _descicao; }
            set { _descicao = value; }
        }
        [Display(Name = "Desenvolvedor")]
        public int idDev {
            get { return _idDev; }
            set { _idDev = value; }
        }
        [Display(Name = "Tipo de desenvolvimento")]
        public int? idTipoDesenv {
            get { return _idTipoDesenv; }
            set { _idTipoDesenv = value; }
        }
        [Display(Name = "Status")]
        public int? idStatus {
            get { return _idStatus; }
            set { _idStatus = value; }
        }
        [Display(Name = "Novo / Alteração nomes: Camadas | Métodos | Páginas Web")]
        [Required(ErrorMessage = "O campo {0} é obrigatório", AllowEmptyStrings = false)]
        [StringLength(5000, MinimumLength = 2, ErrorMessage = "Este campo deve conter ao menos {1} letras e no máximo {2}.")]
        public string camadasMetodos {
            get { return _camadasMetodos; }
            set { _camadasMetodos = value; }
        }
        [Display(Name = "Novo / Alteração nomes: Procedures | Triggers | Tabelas")]
        [Required(ErrorMessage = "O campo {0} é obrigatório", AllowEmptyStrings = false)]
        [StringLength(5000, MinimumLength = 2, ErrorMessage = "Este campo deve conter ao menos {1} letras e no máximo {2}.")]
        public string proceduresNomes {
            get { return _proceduresNomes; }
            set { _proceduresNomes = value; }
        }
        [Display(Name = "Descrição desenvolvedor")]
        [Required(ErrorMessage = "O campo {0} é obrigatório", AllowEmptyStrings = false)]
        [StringLength(5000, MinimumLength = 2, ErrorMessage = "Este campo deve conter ao menos {1} letras e no máximo {2}.")]
        public string descDesenvolvimento {
            get { return _descDesenvolvimento; }
            set { _descDesenvolvimento = value; }
        }
        [Display(Name = "Data Término")]
        [DisplayFormat(ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "O campo é obrigatório")]
        [DataType(DataType.DateTime, ErrorMessage = "Data em formato inválido.")]
        public DateTime dtTermino {
            get { return _dtTermino; }
            set { _dtTermino = value; }
        }
        [Display(Name = "Commit item")]
        [Required(ErrorMessage = "O campo é obrigatório")]
        public bool? flCommit {
            get { return _flCommit; }
            set { _flCommit = value; }
        }
        [Display(Name = "Total de horas utilizadas para o desenvolvimento")]
        [DisplayFormat(ApplyFormatInEditMode = true)]
        [DataType(DataType.Time, ErrorMessage = "Tempo em formato inválido.")]
        public TimeSpan totalHoras {
            get { return _totalHoras; }
            set { _totalHoras = value; }
        }

        [Display(Name = "Item encerrado")]       
        public bool? flItemEncerrado {
            get { return _flItemEncerrado; }
            set { _flItemEncerrado = value; }
        }
        [Display(Name = "Data de publicação do item")]
        [DisplayFormat(ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "O campo é obrigatório")]
        [DataType(DataType.DateTime, ErrorMessage = "Data em formato inválido.")]
        public DateTime dtPublicacao {
            get { return _dtPublicacao; }
            set { _dtPublicacao = value; }
        }
        [Display(Name = "Prioridade")]
        public int idPrioridade {
            get { return _idPrioridade; }
            set { _idPrioridade = value; }
        }

        public string idUsuarioLogado
        {
            get { return _idUsuarioLogado; }
            set { _idUsuarioLogado = value; }
        }

        [Display(Name = "Projeto")]
        public string nomeProjeto
        {
            get { return _nomeProjeto; }
            set { _nomeProjeto = value; }
        }

        [Display(Name = "Data programada")]
        [DisplayFormat(ApplyFormatInEditMode = true)]
        [DataType(DataType.DateTime, ErrorMessage = "Data em formato inválido.")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "O campo {0} é obrigatório.")]
        public DateTime dtProgramada
        {
            get { return _dtProgramada; }
            set{ _dtProgramada = value; }
        }

        [Display(Name = "Título do item")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "O {0} é obrigatório.")]
        [StringLength(50, MinimumLength = 05, ErrorMessage = "O campo deve conter entre 5 e 50 letras.")]
        public string tituloItem
        {
            get { return _tituloItem; }
            set { _tituloItem = value; }
        }
    }
}
