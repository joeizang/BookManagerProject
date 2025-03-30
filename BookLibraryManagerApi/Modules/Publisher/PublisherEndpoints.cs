using BookLibraryManagerApi.DataTransferObjects;

namespace BookLibraryManagerApi.Modules.Publisher;

public static class PublisherEndpoints
{
    public static RouteGroupBuilder MapPublisherEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var publisherGroup = endpoints.MapGroup("api/publishers");
        var publisherGroupWithIds = publisherGroup.MapGroup("api/publishers/{id}"); //endpoints.MapGroup("api/publisher/{id}");
        
        publisherGroup.MapGet("", PublisherEndpointHandlers.GetAllPublishers)
            .WithName("GetAllPublishers")
            .Produces<List<PublisherDto>>()
            .Produces(StatusCodes.Status500InternalServerError);
        
        publisherGroup.MapGet("/{id}", PublisherEndpointHandlers.GetPublisherById)
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
            .Accepts<PublisherDto>("application/json")
            .Produces<PublisherDto>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError);
        
        publisherGroupWithIds.MapPut("", PublisherEndpointHandlers.UpdatePublisher)
            .WithName("UpdatePublisher")
            .Accepts<PublisherDto>("application/json")
            .Produces<PublisherDto>()
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status500InternalServerError);
        
        // publisherGroupWithIds.MapDelete("", PublisherEndpointHandlers.DeletePublisher)
        //     .WithName("DeletePublisher")
        //     .Produces(StatusCodes.Status204NoContent)
        //     .Produces(StatusCodes.Status404NotFound)
        //     .Produces(StatusCodes.Status500InternalServerError);
        
        

        return publisherGroup;
    }
}