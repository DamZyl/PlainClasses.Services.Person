using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlainClasses.Services.Person.Domain.Models;

namespace PlainClasses.Services.Person.Infrastructure.Sql.Configurations
{
    public class PersonAuthConfiguration : IEntityTypeConfiguration<PersonAuth>
    {
        public void Configure(EntityTypeBuilder<PersonAuth> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .IsRequired()
                .ValueGeneratedNever();
            
            builder.HasOne(x => x.Person)
                .WithMany(x => x.PersonAuths)
                .HasForeignKey(x => x.PersonId);
        }
    }
}