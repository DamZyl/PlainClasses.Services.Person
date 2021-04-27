using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MicroserviceLibrary.Application.Configurations.Dispatchers;
using MicroserviceLibrary.Application.Utils;
using MicroserviceLibrary.Domain.Repositories;
using PlainClasses.Services.Person.Application.Rules;
using PlainClasses.Services.Person.Domain.Models.DomainServices;

namespace PlainClasses.Services.Person.Application.Commands.UpdatePerson
{
    public class UpdatePersonCommandHandler : ICommandHandler<UpdatePersonCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGetPersonForId _getPersonForId;
        private readonly IGetMilitaryRankForId _getMilitaryRankForId;
        private readonly IPersonPasswordHasher _passwordHasher;

        public UpdatePersonCommandHandler(IUnitOfWork unitOfWork, IGetPersonForId getPersonForId, 
            IGetMilitaryRankForId getMilitaryRankForId, IPersonPasswordHasher passwordHasher)
        {
            _unitOfWork = unitOfWork;
            _getPersonForId = getPersonForId;
            _getMilitaryRankForId = getMilitaryRankForId;
            _passwordHasher = passwordHasher;
        }
        
        public async Task<Unit> Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
        {
            var person = await _getPersonForId.GetDetailAsync(request.PersonId);
            ExceptionHelper.CheckRule(new PersonDoesNotExistRule(person));
            
            person.UpdatePersonalData(request.MilitaryRankId, request.PlatoonId, request.Password, request.LastName, 
                request.WorkPhoneNumber, request.PersonalPhoneNumber, request.Position, _passwordHasher, _getMilitaryRankForId);

            await _unitOfWork.Repository<Domain.Models.Person>().EditAsync(person);
            await _unitOfWork.CommitAsync(cancellationToken);
            
            return Unit.Value;
        }
    }
}