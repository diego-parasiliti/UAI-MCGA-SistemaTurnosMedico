//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MCGA.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class Atencion
    {
        public int Id { get; set; }
        public int turno_id { get; set; }
        public System.DateTime hora_llegada { get; set; }
        public Nullable<System.DateTime> hora_atencion { get; set; }
        public string sintomas { get; set; }
        public string diagnostico { get; set; }
        public int bono_id { get; set; }
        public System.DateTime createdon { get; set; }
        public string createdby { get; set; }
        public System.DateTime changedon { get; set; }
        public string changedby { get; set; }
    
        public virtual Turno Turno { get; set; }
    }
}
