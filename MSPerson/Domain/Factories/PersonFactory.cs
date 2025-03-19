using System;
namespace MSPerson.Domain.Factories
{
    public static class PersonFactory
    {
        public static Person Create(string name, string documentType, string documentNumber, DateTime dateOfBirth, string phoneNumber, string email, string personType)
        {
            if (!Enum.TryParse(personType, out PersonType parsedPersonType))
            {
                throw new ArgumentException("Invalid PersonType value.");
            }

            return Person.Create(name, documentType, documentNumber, dateOfBirth, phoneNumber, email, parsedPersonType);
        }
    }
}