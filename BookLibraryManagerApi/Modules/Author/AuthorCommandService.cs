using BookLibraryManagerApi.Data;
using BookLibraryManagerApi.DataTransferObjects;
using LanguageExt;
using Microsoft.EntityFrameworkCore;

namespace BookLibraryManagerApi.Modules.Author;

public static class AuthorCommandService
{
    public static async Task<AuthorDto> CreateAuthor(
        BookManagerContext context,
        AuthorCreateDto authorDto,
        CancellationToken cancellationToken)
    {
        var author = new DomainModels.Author(authorDto.FirstName, authorDto.LastName);

        context.Authors.Add(author);
        await context.SaveChangesAsync(cancellationToken);

        return new AuthorDto(
            $"{author.FirstName} {author.LastName}",
            author.AuthorId,
            author.Books.Count(),
            author.Publishers.Count());
    }

    public static async Task<Option<AuthorDto>> UpdateAuthor(
        BookManagerContext context,
        AuthorUpdateDto authorDto,
        CancellationToken cancellationToken)
    {
        var author = await context.Authors.AsNoTracking()
            .Include(a => a.Books)
            .Include(a => a.Publishers)
            .SingleOrDefaultAsync(a => a.AuthorId == authorDto.AuthorId, cancellationToken);

        if (author is null)
            return Option<AuthorDto>.None;

        author.UpdateAuthor(authorDto);

        await context.SaveChangesAsync(cancellationToken);

        return Option<AuthorDto>.Some(new AuthorDto($"{author.FirstName} {author.LastName}", 
            author.AuthorId, author.Books.Count(), author.Publishers.Count()));
    }
}