namespace BookLibraryManagerApi.DataTransferObjects;

public record PublisherDto(string PublisherName, Guid PublisherId, int NumberOfBooksPublished, int AuthorsCount);

public record PublisherCreateDto(string PublisherName, string PublisherAddress);

public record PublisherCreatedDto(Guid PublisherId);

public record PublisherUpdateDto(string PublisherName, string PublisherAddress, Guid PublisherId);