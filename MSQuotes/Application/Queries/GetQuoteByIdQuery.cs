using MediatR;
using MSQuotes.Application.DTOs;

namespace MSQuotes.Application.Queries
{
    public class GetQuoteByIdQuery : IRequest<QuoteDto>
    {
        public int Id { get; set; }
        public GetQuoteByIdQuery(int id) => Id = id;
    }
}