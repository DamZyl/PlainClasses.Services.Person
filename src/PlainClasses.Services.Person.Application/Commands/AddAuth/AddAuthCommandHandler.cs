using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using MicroserviceLibrary.Application.Configurations.Dispatchers;
using MicroserviceLibrary.Application.Utils;
using MicroserviceLibrary.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using PlainClasses.Services.Person.Application.Commands.CreatePerson;
using PlainClasses.Services.Person.Application.Rules;
using SharedModels;

namespace PlainClasses.Services.Person.Application.Commands.AddAuth
{
    public class AddAuthCommandHandler : ICommandHandler<AddAuthCommand, ReturnPersonViewModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBus _bus;

        public AddAuthCommandHandler(IUnitOfWork unitOfWork, IBus bus)
        {
            _unitOfWork = unitOfWork;
            _bus = bus;
        }
        
        public async Task<ReturnPersonViewModel> Handle(AddAuthCommand request, CancellationToken cancellationToken)
        {
            var person = await _unitOfWork.Repository<Domain.Models.Person>()
                .FindByWithIncludesAsync(x => x.Id == request.PersonId,
                includes: i => i.Include(x => x.PersonAuths));
            ExceptionHelper.CheckRule(new PersonDoesNotExistRule(person));
            
            person.AddAuthToPerson(request.AuthName);

            await _unitOfWork.CommitAsync(cancellationToken);
            
            var uri = new Uri("rabbitmq://localhost/personQueue");
            var endPoint = await _bus.GetSendEndpoint(uri);
            await endPoint.Send(new AuthAddedIntegrationEvent()
            {
                PersonId = person.Id,
                AuthName = request.AuthName
            }, cancellationToken);

            return new ReturnPersonViewModel { Id =  person.Id };
        }
    }
}