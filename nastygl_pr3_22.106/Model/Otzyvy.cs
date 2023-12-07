namespace nastygl_pr3_22._106.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Otzyvy")]
    public partial class Otzyvy
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID_Otzyvov { get; set; }

        public int ID_Zakaza { get; set; }

        public int ID_Klienta { get; set; }

        [Column(TypeName = "date")]
        public DateTime Data_Otzyva { get; set; }

        [Required]
        [StringLength(50)]
        public string Soderjanie_Otzyva { get; set; }

        public int Ocenka_Otzyva { get; set; }

        public virtual Klienty Klienty { get; set; }

        public virtual Zakazy Zakazy { get; set; }
    }
}
