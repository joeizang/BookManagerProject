namespace BookLibraryManagerApi.DataTransferObjects;

public record AuthorDto(string FullName, Guid AuthorId, int NumberOfBooksAuthored,
        int NumberOfPublishingHousesCollaborated);

public record AuthorCreateDto(string FirstName, string LastName);

public record AuthorUpdateDto(string FirstName, string LastName, Guid AuthorId, 
        AuthorBook? Book, AuthorsPublisher? Publisher);

public record AuthorDetailDto(string FirstName, string LastName, Guid AuthorId,
        int NumberOfBooksAuthored, int NumberOfPublishingHousesCollaborated,
        IEnumerable<AuthorsBook> BooksAuthored, IEnumerable<AuthorsPublishers> AuthorsPublishers
        );

public record AuthorsBook(Guid BookId, string BookTitle, string BookCoverImage);

public record AuthorBook(Guid BookId);

public record AuthorsPublisher(Guid PublisherId);

public record AuthorsPublishers(Guid PublisherId, string PublisherName);

public record EmptyAuthor();
