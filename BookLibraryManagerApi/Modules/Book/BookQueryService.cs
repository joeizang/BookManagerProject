using BookLibraryManagerApi.Data;
using BookLibraryManagerApi.DataTransferObjects;
using Microsoft.EntityFrameworkCore;

namespace BookLibraryManagerApi.Modules.Book;

public static class BookQueryService
{
    public static readonly Func<BookManagerContext, IEnumerable<BookDto>> 
        GetAllBooks = EF.CompileQuery(
            (BookManagerContext context) =>
                context.Books.AsNoTracking()
                    .Select(b => new BookDto(
                        b.BookId,
                        b.Title,
                        b.PublishedDate.ToString(),
                        b.NumberOfPages,
                        b.Authors.Select(ba => $"{ba.FirstName} {ba.LastName},").Concat(),
                        b.Publisher.PublisherName,
                        b.BookBlob,
                        b.BookCoverImage
                    ))
        );

    public static readonly Func<BookManagerContext, Guid, Task<BookDetails?>> 
        GetBookById = EF.CompileAsyncQuery(
            (BookManagerContext context, Guid id) =>
                context.Books.AsNoTracking()
                    .Include(b => b.Authors)
                    .Include(b => b.Publisher)
                    .Where(b => b.BookId == id)
                    .Select(b => new BookDetails(
                        b.BookBlob,
                        b.BookCoverImage,
                        b.Authors.Select(ba => $"{ba.FirstName} {ba.LastName},").Concat(),
                        b.PublishedDate.ToString(),
                        b.Publisher.PublisherName,
                        b.BookId,
                        b.Authors.SingleOrDefault()!.AuthorId,
                        b.Publisher.PublisherId,
                        b.Title
                    )).SingleOrDefault()
        );

    public static readonly Func<BookManagerContext, string, Task<BookDto?>>
        GetBookByTitle = EF.CompileAsyncQuery(
            (BookManagerContext context, string title) =>
                context.Books.AsNoTracking()
                    .Include(b => b.Authors)
                    .Include(b => b.Publisher)
                    .Where(b => b.Title == title)
                    .Select(b => new BookDto(
                        b.BookId,
                        b.Title,
                        b.PublishedDate.ToString(),
                        b.NumberOfPages,
                        b.Authors.Select(ba => $"{ba.FirstName} {ba.LastName},").Concat(),
                        b.Publisher.PublisherName,
                        b.BookBlob,
                        b.BookCoverImage
                    )).SingleOrDefault()
        );
}