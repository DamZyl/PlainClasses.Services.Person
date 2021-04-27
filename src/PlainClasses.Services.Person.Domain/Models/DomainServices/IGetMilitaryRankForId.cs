using System;
using MicroserviceLibrary.Domain.SharedKernels;

namespace PlainClasses.Services.Person.Domain.Models.DomainServices
{
    public interface IGetMilitaryRankForId : IDomainService
    {
        MilitaryRank Get(Guid militaryRankId);
    }
}