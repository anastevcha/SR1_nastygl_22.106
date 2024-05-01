namespace nastygl_pr3_22._106.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Doljnost")]
    public partial class Doljnost
    {
        [Key]
        public int ID_Doljnosti { get; set; }

        [Required]
        [StringLength(50)]
        public string Nazvanie { get; set; }
    }
}
