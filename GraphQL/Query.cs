using BookStoreGraphQL.Data;
using BookStoreGraphQL.Models;
using HotChocolate.Data.Filters;
using HotChocolate.Data.Sorting;
using HotChocolate.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using HotChocolate;

namespace BookStoreGraphQL.GraphQL
{
    public class Query
    {
        [UseDbContext(typeof(BookStoreContext))]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Author> GetAuthors([Service] BookStoreContext context)
        {
            return context.Authors;
        }

        [UseDbContext(typeof(BookStoreContext))]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Book> GetBooks([Service] BookStoreContext context)
        {
            return context.Books;
        }
    }

    // Customize Filters and Sorting (Optional)
    // You can create custom filters and sorting rules by defining a GraphQL input type for them.
    public class BookFilterType : FilterInputType<Book>
    {
        protected override void Configure(IFilterInputTypeDescriptor<Book> descriptor)
        {
            descriptor.Field(b => b.Title).Ignore(); // Exclude title from filters
            descriptor.Field(b => b.Genre).Name("customGenre"); // Rename filter field
        }
    }

    public class BookSortType : SortInputType<Book>
    {
        protected override void Configure(ISortInputTypeDescriptor<Book> descriptor)
        {
            descriptor.Field(b => b.Title).Name("customTitleSort"); // Rename sort field
        }
    }

}
