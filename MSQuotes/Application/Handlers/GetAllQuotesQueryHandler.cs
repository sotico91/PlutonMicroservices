using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using MediatR;
using MSQuotes.Application.Interfaces;
using MSQuotes.Application.Queries;
using MSQuotes.Domain;
using System.Linq;
using System;

namespace MSQuotes.Application.Handlers
{
    public class GetAllQuotesQueryHandler : IRequestHandler<GetAllQuotesQuery, List<Quote>>
    {
        private readonly IQuoteService _quoteService;

        public GetAllQuotesQueryHandler(IQuoteService quoteService)
        {
            _quoteService = quoteService;
        }

        public async Task<List<Quote>> Handle(GetAllQuotesQuery request, CancellationToken cancellationToken)
        {
            var quotes = await _quoteService.GetAllQuotesAsync();
            return quotes.Select(q => new Quote
            {
                Id = q.Id,
                Date = q.Date,
                Location = q.Location,
                PatientId = q.PatientId,
                DoctorId = q.DoctorId,
                Status = Enum.TryParse<QuoteStatus>(q.Status, out var status) ? status : QuoteStatus.Pending
            }).ToList();
        }
    }
}