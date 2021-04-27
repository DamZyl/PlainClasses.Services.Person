using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlainClasses.Services.Person.Domain.Models;

namespace PlainClasses.Services.Person.Infrastructure.Sql.Configurations
{
    public class MilitaryRankConfiguration : IEntityTypeConfiguration<MilitaryRank>
    {
        public void Configure(EntityTypeBuilder<MilitaryRank> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}