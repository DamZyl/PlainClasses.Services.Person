using MicroserviceLibrary.Domain.SharedKernels;

namespace PlainClasses.Services.Person.Domain.Models.Rules
{
    public class MilitaryRankExistRule : IBusinessRule
    {
        private readonly MilitaryRank _militaryRank;

        public MilitaryRankExistRule(MilitaryRank militaryRank)
        {
            _militaryRank = militaryRank;
        }

        public bool IsBroken() => _militaryRank == null;

        public string Message => $"Military rank with id: {_militaryRank.Id} does not exist.";
    }
}