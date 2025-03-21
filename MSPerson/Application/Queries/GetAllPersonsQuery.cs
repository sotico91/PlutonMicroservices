using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using MediatR;
using MSPerson.Application.DTOs;

namespace MSPerson.Application.Queries
{
    public class GetAllPersonsQuery : IRequest<List<PersonDto>> { }

}