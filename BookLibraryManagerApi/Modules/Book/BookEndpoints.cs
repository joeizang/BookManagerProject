using BookLibraryManagerApi.DataTransferObjects;

namespace BookLibraryManagerApi.Modules.Book;

public static class BookEndpoints
{
    public static RouteGroupBuilder MapBooksEndpoints(this IEndpointRouteBuilder app)
    {
        var bookGroup = app.MapGroup("api/books");
        var bookGroupWithIds = bookGroup.MapGroup("/{id:guid}");

        bookGroup.MapGet("", BookEndpointHandlers.GetAllBooks)
            .WithName("GetAllBooks")
            .Produces<List<BookDto>>()
            .Produces(StatusCodes.Status500InternalServerError);

        bookGroupWithIds.MapGet("", BookEndpointHandlers.GetBookById)
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
            .Accepts<BookCreateDto>("application/json")
            .Produces<BookDto>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError);

        bookGroupWithIds.MapPut("", BookEndpointHandlers.UpdateBook)
            .WithName("UpdateBook")
            .Accepts<BookUpdateDto>("application/json")
            .Produces<BookDto>()
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);
        
        bookGroupWithIds.MapDelete("", BookEndpointHandlers.DeleteBook)
            .WithName("DeleteBook")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);

        return bookGroup;
    }
}