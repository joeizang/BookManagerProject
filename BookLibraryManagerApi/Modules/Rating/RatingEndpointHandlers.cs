using BookLibraryManagerApi.Data;
using BookLibraryManagerApi.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;

namespace BookLibraryManagerApi.Modules.Rating;

public static class RatingEndpointHandlers
{
    public static async Task<IResult> GetRatingsByBookId(
        Guid bookId,
        [FromServices] BookManagerContext context,
        CancellationToken token = default)
    {
        var ratings = RatingQueryService.GetRatingsByBookId(context, bookId).ToList();
        return Results.Ok(ratings);
    }

    public static async Task<IResult> GetAverageRatingByBookPublisher(
        string publisherName,
        [FromServices] BookManagerContext context,
        CancellationToken token = default)
    {
        var averageRating = RatingQueryService.GetAverageRatingByBookPublisher(context, publisherName);
        return averageRating != null ? Results.Ok(averageRating) : Results.NotFound();
    }
    
    public static async Task<IResult> AddRating(
        [FromBody] RatingCreateDto dto,
        [FromServices] BookManagerContext context,
        HttpContext httpContext,
        CancellationToken token = default)
    {
        var rating = await RatingCommandService.AddRating(dto, context, token);
        return Results.Created($"{httpContext.Request.PathBase}/ratings/{rating.RatingId}", rating);
    }
    
    public static async Task<IResult> UpdateRating(
        [FromBody] RatingUpdateDto dto,
        [FromServices] BookManagerContext context,
        CancellationToken token = default)
    {
        var rating = await RatingCommandService.UpdateRating(dto, context, token);
        return rating.Match(
            Some: _ => Results.NoContent(),
            None: () => Results.NotFound()
        );
    }
}