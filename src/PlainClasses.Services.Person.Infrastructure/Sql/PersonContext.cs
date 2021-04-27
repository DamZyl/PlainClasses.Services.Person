using MicroserviceLibrary.Application.Configurations.Options;
using MicroserviceLibrary.Infrastructure.Databases;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PlainClasses.Services.Person.Domain.Models;

namespace PlainClasses.Services.Person.Infrastructure.Sql
{
    public class PersonContext : AbstractApplicationDbContext
    {
        #region DbSets
        
        public DbSet<Domain.Models.Person> Persons { get; set; }
        public DbSet<PersonAuth> PersonAuths { get; set; }
        public DbSet<MilitaryRank> MilitaryRanks { get; set; }

        #endregion
        
        public PersonContext(IOptions<SqlOption> options) : base(options, "PlainClasses.Services.Person.Api") { }
    }
}