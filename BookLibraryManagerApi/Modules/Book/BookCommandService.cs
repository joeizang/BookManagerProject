using BookLibraryManagerApi.Data;
using BookLibraryManagerApi.DataTransferObjects;
using BookLibraryManagerApi.Extensions;
using LanguageExt;
using Microsoft.EntityFrameworkCore;
using NodaTime;

namespace BookLibraryManagerApi.Modules.Book;

public static class BookCommandService
{
    public static async Task<BookDto> CreateBook(
        BookManagerContext context,
        BookCreateDto bookDto,
        CancellationToken cancellationToken)
    {
        var date = bookDto.PublishedDate.ToInstantDate();
        var book = new DomainModels.Book(bookDto.Title, bookDto.PageCount, 
            date, bookDto.PublisherId, bookDto.Isbn);
        book.AddAuthor(new DomainModels.Author(bookDto.Author.FirstName, bookDto.Author.LastName));

        context.Books.Add(book);
        await context.SaveChangesAsync(cancellationToken);

        return new BookDto( book.BookId, book.Title, bookDto.PublishedDate, book.NumberOfPages,
            book.Authors.Select(a => $"{a.FirstName} {a.LastName},").Concat(),
            book.Publisher.PublisherName,
            book.BookBlob,
            book.BookCoverImage);
    }

    public static async Task<Option<BookDto>> UpdateBook(
        BookManagerContext context,
        BookUpdateDto bookDto,
        CancellationToken cancellationToken)
    {
        var book = await context.Books.FindAsync([bookDto.BookId], cancellationToken);

        if (book is null)
            return Option<BookDto>.None;

        book.UpdateBook(bookDto);
        context.Entry(book).State = EntityState.Modified;
        await context.SaveChangesAsync(cancellationToken);

        return Option<BookDto>
            .Some(new BookDto(book.BookId, book.Title, book.PublishedDate.ToString(), book.NumberOfPages,
                book.Authors.Select(a => $"{a.FirstName} {a.LastName},").Concat(),
                book.Publisher.PublisherName,
                book.BookBlob,
                book.BookCoverImage));
    }
    
    public static async Task<Option<BookDto>> DeleteBook(
        BookManagerContext context,
        Guid bookId,
        CancellationToken cancellationToken)
    {
        var book = await context.Books.FindAsync([bookId], cancellationToken);

        if (book is null)
            return Option<BookDto>.None;

        context.Books.Remove(book);
        await context.SaveChangesAsync(cancellationToken);

        return Option<BookDto>.Some(new BookDto(bookId, book.Title, book.PublishedDate.ToString(), book.NumberOfPages,
            book.Authors.Select(a => $"{a.FirstName} {a.LastName},").Concat(),
            book.Publisher.PublisherName,
            book.BookBlob,
            book.BookCoverImage));
    }
}