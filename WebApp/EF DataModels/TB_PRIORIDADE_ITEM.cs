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
    
    public partial class TB_PRIORIDADE_ITEM
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TB_PRIORIDADE_ITEM()
        {
            this.TB_PROJETO_ITENS = new HashSet<TB_PROJETO_ITENS>();
        }
    
        public int ID { get; set; }
        public string PRIORIDADE_TIPO { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TB_PROJETO_ITENS> TB_PROJETO_ITENS { get; set; }
    }
}