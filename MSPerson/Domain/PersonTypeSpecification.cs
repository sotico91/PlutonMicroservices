using System;

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