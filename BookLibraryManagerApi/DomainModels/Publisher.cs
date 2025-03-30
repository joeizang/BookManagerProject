using BookLibraryManagerApi.DataTransferObjects;

namespace BookLibraryManagerApi.DomainModels;

public class Publisher
{
    private readonly List<Book> _books = [];
    private readonly List<Author> _authors = [];
    private Publisher()
    {
        PublisherId = Guid.NewGuid();
        BooksPublished = _books.AsReadOnly();
        BookAuthors = _authors.AsReadOnly();
    }

    public Publisher(string publisherName, string publisherAddress)
    {
        PublisherId = Guid.NewGuid();
        PublisherName = publisherName;
        PublisherAddress = publisherAddress;
        BooksPublished = _books.AsReadOnly();
        BookAuthors = _authors.AsReadOnly();
    }

    public Publisher(Guid publisherId)
    {
        PublisherId = publisherId;
    }
    public Guid PublisherId { get; private set; }

    public string PublisherName { get; private set; }

    public string PublisherAddress { get; private set; }

    public IEnumerable<Book> BooksPublished { get; }

    public IEnumerable<Author> BookAuthors { get; }
    
    public void AddBook(Book book)
    {
        _books.Add(book);
    }
    
    public void AddAuthor(Author author)
    {
        _authors.Add(author);
    }

    public void UpdatePublisherDetails(PublisherUpdateDto dto)
    {
        PublisherName = dto.PublisherName;
        PublisherAddress = dto.PublisherAddress;
    }
    
    public void RemoveBook(Book book)
    {
        _books.Remove(book);
    }
    
    
}