//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApp.EF_DataModels
{
    using System;
    using System.Collections.Generic;
    
    public partial class TB_PROJETOS_SISTEMAS
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TB_PROJETOS_SISTEMAS()
        {
            this.TB_PROJETO_DESENVOLVIMENTO = new HashSet<TB_PROJETO_DESENVOLVIMENTO>();
        }
    
        public int ID_PROJETO { get; set; }
        public string NOME_PROJETO { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TB_PROJETO_DESENVOLVIMENTO> TB_PROJETO_DESENVOLVIMENTO { get; set; }
    }
}
