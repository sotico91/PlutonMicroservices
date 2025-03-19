using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using MediatR;
using MSQuotes.Domain;
using MSQuotes.Application.Interfaces;

namespace MSQuotes.Application.Queries
{
    public class GetAllQuotesQuery : IRequest<List<Quote>> { }

    public class GetAllQuotesQueryHandler : IRequestHandler<GetAllQuotesQuery, List<Quote>>
    {
        private readonly IQuoteRepository _quoteRepository;

        public GetAllQuotesQueryHandler(IQuoteRepository quoteRepository)
        {
            _quoteRepository = quoteRepository;
        }

        public async Task<List<Quote>> Handle(GetAllQuotesQuery request, CancellationToken cancellationToken)
        {
            return (List<Quote>)await _quoteRepository.GetAllAsync();
        }
    }
}