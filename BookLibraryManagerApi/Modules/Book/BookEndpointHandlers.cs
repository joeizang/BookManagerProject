using BookLibraryManagerApi.Data;
using BookLibraryManagerApi.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;

namespace BookLibraryManagerApi.Modules.Book;

public static class BookEndpointHandlers
{
    public static IResult GetAllBooks([FromServices] BookManagerContext context)
    {
        var books = BookQueryService.GetAllBooks(context).ToList();
        return Results.Ok(books);
    }

    public static async Task<IResult> GetBookById([FromServices] BookManagerContext context,
        Guid id)
    {
        var book = await BookQueryService.GetBookById(context, id).ConfigureAwait(false);
        return book is not null ? Results.Ok(book) : Results.NotFound();
    }

    public static async Task<IResult> GetBookByTitle([FromServices] BookManagerContext context, string title)
    {
        var book = await BookQueryService.GetBookByTitle(context, title).ConfigureAwait(false);
        return book is not null ? Results.Ok(book) : Results.NotFound();
    }
    
    public static async Task<IResult> CreateBook([FromServices] BookManagerContext context,
        [FromBody] BookCreateDto dto, HttpContext httpContext, CancellationToken token)
    {
        try
        {
            var result = await BookCommandService
                .CreateBook(context, dto, token).ConfigureAwait(false);
            return Results.Created($"{httpContext.Request.PathBase}/api/book/{result.BookId}", result);
        }
        catch (Exception e)
        {
            return Results.InternalServerError("An Error occurred on our end. You don't have to do nothing. Just try in a few minutes.");
        }
    }
    
    public static async Task<IResult> UpdateBook([FromServices] BookManagerContext context,
        [FromBody] BookUpdateDto dto, CancellationToken token)
    {
        var result = await BookCommandService
            .UpdateBook(context, dto, token).ConfigureAwait(false);
        
        return result.Match(
            Some: book => Results.NoContent(),
            None: () => Results.NotFound($"Book with id {dto.BookId} not found.")
        );
    }
    
    public static async Task<IResult> DeleteBook([FromServices] BookManagerContext context,
        Guid id, CancellationToken token)
    {
        var result = await BookCommandService
            .DeleteBook(context, id, token).ConfigureAwait(false);
        
        return result.Match(
            Some: book => Results.Ok(book),
            None: () => Results.NotFound($"Book with id {id} not found.")
        );
    }
}