using BookLibraryManagerApi.DataTransferObjects;

namespace BookLibraryManagerApi.DomainModels;

public class Author
{
    private readonly List<Book> _books = [];
    private readonly List<Publisher> _publishers = [];
    
    private Author()
    {
        AuthorId = Guid.NewGuid();
        Books = _books.AsReadOnly();
        Publishers = _publishers.AsReadOnly();
    }

    public Author(string firstName, string lastName)
    {
        FirstName ??= firstName;
        LastName ??= lastName;
        Books = _books.AsReadOnly();
        Publishers = _publishers.AsReadOnly();
    }
    public Guid AuthorId { get; private set; }

    public string FirstName { get; private set; }

    public string LastName { get; private set; }

    public IEnumerable<AuthorsBooks> AuthorsBooks { get; }

    public IEnumerable<AuthorsPublishers> AuthorsPublishers { get; }

    public IEnumerable<Book> Books { get; }

    public IEnumerable<Publisher> Publishers { get; }

    public double AverageRatingOfBooksAuthored()
    {
        // var totalRatingScore = Books.SelectMany(b => b.Ratings).Sum(r => r.RatingScore);
        var t = 0D;
        foreach (var book in Books)
        {
            foreach (var rating in book.Ratings)
            {
                t += rating.RatingScore;
            }
        }
        return t / Books.Count();
    }
    
    public void AddAuthoredBook(Book book)
    {
        if (Books.Contains(book))
        {
            return;
        }
        book.AddAuthor(this);
        _books.Add(book);
    }
    
    public void SignUpWithAPublisher(Guid publisherId)
    {
        var publisher = new Publisher(publisherId);
        publisher.AddAuthor(this);
        _publishers.Add(publisher);
    }

    public void UpdateAuthor(AuthorUpdateDto dto)
    {
        FirstName = dto.FirstName;
        LastName = dto.LastName;
    }
}