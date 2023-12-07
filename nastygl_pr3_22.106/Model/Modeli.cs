namespace nastygl_pr3_22._106.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Modeli")]
    public partial class Modeli
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Modeli()
        {
            Sklad = new HashSet<Sklad>();
            Zakazy = new HashSet<Zakazy>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID_Modeli { get; set; }

        [Required]
        [StringLength(50)]
        public string Nazvanie_Modeni { get; set; }

        [Required]
        [StringLength(50)]
        public string Opisanie { get; set; }

        [Column(TypeName = "money")]
        public decimal Cena { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sklad> Sklad { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Zakazy> Zakazy { get; set; }
    }
}
