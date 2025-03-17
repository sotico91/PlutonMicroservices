using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MSPerson.Domain
{
	public class PersonTypeSpecification
	{
        public bool IsSatisfiedBy(PersonType tipo)
        {
            return Enum.IsDefined(typeof(PersonType), tipo);
        }

    }
}