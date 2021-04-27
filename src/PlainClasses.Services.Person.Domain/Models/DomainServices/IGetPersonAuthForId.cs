using System;
using MicroserviceLibrary.Domain.SharedKernels;

namespace PlainClasses.Services.Person.Domain.Models.DomainServices
{
    public interface IGetPersonAuthForId : IDomainService
    {
        PersonAuth Get(Guid authId);
    }
}