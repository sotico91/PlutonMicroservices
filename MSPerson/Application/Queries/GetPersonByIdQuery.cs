using MediatR;
using MSPerson.Domain;

namespace MSPerson.Application.Queries
{
	public class GetPersonByIdQuery : IRequest<Person>
	{
        public int Id { get; set; }

        public GetPersonByIdQuery(int id)
        {
            Id = id;
        }
    }
}