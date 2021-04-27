using System;
using Dapper;
using MicroserviceLibrary.Application.Configurations.Data;
using PlainClasses.Services.Person.Domain.Models;
using PlainClasses.Services.Person.Domain.Models.DomainServices;

namespace PlainClasses.Services.Person.Application.DomainServices
{
    public class GetPersonAuthForId : IGetPersonAuthForId
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetPersonAuthForId(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }
        
        public PersonAuth Get(Guid authId)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();
            
            const string sql = "SELECT " + 
                               "[PersonAuth].[Id] " +
                               "FROM PersonAuths AS [PersonAuth] " +
                               "WHERE [PersonAuth].[Id] = @authId ";
            
            var personAuth = connection.QuerySingleOrDefault<PersonAuth>(sql, new { authId });

            return personAuth;
        }
    }
}