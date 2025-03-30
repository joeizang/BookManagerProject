using BookLibraryManagerApi.DataTransferObjects;

namespace BookLibraryManagerApi.Modules.Author;

public static class AuthorEndpoints
{
    public static RouteGroupBuilder MapAuthorEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var authorGroup = endpoints.MapGroup("api/authors");
        var authorGroupWithIds = authorGroup.MapGroup("api/authors/{id}"); //endpoints.MapGroup("api/publisher/{id}");
        
        authorGroup.MapGet("", AuthorEndpointHandlers.GetAllAuthors)
            .WithName("GetAllAuthors")
            .Produces<List<AuthorDto>>()
            .Produces(StatusCodes.Status500InternalServerError);
        
        authorGroup.MapGet("/{id}", AuthorEndpointHandlers.GetAuthorById)
            .WithName("GetAuthorById")
            .Produces<AuthorDetailDto>()
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);
        
        authorGroup.MapGet("/{name}", AuthorEndpointHandlers.GetAuthorByName)
            .WithName("GetAuthorByName")
            .Produces<AuthorDetailDto>()
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);
        
        authorGroup.MapPost("", AuthorEndpointHandlers.CreateAuthor)
            .WithName("CreateAuthor")
            .Accepts<AuthorDto>("application/json")
            .Produces<AuthorDto>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError);
        
        authorGroupWithIds.MapPut("", AuthorEndpointHandlers.UpdateAuthor)
            .WithName("UpdateAuthor")
            .Accepts<AuthorDto>("application/json")
            .Produces<AuthorDto>()
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);
        
        // publisherGroupWithIds.MapDelete("", PublisherEndpointHandlers.DeletePublisher)
        //     .WithName("DeletePublisher")
        //     .Produces(StatusCodes.Status204NoContent)
        //     .Produces(StatusCodes.Status404NotFound)
        //     .Produces(StatusCodes.Status500InternalServerError);
        
        
        return authorGroup;
    }
}