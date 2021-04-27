using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MicroserviceLibrary.Application.Configurations.Dispatchers;
using MicroserviceLibrary.Application.Utils;
using MicroserviceLibrary.Domain.Repositories;
using PlainClasses.Services.Person.Application.Rules;
using PlainClasses.Services.Person.Domain.Models.DomainServices;

namespace PlainClasses.Services.Person.Application.Commands.DeletePerson
{
    public class DeletePersonCommandHandler : ICommandHandler<DeletePersonCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGetPersonForId _getPersonForId;

        public DeletePersonCommandHandler(IUnitOfWork unitOfWork, IGetPersonForId getPersonForId)
        {
            _unitOfWork = unitOfWork;
            _getPersonForId = getPersonForId;
        }
        
        public async Task<Unit> Handle(DeletePersonCommand request, CancellationToken cancellationToken)
        {
            var person = await _getPersonForId.GetAsync(request.PersonId);
            ExceptionHelper.CheckRule(new PersonDoesNotExistRule(person));

            await _unitOfWork.Repository<Domain.Models.Person>().DeleteAsync(person);
            await _unitOfWork.CommitAsync(cancellationToken);
            
            return Unit.Value;
        }
    }
}