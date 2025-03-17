using System;
using MSPerson.Domain;
using Xunit;

namespace MSPerson.Tests
{
	public class PersonTests
	{
        [Fact]
        public void SetValidPersonType_ShouldAssignValue()
        {
            var person = new Person();
            var validPersonType = new PersonType();

            person.PersonType = validPersonType;

            Assert.Equal(validPersonType, person.PersonType);
        }
    }
}