namespace BookLibraryManagerApi.DataTransferObjects;

public record RatingDto(Guid RatingId, double RatingScore, string BookName, Guid BookId, string BookAuthorName);

public record RatingCreateDto(Guid BookId, double RatingScore, string ReviewText);

public record RatingUpdateDto(Guid RatingId, Guid BookId, Double RatingScore, string ReviewText);

public record RatingDetailDto(Guid RatingId,
    double RatingScore,
    string BookName,
    Guid BookId,
    string BookAuthorName,
    string RewviewText);
    
public record AverageRatingByBookPublisher(string PublisherName, double AverageRating);    