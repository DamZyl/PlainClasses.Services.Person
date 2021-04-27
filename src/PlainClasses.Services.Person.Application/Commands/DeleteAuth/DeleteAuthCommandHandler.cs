using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MicroserviceLibrary.Application.Configurations.Dispatchers;
using MicroserviceLibrary.Application.Utils;
using MicroserviceLibrary.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using PlainClasses.Services.Person.Application.Rules;
using PlainClasses.Services.Person.Domain.Models.DomainServices;

namespace PlainClasses.Services.Person.Application.Commands.DeleteAuth
{
    public class DeleteAuthCommandHandler : ICommandHandler<DeleteAuthCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGetPersonAuthForId _getPersonAuthForId;

        public DeleteAuthCommandHandler(IUnitOfWork unitOfWork, IGetPersonAuthForId getPersonAuthForId)
        {
            _unitOfWork = unitOfWork;
            _getPersonAuthForId = getPersonAuthForId;
        }
        
        public async Task<Unit> Handle(DeleteAuthCommand request, CancellationToken cancellationToken)
        {
            var person = await _unitOfWork.Repository<Domain.Models.Person>().FindByWithIncludesAsync(x => x.Id == request.PersonId,
                includes: i => i.Include(x => x.PersonAuths));
            ExceptionHelper.CheckRule(new PersonDoesNotExistRule(person));
            
            person.DeleteAuthFromPerson(request.AuthId, _getPersonAuthForId);

            await _unitOfWork.CommitAsync(cancellationToken);
            
            return Unit.Value;
        }
    }
}