//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace appGestion_conge.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class conge
    {
        public Nullable<int> noemployer { get; set; }
        public int noconge { get; set; }
        public string duree { get; set; }
        public Nullable<System.DateTime> date_debut { get; set; }
        public Nullable<System.DateTime> date_fin { get; set; }
        public string motif { get; set; }
        public string etat { get; set; }
    
        public virtual employer employer { get; set; }
    }
}
