using System;

namespace PlainClasses.Services.Person.Api.Controllers.Requests
{
    public class UpdatePersonRequest
    {
        public Guid MilitaryRankId { get; set; }
        public Guid? PlatoonId { get; set; }
        public string Password { get; set; }
        public string LastName { get; set; }
        public string WorkPhoneNumber { get; set; }
        public string PersonalPhoneNumber { get; set; }
        public string Position { get; set; }
    }
}