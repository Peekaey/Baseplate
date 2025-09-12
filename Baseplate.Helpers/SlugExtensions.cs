using System.Security.Cryptography;

namespace Baseplate.Helpers;

public static class SlugExtensions
{
    private static readonly int _defaultSlugLength = 8;
    public static string CreateShareableSlug()
    {
        string slug = RandomNumberGenerator.GetString( "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", _defaultSlugLength);
        // string dateTimeStamp = DateTime.UtcNow.ToString("yyyyMMddHHmmss");
        return slug;
    }
}