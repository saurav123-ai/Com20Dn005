namespace Truyumcasestudy.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("menu")]
    public partial class menu
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int menu_id { get; set; }

        [StringLength(20)]
        public string menu_name { get; set; }

        public double? price { get; set; }

        [Column(TypeName = "date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:yyyy-mm-dd}",ApplyFormatInEditMode =true)]
        public DateTime? date_of_lunch { get; set; }

        [StringLength(3)]
        public string active { get; set; }

        [StringLength(10)]
        public string category { get; set; }
    }
}
