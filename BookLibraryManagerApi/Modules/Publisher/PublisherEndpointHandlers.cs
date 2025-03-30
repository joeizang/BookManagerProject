using BookLibraryManagerApi.Data;
using BookLibraryManagerApi.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;

namespace BookLibraryManagerApi.Modules.Publisher;

public static class PublisherEndpointHandlers
{
    public static IResult GetAllPublishers([FromServices] BookManagerContext context)
    {
        var publishers = PublisherQueryService.GetAllPublishers(context);
        return Results.Ok(publishers);
    }

    public static IResult GetPublisherById([FromServices] BookManagerContext context, Guid publisherId)
    {
        var publisher = PublisherQueryService.GetPublisherById(context, publisherId);
        if (publisher is null)
            return Results.NotFound($"Publisher with id {publisherId} not found.");
        return Results.Ok(publisher);
    }

    public static IResult GetPublisherByName([FromServices] BookManagerContext context, string publisherName)
    {
        var publisher = PublisherQueryService.GetPublisherByName(context, publisherName);
        if (publisher is null)
            return Results.NotFound($"Publisher with name {publisherName} not found.");
        return Results.Ok(publisher);
    }

    public static async Task<IResult> CreatePublisher([FromServices] BookManagerContext context,
        [FromBody] PublisherCreateDto dto, CancellationToken token)
    {
        try
        {
            var result = await PublisherCommandService
                .CreatePublisher(context, dto, token).ConfigureAwait(false);
            return Results.Created($"/api/publisher/{result.PublisherId}", result);
        }
        catch (Exception e)
        {
            return Results.InternalServerError("An Error occurred on our end. You don't have to do nothing. Just try in a few minutes.");
        }
    }

    public static async Task<IResult> UpdatePublisher([FromServices] BookManagerContext context,
        [FromBody] PublisherUpdateDto dto, CancellationToken token)
    {
        var result = await PublisherCommandService
            .UpdatePublisher(context, dto, token).ConfigureAwait(false);
        
        return result.Match(
            Some: publisher => Results.NoContent(),
            None: () => Results.NotFound($"Publisher with id {dto.PublisherId} not found.")
        );
    }

    public static Task DeletePublisher(HttpContext context)
    {
        throw new NotImplementedException();
    }
}