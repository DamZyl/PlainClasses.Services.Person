using System;
using System.Threading.Tasks;
using Dapper;
using MicroserviceLibrary.Application.Configurations.Data;
using PlainClasses.Services.Person.Domain.Models.DomainServices;

namespace PlainClasses.Services.Person.Application.DomainServices
{
    public class GetPersonForId : IGetPersonForId
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetPersonForId(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }
        
        public Domain.Models.Person Get(Guid personId)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();
            
            const string sql = "SELECT " +
                               "[Person].[Id] " +
                               "FROM Persons AS [Person] " +
                               "WHERE [Person].[Id] = @PersonId ";
            
            var person = connection.QuerySingleOrDefault<Domain.Models.Person>(sql, new { personId });

            return person;
        }
        
        public Domain.Models.Person GetDetail(Guid personId)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();
            
            const string sql = "SELECT " +
                               "[Person].[Id], " +
                               "[Person].[PersonalNumber], " +
                               "[Person].[Password], " +
                               "[Person].[MilitaryRankId], " +
                               "[Person].[MilitaryRankAcr], " +
                               "[Person].[PlatoonId], " +
                               "[Person].[FirstName], " +
                               "[Person].[LastName], " +
                               "[Person].[FatherName], " +
                               "[Person].[BirthDate], " +
                               "[Person].[WorkPhoneNumber], " +
                               "[Person].[PersonalPhoneNumber], " +
                               "[Person].[Position] " +
                               "FROM Persons AS [Person] " +
                               "WHERE [Person].[Id] = @PersonId ";
            
            var person = connection.QuerySingleOrDefault<Domain.Models.Person>(sql, new { personId });

            return person;
        }

        public async Task<Domain.Models.Person> GetAsync(Guid personId)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();
            
            const string sql = "SELECT " +
                               "[Person].[Id] " +
                               "FROM Persons AS [Person] " +
                               "WHERE [Person].[Id] = @PersonId ";
            
            var person = await connection.QuerySingleOrDefaultAsync<Domain.Models.Person>(sql, new { personId });

            return person;
        }
        
        public async Task<Domain.Models.Person> GetDetailAsync(Guid personId)
        {
            var connection = _sqlConnectionFactory.GetOpenConnection();
            
            const string sql = "SELECT " +
                               "[Person].[Id], " +
                               "[Person].[PersonalNumber], " +
                               "[Person].[Password], " +
                               "[Person].[MilitaryRankId], " +
                               "[Person].[MilitaryRankAcr], " +
                               "[Person].[PlatoonId], " +
                               "[Person].[FirstName], " +
                               "[Person].[LastName], " +
                               "[Person].[FatherName], " +
                               "[Person].[BirthDate], " +
                               "[Person].[WorkPhoneNumber], " +
                               "[Person].[PersonalPhoneNumber], " +
                               "[Person].[Position] " +
                               "FROM Persons AS [Person] " +
                               "WHERE [Person].[Id] = @PersonId ";
            
            var person = await connection.QuerySingleOrDefaultAsync<Domain.Models.Person>(sql, new { personId });

            return person;
        }
    }
}