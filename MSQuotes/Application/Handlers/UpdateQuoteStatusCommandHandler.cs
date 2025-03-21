using System.Threading.Tasks;
using System.Threading;
using MediatR;
using MSQuotes.Application.Commands;
using MSQuotes.Application.Interfaces;

namespace MSQuotes.Application.Handlers
{
 
    public class UpdateQuoteStatusCommandHandler : IRequestHandler<UpdateQuoteStatusCommand, bool>
    {
        private readonly IQuoteService _quoteService;

        public UpdateQuoteStatusCommandHandler(IQuoteService quoteService)
        {
            _quoteService = quoteService;
        }

        public async Task<bool> Handle(UpdateQuoteStatusCommand request, CancellationToken cancellationToken)
        {
            await _quoteService.UpdateQuoteStatusAsync(request);
            return true;
        }
    }
}