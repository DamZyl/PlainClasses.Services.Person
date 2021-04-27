namespace PlainClasses.Services.Person.Domain.Utils
{
    public static class Consts
    {
        public const string PhoneNumberRegex = @"(?<!\w)(\(?(\+|00)?48\)?)?[ -]?\d{3}[ -]?\d{3}[ -]?\d{3}(?!\w)";
    }
}