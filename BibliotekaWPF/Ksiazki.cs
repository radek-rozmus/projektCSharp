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

    public partial class Ksiazki
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Ksiazki()
        {
            this.Wypozyczenia = new HashSet<Wypozyczenia>();
        }
    
        public int IDKsiazki { get; set; }
        public string Tytul { get; set; }
        public string Kategoria { get; set; }
        public int Autor { get; set; }
        public Nullable<System.DateTime> DataWydania { get; set; }
        public bool Wypozyczona { get; set; }
    
        public virtual Autorzy Autorzy { get; set; }
        public virtual Kategorie Kategorie { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Wypozyczenia> Wypozyczenia { get; set; }
    }
}
