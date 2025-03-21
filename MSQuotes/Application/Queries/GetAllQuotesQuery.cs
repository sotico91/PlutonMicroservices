using System.Collections.Generic;
using MediatR;
using MSQuotes.Domain;

namespace MSQuotes.Application.Queries
{
    public class GetAllQuotesQuery : IRequest<List<Quote>> { }
}