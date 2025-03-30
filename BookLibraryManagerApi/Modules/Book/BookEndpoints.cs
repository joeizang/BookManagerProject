using BookLibraryManagerApi.DataTransferObjects;

namespace BookLibraryManagerApi.Modules.Book;

public static class BookEndpoints
{
    public static RouteGroupBuilder MapBooksEndpoints(this IEndpointRouteBuilder app)
    {
        var bookGroup = app.MapGroup("api/books");
        var bookGroupWithIds = bookGroup.MapGroup("api/books/{id}");

        bookGroup.MapGet("", BookEndpointHandlers.GetAllBooks)
            .WithName("GetAllBooks")
            .Produces<List<BookDto>>()
            .Produces(StatusCodes.Status500InternalServerError);

        bookGroup.MapGet("/{id:guid}", BookEndpointHandlers.GetBookById)
            .WithName("GetBookById")
            .Produces<BookDetails>()
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);

        bookGroup.MapGet("/{title}", BookEndpointHandlers.GetBookByTitle)
            .WithName("GetBookByTitle")
            .Produces<BookDto>()
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);

        bookGroup.MapPost("", BookEndpointHandlers.CreateBook)
            .WithName("CreateBook")
            .Accepts<BookDto>("application/json")
            .Produces<BookDto>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError);

        bookGroupWithIds.MapPut("", BookEndpointHandlers.UpdateBook)
            .WithName("UpdateBook")
            .Accepts<BookDto>("application/json")
            .Produces<BookDto>()
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);

        return bookGroup;
    }
}