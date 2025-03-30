namespace BookLibraryManagerApi.DataTransferObjects;

public record BookDetails(string? BookBlob, 
    string? BookCoverImage,
    string AuthorName,
    string PublishDate,
    string PublisherName,
    Guid BookId,
    Guid AuthorId,
    Guid PublisherId,
    string BookTitle);

public record BookDto(
    Guid BookId,
    string Title,
    string PublishedDate,
    int PageCount,
    string? BookBlob = null,
    string? BookCoverImage = null
);

public record BookCreateDto(
    string Title,
    string? BookBlob,
    string? BookCoverImage,
    string PublishedDate,
    BookAuthor Author,
    Guid PublisherId,
    string Isbn,
    int PageCount = 0
);

public record BookAuthor(string FirstName, string LastName);

public record BookUpdateDto(
    Guid BookId,
    string BookTitle,
    string? BookBlob,
    string? BookCoverImage,
    string PublishedDate,
    Guid PublisherId,
    string Isbn,
    int PageCount = 0
);