using System;
using System.Collections.Generic;
using MicroserviceLibrary.Domain.SharedKernels;

namespace PlainClasses.Services.Person.Domain.Models
{
    public class MilitaryRank : Entity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Acronym { get; set; }
        private ISet<Person> _persons = new HashSet<Person>();
        public IEnumerable<Person> Persons => _persons;
    }
}