namespace nastygl_pr3_22._106.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Zakazy")]
    public partial class Zakazy
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Zakazy()
        {
            Otzyvy = new HashSet<Otzyvy>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID_Zakaza { get; set; }

        public int ID_klienta { get; set; }

        public int ID_Izdeliya { get; set; }

        public int ID_materiala { get; set; }

        public int ID_sotrudnika { get; set; }

        [Required]
        [StringLength(50)]
        public string Opisanie { get; set; }

        [Column(TypeName = "date")]
        public DateTime Data_Zakaza { get; set; }

        [Column(TypeName = "money")]
        public decimal Summa_Zakaza { get; set; }

        public int Status_Zakaza { get; set; }

        public int Skidki { get; set; }

        public int ID_Modeli { get; set; }

        public int ID_Razmera { get; set; }

        public virtual Izdeliya Izdeliya { get; set; }

        public virtual Klienty Klienty { get; set; }

        public virtual Materialy Materialy { get; set; }

        public virtual Modeli Modeli { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Otzyvy> Otzyvy { get; set; }

        public virtual Razmer_Odejdy Razmer_Odejdy { get; set; }

        public virtual Skidki Skidki1 { get; set; }

        public virtual Sotrudniki Sotrudniki { get; set; }

        public virtual Status_Zakaza Status_Zakaza1 { get; set; }
    }
}
