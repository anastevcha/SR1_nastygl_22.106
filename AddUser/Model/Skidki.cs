//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AddUser.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Skidki
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Skidki()
        {
            this.Zakazy = new HashSet<Zakazy>();
        }
    
        public int ID_Skidki { get; set; }
        public string Nazvanie_Skidki { get; set; }
        public int Procent_Skidki { get; set; }
        public System.DateTime Data_Nachala { get; set; }
        public System.DateTime Data_Konca { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Zakazy> Zakazy { get; set; }
    }
}
