//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BibliotekaWPF
{
    using System;
    using System.Collections.Generic;
    
    public partial class Wypozyczenia
    {
        public int IDWypozyczenia { get; set; }
        public int IDCzytelnika { get; set; }
        public int IDKsiazki { get; set; }
        public System.DateTime DataWypozyczenia { get; set; }
        public Nullable<System.DateTime> DataOddania { get; set; }
        public string StatusWypozyczenia { get; set; }
    
        public virtual Czytelnicy Czytelnicy { get; set; }
        public virtual Ksiazki Ksiazki { get; set; }
    }
}
