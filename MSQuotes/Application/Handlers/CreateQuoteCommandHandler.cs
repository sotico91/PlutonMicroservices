using System.Threading.Tasks;
using System.Threading;
using MediatR;
using MSQuotes.Application.Commands;
using MSQuotes.Application.Interfaces;

namespace MSQuotes.Application.Handlers
{
    public class CreateQuoteCommandHandler : IRequestHandler<CreateQuoteCommand, int>
    {
        private readonly IQuoteService _quoteService;

        public CreateQuoteCommandHandler(IQuoteService quoteService)
        {
            _quoteService = quoteService;
        }

        public async Task<int> Handle(CreateQuoteCommand request, CancellationToken cancellationToken)
        {
            return await _quoteService.CreateQuoteAsync(request);
        }
    }
}