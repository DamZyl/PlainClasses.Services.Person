using MicroserviceLibrary.Domain.SharedKernels;

namespace PlainClasses.Services.Person.Domain.Models.DomainServices
{
    public interface IPersonPasswordHasher : IDomainService
    {
        string Hash(string password);
        bool Check(string hash, string password);
    }
}