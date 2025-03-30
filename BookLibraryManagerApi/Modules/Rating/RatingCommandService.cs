using BookLibraryManagerApi.Data;
using BookLibraryManagerApi.DataTransferObjects;
using LanguageExt;
using Microsoft.EntityFrameworkCore;

namespace BookLibraryManagerApi.Modules.Rating;

public static class RatingCommandService
{
    public static async Task<RatingDto> AddRating(RatingCreateDto dto, BookManagerContext context,
        CancellationToken token = default)
    {
        var rating = new DomainModels.Rating(dto.ReviewText, dto.RatingScore, dto.BookId);
        
        context.Add(rating);
        await context.SaveChangesAsync(token);
        return new RatingDto(
            rating.RatingId,
            rating.RatingScore,
            rating.Book.Title,
            rating.BookId,
            rating.Book.Authors.Select(a => $"{a.FirstName} {a.LastName},").Concat()
        );
    }
    
    public static async Task<Option<RatingDto>> UpdateRating(RatingUpdateDto dto, BookManagerContext context,
        CancellationToken token = default)
    {
        var rating = await context.Ratings.FindAsync([dto.RatingId], token);
        if (rating == null)
            return Option<RatingDto>.None;

        rating.UpdateRating(dto.ReviewText, dto.RatingScore);
        context.Entry(rating).State = EntityState.Modified;
        await context.SaveChangesAsync(token);

        return Option<RatingDto>.Some(new RatingDto(
            rating.RatingId,
            rating.RatingScore,
            rating.Book.Title,
            rating.BookId,
            rating.Book.Authors.Select(a => $"{a.FirstName} {a.LastName},").Concat()
        ));
    }
}