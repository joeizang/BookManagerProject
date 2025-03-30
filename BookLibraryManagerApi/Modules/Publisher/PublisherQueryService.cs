using BookLibraryManagerApi.Data;
using BookLibraryManagerApi.DataTransferObjects;
using Microsoft.EntityFrameworkCore;

namespace BookLibraryManagerApi.Modules.Publisher;

public static class PublisherQueryService
{
    public static readonly Func<BookManagerContext, IEnumerable<PublisherDto>>
        GetAllPublishers = EF.CompileQuery(
            (BookManagerContext context) =>
                context.Publishers.AsNoTracking()
                    .Include(p => p.BooksPublished)
                    .Include(p => p.BookAuthors)
                    .Select(p => new PublisherDto(
                        p.PublisherName,
                        p.PublisherId,
                        p.BooksPublished.Count(),
                        p.BookAuthors.Count()
                    ))
        );
    
    public static readonly Func<BookManagerContext, Guid, PublisherDto?>
        GetPublisherById = EF.CompileQuery(
            (BookManagerContext context, Guid id) =>
                context.Publishers.AsNoTracking()
                    .Include(p => p.BooksPublished)
                    .Include(p => p.BookAuthors)
                    .Where(p => p.PublisherId == id)
                    .Select(p => new PublisherDto(
                        p.PublisherName,
                        p.PublisherId,
                        p.BooksPublished.Count(),
                        p.BookAuthors.Count()
                    )).SingleOrDefault()
        );
    
    public static readonly Func<BookManagerContext, string, PublisherDto?>
        GetPublisherByName = EF.CompileQuery(
            (BookManagerContext context, string name) =>
                context.Publishers.AsNoTracking()
                    .Include(p => p.BooksPublished)
                    .Include(p => p.BookAuthors)
                    .Where(p => p.PublisherName == name)
                    .Select(p => new PublisherDto(
                        p.PublisherName,
                        p.PublisherId,
                        p.BooksPublished.Count(),
                        p.BookAuthors.Count()
                    )).SingleOrDefault()
        );
    
}