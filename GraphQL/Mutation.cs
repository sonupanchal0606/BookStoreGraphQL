using BookStoreGraphQL.Data;
using BookStoreGraphQL.Models;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using HotChocolate.Types.Relay;
using HotChocolate.Resolvers;

namespace BookStoreGraphQL.GraphQL
{
    public class Mutation
    {
        /*        public async Task<Author> AddAuthor(Author input, [ScopedService] BookStoreContext context)
                {
                    context.Authors.Add(input);
                    await context.SaveChangesAsync();
                    return input;
                }*/

        /*        [UseDbContext(typeof(BookStoreContext))]
                public async Task<Author> AddAuthor( [ScopedService] BookStoreContext context, string name, string bio)
                {
                    var author = new Author { Name = name, Bio = bio };
                    context.Authors.Add(author);
                    await context.SaveChangesAsync();
                    return author;
                }*/
        [UseDbContext(typeof(BookStoreContext))]
        public async Task<Author> AddAuthor(AddAuthorInput input, [Service] BookStoreContext context)
        {
            var author = new Author
            {
                Name = input.Name,
                Bio = input.Bio
            };

            context.Authors.Add(author);
            await context.SaveChangesAsync();
            return author;
        }
    

    public record AddAuthorInput(string Name, string Bio);

        /*    public async Task<Book> AddBook(Book input, [ScopedService] BookStoreContext context)
                {
                    context.Books.Add(input);
                    await context.SaveChangesAsync();
                    return input;
                }*/
        /*
                [UseDbContext(typeof(BookStoreContext))]
                public async Task<Book> AddBook( [ScopedService] BookStoreContext context, string title, string genre, Guid authorId)
                {
                    var book = new Book { Title = title, Genre = genre, AuthorId = authorId };
                    context.Books.Add(book);
                    await context.SaveChangesAsync();
                    return book;
                }*/

        [UseDbContext(typeof(BookStoreContext))]
        public async Task<Book> AddBook(AddBookInput input, [Service] BookStoreContext context)
        {
            var author = await context.Authors.FindAsync(input.AuthorId);
            if (author == null)
            {
                throw new Exception("Invalid AuthorId provided.");
            }

            var book = new Book
            {
                Title = input.Title,
                Genre = input.Genre,
                AuthorId = input.AuthorId
            };

            context.Books.Add(book);
            await context.SaveChangesAsync();
            return book;
        }
        public record AddBookInput(string Title, string Genre, Guid AuthorId);

        public async Task<bool> DeleteBook(int id, [Service] BookStoreContext context)
        {
            var book = await context.Books.FindAsync(id);
            if (book == null) return false;

            context.Books.Remove(book);
            await context.SaveChangesAsync();
            return true;
        }
    }
  
}
