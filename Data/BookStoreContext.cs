using BookStoreGraphQL.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStoreGraphQL.Data
{
    public class BookStoreContext : DbContext
    {
        public BookStoreContext(DbContextOptions<BookStoreContext> options) : base(options) { }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
    }
}
