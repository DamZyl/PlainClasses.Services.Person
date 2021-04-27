using System;
using System.Threading.Tasks;
using MicroserviceLibrary.Domain.SharedKernels;

namespace PlainClasses.Services.Person.Domain.Models.DomainServices
{
    public interface IGetPersonForId : IDomainService
    {
        Person Get(Guid personId);
        Person GetDetail(Guid personId);
        Task<Person> GetAsync(Guid personId);
        Task<Person> GetDetailAsync(Guid personId);
    }
}