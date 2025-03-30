namespace BookLibraryManagerApi.DataTransferObjects;

public record PublisherDto(string PublisherName, Guid PublisherId, 
    int NumberOfBooksPublished, int AuthorsCount,
    IEnumerable<BookPublishedDto> BooksPublished = null);

public record PublisherCreateDto(string PublisherName, string PublisherAddress);

public record PublisherCreatedDto(Guid PublisherId);

public record PublisherUpdateDto(string PublisherName, string PublisherAddress, Guid PublisherId);

public record PublisherAuthorRosterUpdate(Guid AuthorId, Guid PublisherId);

public record NewPublisherAuthor(Guid AuthorId);

public record EmptyPublisher();

public record BookPublishedDto(Guid BookId, string Title, string ISBN, string AuthorName);

