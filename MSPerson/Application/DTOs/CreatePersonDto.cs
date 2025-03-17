using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSPerson.Application.DTOs
{
	public class CreatePersonDto
	{
        public string Name { get; set; }
        public string DocumentType { get; set; }
        public string DocumentNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string PersonType { get; set; }

    }
}