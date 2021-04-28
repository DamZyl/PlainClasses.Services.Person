using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MicroserviceLibrary.Application.Configurations.Dispatchers;
using MicroserviceLibrary.Domain.Repositories;
using PlainClasses.Services.Person.Domain.Models.DomainServices;
using SharedModels;

namespace PlainClasses.Services.Person.Application.Commands.CreatePerson
{
    public class CreatePersonCommandHandler : ICommandHandler<CreatePersonCommand, ReturnPersonViewModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPersonPasswordHasher _passwordHasher;
        private readonly IGetMilitaryRankForId _getMilitaryRankForId;
        private readonly IBus _bus;

        public CreatePersonCommandHandler(IUnitOfWork unitOfWork, IPersonPasswordHasher passwordHasher, 
            IGetMilitaryRankForId getMilitaryRankForId, IBus bus)
        {
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
            _getMilitaryRankForId = getMilitaryRankForId;
            _bus = bus;
        }
        
        public async Task<ReturnPersonViewModel> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
        {
            var person = Domain.Models.Person.CreatePerson(request.MilitaryRankId, request.PlatoonId, request.PersonalNumber, request.Password, 
                request.FirstName, request.LastName, request.FatherName, request.BirthDate, request.WorkPhoneNumber,
                request.PersonalPhoneNumber, request.Position, _passwordHasher, _getMilitaryRankForId);

            await _unitOfWork.Repository<Domain.Models.Person>().AddAsync(person);
            await _unitOfWork.CommitAsync(cancellationToken);

            var uri = new Uri("rabbitmq://localhost/personQueue");
            var endPoint = await _bus.GetSendEndpoint(uri);
            await endPoint.Send(new PersonCreatedIntegrationEvent
            {
                PersonId = person.Id,
                PersonalNumber = person.PersonalNumber,
                MilitaryRankAcr = person.MilitaryRankAcr,
                Password = person.Password,
                FirstName = person.FirstName,
                LastName = person.LastName
            }, cancellationToken);
            
            return new ReturnPersonViewModel { Id = person.Id };
        }
    }
}