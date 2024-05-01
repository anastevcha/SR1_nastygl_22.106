namespace nastygl_pr3_22._106.Model2
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Sotrudniki")]
    public partial class Sotrudniki
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Sotrudniki()
        {
            Otchety = new HashSet<Otchety>();
            Zakazy = new HashSet<Zakazy>();
        }

        [Key]
        public int ID_Sotrudnika { get; set; }

        [Required]
        [StringLength(50)]
        public string Familiya { get; set; }

        [Required]
        [StringLength(50)]
        public string Imya { get; set; }

        [StringLength(50)]
        public string Otchestvo { get; set; }

        public int Vozrast { get; set; }

        public int Stag { get; set; }

        public int Doljnost { get; set; }

        [Column(TypeName = "money")]
        public decimal Zarplata { get; set; }

        public int ID_Avtorizacii { get; set; }

        [Required(ErrorMessage = "Почта обязательна")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$",
             ErrorMessage = "формат ввода почты example@mail.com")]
        [StringLength(50)]
        public string Email { get; set; }

        public virtual Avtorizacia Avtorizacia { get; set; }

        public virtual Doljnost Doljnost1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Otchety> Otchety { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Zakazy> Zakazy { get; set; }
    }
}
