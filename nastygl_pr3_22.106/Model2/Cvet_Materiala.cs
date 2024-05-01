namespace nastygl_pr3_22._106.Model2
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Cvet_Materiala
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Cvet_Materiala()
        {
            Materialy = new HashSet<Materialy>();
        }

        [Key]
        public int ID_Cvet_Materiala { get; set; }

        [Column("Cvet_Materiala")]
        [Required]
        [StringLength(50)]
        public string Cvet_Materiala1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Materialy> Materialy { get; set; }
    }
}
