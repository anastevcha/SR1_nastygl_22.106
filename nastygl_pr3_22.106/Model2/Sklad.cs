namespace nastygl_pr3_22._106.Model2
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Sklad")]
    public partial class Sklad
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID_Sklada { get; set; }

        public int ID_Modeli { get; set; }

        public int Kolvo_Na_Sklade { get; set; }

        [Column(TypeName = "date")]
        public DateTime Data_Postupleniya { get; set; }

        public virtual Modeli Modeli { get; set; }
    }
}
