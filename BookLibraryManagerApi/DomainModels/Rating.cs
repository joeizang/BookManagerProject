namespace BookLibraryManagerApi.DomainModels;

public class Rating
{
    private Rating()
    {
        
    }

    public Rating(string bookReview, double ratingScore, Guid bookId)
    {
        RatingId = Guid.NewGuid();
        RatingScore = CalculateBookRating(ratingScore);
        BookReview = bookReview;
        BookId = bookId;
    }
    public Guid RatingId { get; private set; }

    public string BookReview { get; private set; } = string.Empty;

    public double RatingScore { get; private set; }
    
    public Book Book { get; }
    
    public Guid BookId { get; private set; }

    private static double CalculateBookRating(double score)
    {
        if(score > 5)
        {
            throw new ArgumentException("Rating score cannot be greater than 5");
        }
        return 0D;
    }
    
    public void UpdateRating(string bookReview, double ratingScore)
    {
        BookReview = bookReview;
        RatingScore = CalculateBookRating(ratingScore);
    }
}