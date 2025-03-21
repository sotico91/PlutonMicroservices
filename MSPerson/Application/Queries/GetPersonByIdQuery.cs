using MediatR;
using MSPerson.Application.DTOs;

namespace MSPerson.Application.Queries
{
	public class GetPersonByIdQuery : IRequest<PersonDto>
	{
        public int Id { get; set; }

        public GetPersonByIdQuery(int id)
        {
            Id = id;
        }
    }
}