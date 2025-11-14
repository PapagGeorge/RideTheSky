namespace GenericHttpClient.Application;

public static class StringExtensions
{
    public static DateTime? ToNullableDateTime(this string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return null;

        return DateTime.TryParse(value, out var result) ? result : null;
    }
}