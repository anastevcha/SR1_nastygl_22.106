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
    
    public partial class Avtorizacia
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Avtorizacia()
        {
            this.Sotrudniki = new HashSet<Sotrudniki>();
        }
    
        public int ID_Avtorizacii { get; set; }
        public string Login { get; set; }
        public string Parol { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sotrudniki> Sotrudniki { get; set; }
    }
}