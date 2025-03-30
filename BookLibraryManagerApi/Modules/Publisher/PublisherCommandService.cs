using BookLibraryManagerApi.Data;
using BookLibraryManagerApi.DataTransferObjects;
using LanguageExt;
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
        var publisher = await context.Publishers.FindAsync([dto.PublisherId], token)
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
}