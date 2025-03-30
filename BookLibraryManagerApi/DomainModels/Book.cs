using BookLibraryManagerApi.DataTransferObjects;
using BookLibraryManagerApi.Extensions;
using LanguageExt;
using NodaTime;

namespace BookLibraryManagerApi.DomainModels;

public class Book
{
    private readonly List<Rating> _ratings = [];
    private readonly List<Author> _authors = [];
    
    
    private Book(Guid publisherId)
    {
        PublisherId = publisherId;
        Authors = _authors.AsReadOnly();
        Ratings = _ratings.AsReadOnly();
    }
    
    public Book(string title, int numberOfPages, Instant publishedDate, Guid publisherId, string isbn)
    {
        BookId = Guid.NewGuid();
        Title = title;
        NumberOfPages = numberOfPages;
        PublisherId = publisherId;
        Isbn = isbn;
        PublishedDate = ValidatePublishDate(publishedDate)
            .Match(result => result, Instant.MinValue);// Don't save when the date is minvalue
        Authors = _authors.AsReadOnly();
        Ratings = _ratings.AsReadOnly();
    }
    
    public Guid BookId { get; private set; }
    
    public string Title { get; private set; } = string.Empty;
    
    public int NumberOfPages { get; private set; }

    public string BookBlob { get; private set; } = string.Empty;

    public string BookCoverImage { get; private set; } = string.Empty;

    public string Isbn { get; private set; }

    public void AddBookDetails(BookDetails bookDetail)
    {
        if(!string.IsNullOrWhiteSpace(bookDetail.BookBlob))
        {
            BookBlob = bookDetail.BookBlob;
        }

        if (!string.IsNullOrWhiteSpace(bookDetail.BookCoverImage))
        {
            BookCoverImage = bookDetail.BookCoverImage;
        }
    }
    
    public Instant PublishedDate { get; private set; }

    public IEnumerable<AuthorsBooks> AuthorsBooks { get; }
    
    public IEnumerable<Rating> Ratings { get; }

    public Publisher Publisher { get; }
    
    public Guid PublisherId { get; private set; }

    public IEnumerable<Author> Authors { get; }

    private static Option<Instant> ValidatePublishDate(Instant date)
    {
        return date == default || date == Instant.MinValue 
            ? Option<Instant>.None 
            : Option<Instant>.Some(date);
    }
    
    public void AddAuthor(Author author)
    {
            if (Authors.Contains(author))
            {
                return;
            }
            _authors.Add(author);
            author.AddAuthoredBook(this);
            // author.SignUpWithAPublisher(PublisherId);
    }
    
    public void AddRating(Rating rating)
    {
        if (Ratings.Contains(rating))
        {
            return;
        }
        _ratings.Add(rating);
    }

    public void UpdateBook(BookUpdateDto dto)
    {
        Title = dto.BookTitle;
        NumberOfPages = dto.PageCount;
        Isbn = dto.Isbn;
        PublishedDate = ValidatePublishDate(dto.PublishedDate.ToInstantDate())
            .Match(result => result, Instant.MinValue);
        BookBlob = dto.BookBlob ?? BookBlob;
        BookCoverImage = dto.BookCoverImage ?? BookCoverImage;
    }


}