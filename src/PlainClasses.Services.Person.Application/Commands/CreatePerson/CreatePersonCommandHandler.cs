using System.Threading;
using System.Threading.Tasks;
using MicroserviceLibrary.Application.Configurations.Dispatchers;
using MicroserviceLibrary.Domain.Repositories;
using PlainClasses.Services.Person.Domain.Models.DomainServices;

namespace PlainClasses.Services.Person.Application.Commands.CreatePerson
{
    public class CreatePersonCommandHandler : ICommandHandler<CreatePersonCommand, ReturnPersonViewModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPersonPasswordHasher _passwordHasher;
        private readonly IGetMilitaryRankForId _getMilitaryRankForId;

        public CreatePersonCommandHandler(IUnitOfWork unitOfWork, IPersonPasswordHasher passwordHasher, 
            IGetMilitaryRankForId getMilitaryRankForId)
        {
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
            _getMilitaryRankForId = getMilitaryRankForId;
        }
        
        public async Task<ReturnPersonViewModel> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
        {
            var person = Domain.Models.Person.CreatePerson(request.MilitaryRankId, request.PlatoonId, request.PersonalNumber, request.Password, 
                request.FirstName, request.LastName, request.FatherName, request.BirthDate, request.WorkPhoneNumber,
                request.PersonalPhoneNumber, request.Position, _passwordHasher, _getMilitaryRankForId);

            await _unitOfWork.Repository<Domain.Models.Person>().AddAsync(person);
            await _unitOfWork.CommitAsync(cancellationToken);

            return new ReturnPersonViewModel { Id = person.Id };
        }
    }
}