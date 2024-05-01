namespace nastygl_pr3_22._106.Model2
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Izdeliya")]
    public partial class Izdeliya
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Izdeliya()
        {
            Zakazy = new HashSet<Zakazy>();
        }

        [Key]
        public int ID_Izdeliya { get; set; }

        [Required]
        [StringLength(50)]
        public string Nazvanie_Izdeliya { get; set; }

        [Required]
        [StringLength(50)]
        public string Opisanie { get; set; }

        [Column(TypeName = "money")]
        public decimal Stoimost_Izdelia { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Zakazy> Zakazy { get; set; }
    }
}
