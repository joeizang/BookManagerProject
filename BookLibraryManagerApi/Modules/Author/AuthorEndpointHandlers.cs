using BookLibraryManagerApi.Data;
using BookLibraryManagerApi.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;

namespace BookLibraryManagerApi.Modules.Author;

public static class AuthorEndpointHandlers
{
    public static IResult GetAllAuthors(
        [FromServices] BookManagerContext context)
    {
        var authors = AuthorQueryService.GetAllAuthors(context).ToList();
        return Results.Ok(authors);
    }

    public static IResult GetAuthorById(
        [FromServices] BookManagerContext context,
        Guid id)
    {
        var author = AuthorQueryService.GetAuthorById(context, id);
        return author is not null ? Results.Ok(author) : Results.NotFound();
    }

    public static IResult GetAuthorByName(
        [FromServices] BookManagerContext context,
        string name)
    {
        var author = AuthorQueryService.GetAuthorByName(context, name);
        return author is not null ? Results.Ok(author) : Results.NotFound();
    }

    public static async Task<IResult> CreateAuthor(
        [FromServices] BookManagerContext context,
        [FromBody] AuthorCreateDto authorDto,
        HttpContext httpContext,
        CancellationToken cancellationToken)
    {
        var author = await AuthorCommandService.CreateAuthor(context, authorDto, cancellationToken);
        return Results.Created($"{httpContext.Request.PathBase}/api/author/{author.AuthorId}", author);
    }

    public static async Task<IResult> UpdateAuthor(
        BookManagerContext context,
        AuthorUpdateDto authorDto,
        CancellationToken cancellationToken)
    {
        var author = await AuthorCommandService.UpdateAuthor(context, authorDto, cancellationToken);
        return author.Match(_ => Results.NoContent(),
            () => Results.NotFound($"Author with id {authorDto.AuthorId} not found."));
    }
    
    public static async Task<IResult> DeleteAuthor(
        BookManagerContext context,
        Guid id,
        CancellationToken cancellationToken)
    {
        var author = await AuthorCommandService.DeleteAuthor(context, id, cancellationToken);
        return author.Match(_ => Results.NoContent(),
            () => Results.NotFound($"Author with id {id} not found."));
    }
}