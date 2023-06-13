using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrixNetCoreApp.Models
{
    // [Table("tbl_category")]
    public class Category
    {

        // [Key]
        // [Column("cat_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryId { get; set; }

        // [Column(TypeName = "ntext")]
        [Required(ErrorMessage = "ประเภทสินค้า ห้ามว่าง")]
        public string CategoryName { get; set; } = null!;

        public bool IsActive { get; set; } = true;
    }
}
