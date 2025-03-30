using BookLibraryManagerApi.DataTransferObjects;

namespace BookLibraryManagerApi.Modules.Rating;

public static class RatingEndpoints
{
    public static RouteGroupBuilder MapRatingEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/ratings").WithTags("Ratings");

        group.MapGet("/{bookId:guid}", RatingEndpointHandlers.GetRatingsByBookId)
            .Produces<RatingDto[]>()
            .Produces(StatusCodes.Status404NotFound);

        group.MapGet("/average-ratings/{publisherName}", RatingEndpointHandlers.GetAverageRatingByBookPublisher)
            .Produces<AverageRatingByBookPublisher>()
            .Produces(StatusCodes.Status404NotFound);

        group.MapPost("/", RatingEndpointHandlers.AddRating)
            .Produces<RatingDto>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest);

        group.MapPut("/{id:guid}", RatingEndpointHandlers.UpdateRating)
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound);

        return group;
    }
}