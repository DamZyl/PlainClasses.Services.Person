using MicroserviceLibrary.Application.Auths;
using PlainClasses.Services.Person.Domain.Models.DomainServices;

namespace PlainClasses.Services.Person.Application.DomainServices
{
    public class PersonPasswordHasher : IPersonPasswordHasher
    {
        private readonly IPasswordHasher _passwordHasher;

        public PersonPasswordHasher(IPasswordHasher passwordHasher)
        {
            _passwordHasher = passwordHasher;
        }
        
        public string Hash(string password) => _passwordHasher.Hash(password);

        public bool Check(string hash, string password) => _passwordHasher.Check(hash, password);
    }
}