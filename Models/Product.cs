using System.ComponentModel.DataAnnotations.Schema;

namespace OrixNetCoreApp.Models
{
    public class Product
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }

        public string ProduuctName{ get; set; } = null!;

        [Column(TypeName = "decimal(10, 2)")]
   
        public decimal ProductPrice { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime ProductExpire { get; set; } = DateTime.Now.AddMonths(3);

        //FK
        //[Foeignkey("cat_id")]
        public int CategoryId { get; set; }

        //Relation 
        //Many to One
        public Category? Category { get; set; }
    }
}
