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
            var expectedPersonType = PersonType.Patient;
            var person = Person.Create("Carla Andrade", "PP", "1555585", DateTime.Now, "84111", "carlaAndrade@example.com", expectedPersonType);

            Assert.Equal(expectedPersonType, person.PersonType);
        }
     }
}