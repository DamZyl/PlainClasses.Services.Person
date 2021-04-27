using System;
using Dapper;
using MicroserviceLibrary.Application.Configurations.Data;
using PlainClasses.Services.Person.Domain.Models;
using PlainClasses.Services.Person.Domain.Models.DomainServices;

namespace PlainClasses.Services.Person.Application.DomainServices
{
    public class GetMilitaryRankForId : IGetMilitaryRankForId
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetMilitaryRankForId(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }
        
        public MilitaryRank Get(Guid militaryRankId)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();
            
            const string sqlMilitaryRank = "SELECT " +
                                           "[MilitaryRank].[Id], " +
                                           "[MilitaryRank].[Acronym] " +
                                           "FROM MilitaryRanks AS [MilitaryRank] " +
                                           "WHERE [MilitaryRank].[Id] = @MilitaryRankId ";
            
            var militaryRank = connection.QuerySingleOrDefault<MilitaryRank>(sqlMilitaryRank, new { militaryRankId });

            return militaryRank;
        }
    }
}