namespace BookLibraryManagerApi.DomainModels;

public class AuthorsPublishers
{
    public Guid AuthorId { get; set; }
    
    public Author Author { get; set; }
    
    public Guid PublisherId { get; set; }
    
    public Publisher Publisher { get; set; }
}