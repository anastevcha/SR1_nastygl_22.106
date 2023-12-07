namespace nastygl_pr3_22._106.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Razmer_Odejdy
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Razmer_Odejdy()
        {
            Zakazy = new HashSet<Zakazy>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID_Razmera { get; set; }

        [Required]
        [StringLength(50)]
        public string Tip_Odejdi { get; set; }

        [Required]
        [StringLength(50)]
        public string Razmer { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Zakazy> Zakazy { get; set; }
    }
}
