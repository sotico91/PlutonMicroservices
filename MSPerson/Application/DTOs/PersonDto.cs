using System;

namespace MSPerson.Application.DTOs
{
	public class PersonDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string DocumentType { get; set; }

        public string DocumentNumber { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string PersonType { get; set; }
    }
}