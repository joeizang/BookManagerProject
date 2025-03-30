using NodaTime;
using NodaTime.Extensions;
using NodaTime.Text;

namespace BookLibraryManagerApi.Extensions;

public static class BookLibraryExtensions
{
    public static bool TryParseStringToInstant(this string date, out Instant instant)
    {
        if (InstantPattern.ExtendedIso.Parse(date) is not { Success: true, Value: var parsedEndDate })
        {
            instant = DateTimeOffset.MinValue.ToInstant();
            return false;
        }
        else
        {
            instant = parsedEndDate;
            return true;
        }
    }
    
    public static Instant ToInstantDate(this string date)
    {
        if (InstantPattern.ExtendedIso.Parse(date) is not { Success: true, Value: var parsedEndDate })
        {
            Guid.TryParse(Guid.Empty.ToString(), out var guid);
            return new Instant();
        }
        else
        {
            return parsedEndDate;
        }
    }
}