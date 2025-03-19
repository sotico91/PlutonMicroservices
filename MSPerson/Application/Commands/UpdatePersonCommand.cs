using System;
using MediatR;
using MSPerson.Domain;

namespace MSPerson.Application.Commands
{
    public class UpdatePersonCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DocumentType { get; set; }
        public string DocumentNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public PersonType? PersonType { get; set; }
    }
}