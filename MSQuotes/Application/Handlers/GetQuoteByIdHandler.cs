using System;
using System.Threading.Tasks;
using System.Threading;
using MediatR;
using MSQuotes.Application.DTOs;
using MSQuotes.Application.Interfaces;
using MSQuotes.Application.Queries;

namespace MSQuotes.Application.Handlers
{
    public class GetQuoteByIdHandler : IRequestHandler<GetQuoteByIdQuery, QuoteDto>
    {
        private readonly IQuoteRepository _quoteRepository;

        public GetQuoteByIdHandler(IQuoteRepository quoteRepository)
        {
            _quoteRepository = quoteRepository;
        }

        public async Task<QuoteDto> Handle(GetQuoteByIdQuery request, CancellationToken cancellationToken)
        {
            var quote = await _quoteRepository.GetByIdAsync(request.Id);
            if (quote == null)
            {
                throw new ArgumentException("Quote not found");
            }

            return new QuoteDto
            {
                Id = quote.Id,
                Date = quote.Date,
                Location = quote.Location,
                PatientId = quote.PatientId,
                DoctorId = quote.DoctorId,
                Status = quote.Status.ToString()
            };
        }
    }
}