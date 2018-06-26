using System.ComponentModel.DataAnnotations.Schema;

namespace Message.Publisher.DB
{
    [Table("Product")]
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
