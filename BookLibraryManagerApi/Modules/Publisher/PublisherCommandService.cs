using BookLibraryManagerApi.Data;
using BookLibraryManagerApi.DataTransferObjects;
using LanguageExt;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookLibraryManagerApi.Modules.Publisher;

public static class PublisherCommandService
{
    public static async Task<PublisherCreatedDto> CreatePublisher(BookManagerContext context,
        PublisherCreateDto dto, CancellationToken token)
    {
        var publisher = new DomainModels.Publisher(dto.PublisherName, dto.PublisherAddress);
        
        context.Publishers.Add(publisher);
        await context.SaveChangesAsync(token).ConfigureAwait(false);
        
        return new PublisherCreatedDto(publisher.PublisherId);
    }
    
    public static async Task<Option<PublisherDto>> UpdatePublisher(BookManagerContext context,
        PublisherUpdateDto dto, CancellationToken token)
    {
        var publisher = await context.Publishers.FindAsync(new object?[] { dto.PublisherId }, token)
            .ConfigureAwait(false);
        if (publisher is null)
            return Option<PublisherDto>.None;
        
        publisher.UpdatePublisherDetails(dto);

        context.Entry(publisher).State = EntityState.Modified;
        
        await context.SaveChangesAsync(token).ConfigureAwait(false);

        return Option<PublisherDto>.Some(new PublisherDto(
            publisher.PublisherName,
            publisher.PublisherId,
            publisher.BooksPublished.Count(),
            publisher.BookAuthors.Count()));
    }

    public static async Task<Option<PublisherCreatedDto>> UpdatePublisherAuthorRoster(
        [FromServices] BookManagerContext context,
        [FromBody] PublisherAuthorRosterUpdate author, CancellationToken token)
    {
        var publisher = await context.Publishers.FindAsync([author.PublisherId], token)
            .ConfigureAwait(false);
        if (publisher is null)
            return Option<PublisherCreatedDto>.None;
        
        publisher.AddAuthor(new DomainModels.Author(author.AuthorId));
        context.Entry(publisher).State = EntityState.Modified;
        
        await context.SaveChangesAsync(token).ConfigureAwait(false);
        
        return Option<PublisherCreatedDto>.Some(new PublisherCreatedDto(publisher.PublisherId));
    }
    
    public static async Task<Option<EmptyPublisher>> DeletePublisher(
        [FromServices] BookManagerContext context, Guid publisherId, CancellationToken token)
    {
        var publisher = await context.Publishers.FindAsync([publisherId], token)
            .ConfigureAwait(false);
        if (publisher is null)
            return Option<EmptyPublisher>.None;
        
        context.Publishers.Remove(publisher);
        await context.SaveChangesAsync(token).ConfigureAwait(false);
        
        return Option<EmptyPublisher>.Some(new EmptyPublisher());
    }
}