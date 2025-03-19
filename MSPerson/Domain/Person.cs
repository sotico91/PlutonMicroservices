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

        public PersonType PersonType { get; private set; }

        private Person(string name, string documentType, string documentNumber, DateTime dateOfBirth,
                   string phoneNumber, string email, PersonType personType)
        {
            Name = name;
            DocumentType = documentType;
            DocumentNumber = documentNumber;
            DateOfBirth = dateOfBirth;
            PhoneNumber = phoneNumber;
            Email = email;
            PersonType = personType;
        }

        private Person() { }

        public static Person Create(string name, string documentType, string documentNumber, DateTime dateOfBirth,
                                    string phoneNumber, string email, PersonType personType)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name is required");
            if (string.IsNullOrWhiteSpace(documentNumber))
                throw new ArgumentException("The document number is required");

            return new Person(name, documentType, documentNumber, dateOfBirth, phoneNumber, email, personType);
        }
        public void Update(string name, string documentType, string documentNumber, DateTime dateOfBirth,
                        string phoneNumber, string email, PersonType personType)
        {
            Name = name;
            DocumentType = documentType;
            DocumentNumber = documentNumber;
            DateOfBirth = dateOfBirth;
            PhoneNumber = phoneNumber;
            Email = email;
            PersonType = personType;
        }
    }
}