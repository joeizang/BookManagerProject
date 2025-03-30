using BookLibraryManagerApi.DataTransferObjects;

namespace BookLibraryManagerApi.Modules.Publisher;

public static class PublisherEndpoints
{
    public static RouteGroupBuilder MapPublisherEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var publisherGroup = endpoints.MapGroup("api/publishers");
        var publisherGroupWithIds = publisherGroup.MapGroup("/{id:guid}");
        
        publisherGroup.MapGet("", PublisherEndpointHandlers.GetAllPublishers)
            .WithName("GetAllPublishers")
            .Produces<List<PublisherDto>>()
            .Produces(StatusCodes.Status500InternalServerError);
        
        publisherGroupWithIds.MapGet("", PublisherEndpointHandlers.GetPublisherById)
            .WithName("GetPublisherById")
            .Produces<PublisherDto>()
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);
        
        publisherGroup.MapGet("/{name}", PublisherEndpointHandlers.GetPublisherByName)
            .WithName("GetPublisherByName")
            .Produces<PublisherDto>()
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);
        
        publisherGroup.MapPost("", PublisherEndpointHandlers.CreatePublisher)
            .WithName("CreatePublisher")
            .Accepts<PublisherCreateDto>("application/json")
            .Produces<PublisherCreatedDto>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError);
        
        publisherGroupWithIds.MapPut("", PublisherEndpointHandlers.UpdatePublisher)
            .WithName("UpdatePublisher")
            .Accepts<PublisherCreateDto>("application/json")
            .Produces<PublisherDto>()
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);
        
        publisherGroupWithIds.MapPut("/authors", PublisherEndpointHandlers.UpdatePublisherAuthorRoster)
            .WithName("UpdatePublisherAuthorRoster")
            .Accepts<PublisherAuthorRosterUpdate>("application/json")
            .Produces<PublisherCreatedDto>()
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);
        
        publisherGroupWithIds.MapDelete("", PublisherEndpointHandlers.DeletePublisher)
            .WithName("DeletePublisher")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);
        
        

        return publisherGroup;
    }
}