namespace PlainClasses.Services.Person.Domain.Utils.Extensions
{
    public static class StringExtension
    {
        public static string ToUppercaseFirstInvariant(this string convertString)
            => char.ToUpperInvariant(convertString[0]) + convertString.Substring(1).ToLowerInvariant();
    }
}