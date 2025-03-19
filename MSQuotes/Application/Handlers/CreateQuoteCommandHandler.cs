using System.Threading.Tasks;
using System.Threading;
using MediatR;
using MSQuotes.Application.Commands;
using MSQuotes.Application.Interfaces;
using MSQuotes.Domain;

namespace MSQuotes.Application.Handlers
{
    public class CreateQuoteCommandHandler : IRequestHandler<CreateQuoteCommand, int>
    {
        private readonly IQuoteRepository _quoteRepository;

        public CreateQuoteCommandHandler(IQuoteRepository quoteRepository)
        {
            _quoteRepository = quoteRepository;
        }

        public async Task<int> Handle(CreateQuoteCommand request, CancellationToken cancellationToken)
        {
            var quote = new Quote
            {
                Date = request.Date,
                Location = request.Location,
                PatientId = request.PatientId,
                DoctorId = request.DoctorId,
                Status = QuoteStatus.Pending
            };

            await _quoteRepository.AddAsync(quote);
            return quote.Id;
        }
    }
}