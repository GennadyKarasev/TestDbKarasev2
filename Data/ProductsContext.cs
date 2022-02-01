using System.ComponentModel.DataAnnotations.Schema;
using TestDbKarasev2.Model;

namespace TestDbKarasev2.Data
{
    [Table("Product")]
    public class ProductsContext: DbContext
    {
        public ProductsContext(DbContextOptions<ProductsContext> options) : base(options) { Database.EnsureCreated(); }

        public DbSet<Product> Products { get; set; }
    }
}
