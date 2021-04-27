using System.Threading;
using System.Threading.Tasks;
using MicroserviceLibrary.Application.Configurations.Dispatchers;
using MicroserviceLibrary.Application.Utils;
using MicroserviceLibrary.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using PlainClasses.Services.Person.Application.Commands.CreatePerson;
using PlainClasses.Services.Person.Application.Rules;

namespace PlainClasses.Services.Person.Application.Commands.AddAuth
{
    public class AddAuthCommandHandler : ICommandHandler<AddAuthCommand, ReturnPersonViewModel>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddAuthCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public async Task<ReturnPersonViewModel> Handle(AddAuthCommand request, CancellationToken cancellationToken)
        {
            var person = await _unitOfWork.Repository<Domain.Models.Person>()
                .FindByWithIncludesAsync(x => x.Id == request.PersonId,
                includes: i => i.Include(x => x.PersonAuths));
            ExceptionHelper.CheckRule(new PersonDoesNotExistRule(person));
            
            person.AddAuthToPerson(request.AuthName);

            await _unitOfWork.CommitAsync(cancellationToken);

            return new ReturnPersonViewModel { Id =  person.Id };
        }
    }
}