using BookLibraryManagerApi.Data;
using BookLibraryManagerApi.DataTransferObjects;
using Microsoft.EntityFrameworkCore;

namespace BookLibraryManagerApi.Modules.Rating;

public static class RatingQueryService
{
    public static readonly Func<BookManagerContext, Guid, IEnumerable<RatingDto>>
        GetRatingsByBookId = EF.CompileQuery(
            (BookManagerContext context, Guid bookId) =>
                context.Ratings.AsNoTracking()
                    .Include(r => r.Book)
                    .Where(r => r.BookId == bookId)
                    .Select(r => new RatingDto(
                        r.RatingId,
                        r.RatingScore,
                        r.Book.Title,
                        r.BookId,
                        r.Book.Authors.Select(a => $"{a.FirstName} {a.LastName},").Concat()
                    ))
        );
    
    public static readonly Func<BookManagerContext, string, AverageRatingByBookPublisher?>
        GetAverageRatingByBookPublisher = EF.CompileQuery(
            (BookManagerContext context, string publisherName) =>
                context.Ratings.AsNoTracking()
                    .Include(r => r.Book)
                    .ThenInclude(b => b.Publisher)
                    .Where(r => r.Book.Publisher.PublisherName == publisherName)
                    .GroupBy(r => r.Book.Publisher.PublisherName)
                    .Select(g => new AverageRatingByBookPublisher(
                        g.Key,
                        g.Average(r => r.RatingScore)
                    )).SingleOrDefault()
        );
}