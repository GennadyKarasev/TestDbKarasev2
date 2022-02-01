﻿using TestDbKarasev2.Model;

namespace TestDbKarasev2.Data
{
    public class ProductsContext: DbContext
    {
        public ProductsContext(DbContextOptions<ProductsContext> options) : base(options) { Database.EnsureCreated(); }

        public DbSet<Product> Products { get; set; }
    }
}
