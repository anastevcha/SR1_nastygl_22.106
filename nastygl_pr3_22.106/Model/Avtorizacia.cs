namespace nastygl_pr3_22._106.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Avtorizacia")]
    public partial class Avtorizacia
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Avtorizacia()
        {
            Sotrudniki = new HashSet<Sotrudniki>();
        }

        [Key]
        public int ID_Avtorizacii { get; set; }

        [Required]
        public string Login { get; set; }

        [Required]
        public string Parol { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sotrudniki> Sotrudniki { get; set; }
    }
}
