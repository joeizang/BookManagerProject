namespace BookLibraryManagerApi.DataTransferObjects;

public record AuthorDto(string FullName, Guid AuthorId, int NumberOfBooksAuthored,
        int NumberOfPublishingHousesCollaborated);

public record AuthorCreateDto(string FirstName, string LastName);

public record AuthorUpdateDto(string FirstName, string LastName, Guid AuthorId);

public record AuthorDetailDto(string FirstName, string LastName, Guid AuthorId,
        int NumberOfBooksAuthored, int NumberOfPublishingHousesCollaborated,
        IEnumerable<AuthorsBooks> BooksAuthored, IEnumerable<AuthorsPublishers> AuthorsPublishers
        );

public record AuthorsBooks(Guid BookId, string BookTitle, string BookCoverImage);

public record AuthorsPublishers(Guid PublisherId, string PublisherName);
