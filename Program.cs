using BookStoreGraphQL.Data;
using BookStoreGraphQL.GraphQL;
using Microsoft.EntityFrameworkCore;


using BookStoreGraphQL.Data;
using BookStoreGraphQL.GraphQL;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 1. Configure DbContext
/*builder.Services.AddDbContext<BookStoreContext>(options =>
options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));*/

// Register DbContextFactory for scoped use in GraphQL
builder.Services.AddPooledDbContextFactory<BookStoreContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services
builder.Services.AddControllers(); // to show controller of weatherforcast

// 2. Register GraphQL services
builder.Services
    .AddAuthorization() // Add ASP.NET Core Authorization
    .AddGraphQLServer()
    .AddAuthorization() // Hot Chocolate authorization middleware
    .AddQueryType<Query>() // Register the Query class
    .AddMutationType<Mutation>() // Register the Mutation class
    .AddType<BookFilterType>() // for custom filter of book
    .AddType<BookSortType>() // for custom sort of book
    .AddProjections() // Allow projections
    .AddFiltering() // Add filtering support
    .AddSorting(); // Add sorting support

var app = builder.Build();

// 3. Middleware pipeline
app.UseRouting(); // Middleware for routing

// Uncomment the lines below if you're using Authentication/Authorization
// app.UseAuthentication();
// app.UseAuthorization();

// 4. Map the GraphQL endpoint
app.MapGraphQL(); // Default endpoint: /graphql

// Map controllers
app.MapControllers(); // for showing weather forecasr controller


// 5. Run the application
app.Run();
