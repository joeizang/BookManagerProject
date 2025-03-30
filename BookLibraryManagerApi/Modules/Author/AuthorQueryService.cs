using BookLibraryManagerApi.Data;
using BookLibraryManagerApi.DataTransferObjects;
using Microsoft.EntityFrameworkCore;

namespace BookLibraryManagerApi.Modules.Author;

public static class AuthorQueryService
{
    public static readonly Func<BookManagerContext, IEnumerable<AuthorDto>>
        GetAllAuthors = EF.CompileQuery(
            (BookManagerContext context) =>
                context.Authors.AsNoTracking()
                    .Include(a => a.Books)
                    .Include(a => a.Publishers)
                    .Select(a => new AuthorDto(
                        $"{a.FirstName} {a.LastName}",
                        a.AuthorId,
                        a.Books.Count(),
                        a.Publishers.Count()
                    ))
        );
    
    public static readonly Func<BookManagerContext, Guid, AuthorDetailDto?>
        GetAuthorById = EF.CompileQuery(
            (BookManagerContext context, Guid id) =>
                context.Authors.AsNoTracking()
                    .Include(a => a.Books)
                    .Include(a => a.Publishers)
                    .Where(a => a.AuthorId == id)
                    .Select(a => new AuthorDetailDto(
                        a.FirstName,
                        a.LastName,
                        a.AuthorId,
                        a.Books.Count(),
                        a.Publishers.Count(),
                        a.Books.Select(b => new AuthorsBook(b.BookId, b.Title, b.BookCoverImage)),
                        a.Publishers.Select(p => new AuthorsPublishers(p.PublisherId, p.PublisherName))
                    )).SingleOrDefault()
        );
    
    public static readonly Func<BookManagerContext, string, AuthorDto?>
        GetAuthorByName = EF.CompileQuery(
            (BookManagerContext context, string name) =>
                context.Authors.AsNoTracking()
                    .Include(a => a.Books)
                    .Include(a => a.Publishers)
                    .Where(a => $"{a.FirstName} {a.LastName}" == name)
                    .Select(a => new AuthorDto(
                        $"{a.FirstName} {a.LastName}",
                        a.AuthorId,
                        a.Books.Count(),
                        a.Publishers.Count()
                    )).SingleOrDefault()
        );
}