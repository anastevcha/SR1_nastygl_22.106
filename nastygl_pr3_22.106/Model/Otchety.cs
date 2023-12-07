namespace nastygl_pr3_22._106.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Otchety")]
    public partial class Otchety
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID_Otcheta { get; set; }

        [Column(TypeName = "date")]
        public DateTime Data_Otcheta { get; set; }

        [Required]
        [StringLength(50)]
        public string Opisanie_Otcheta { get; set; }

        [Required]
        [StringLength(50)]
        public string Soderjanie_Otcheta { get; set; }

        public int ID_Sotrudnika { get; set; }

        public virtual Sotrudniki Sotrudniki { get; set; }
    }
}
