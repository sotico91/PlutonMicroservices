using System;
using System.Collections.Generic;

namespace MSPerson.Domain
{
    public class Person
    {

        public int Id { get; set; }
        public string Name { get; set; }

        public string DocumentType { get; set; }

        public string DocumentNumber { get; set; }

        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        private PersonType _personType;

        private readonly PersonTypeSpecification _personTypeSpecification;

        public Person()
        {
            _personTypeSpecification = new PersonTypeSpecification();
        }

        public PersonType PersonType
        {
            get => _personType;
            set
            {
                if (!_personTypeSpecification.IsSatisfiedBy(value))
                {
                    throw new ArgumentException("Invalid value for PersonType");
                }
                _personType = value;
            }
        }
    }
}