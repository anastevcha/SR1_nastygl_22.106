namespace nastygl_pr3_22._106.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Materialy")]
    public partial class Materialy
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Materialy()
        {
            Zakazy = new HashSet<Zakazy>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID_Materiala { get; set; }

        [Required]
        [StringLength(50)]
        public string Nazvanie_Materiala { get; set; }

        public int Kolvo_Na_Sklade { get; set; }

        [Column(TypeName = "money")]
        public decimal Stoimost_Za_Metr { get; set; }

        public int Cvet_Materiala { get; set; }

        public int Tip_Materiala { get; set; }

        [Required]
        [StringLength(50)]
        public string Prochnost_Materiala { get; set; }

        public int Postavshik { get; set; }

        public virtual Cvet_Materiala Cvet_Materiala1 { get; set; }

        public virtual Postavshiki Postavshiki { get; set; }

        public virtual Tip_Materiala Tip_Materiala1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Zakazy> Zakazy { get; set; }
    }
}
