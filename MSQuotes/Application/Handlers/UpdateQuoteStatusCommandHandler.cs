using System.Threading.Tasks;
using System.Threading;
using MediatR;
using MSQuotes.Application.Commands;
using MSQuotes.Application.Interfaces;
using System;
using MSQuotes.Domain;
using MSRecipes.Application.DTOs;

namespace MSQuotes.Application.Handlers
{
 
    public class UpdateQuoteStatusCommandHandler : IRequestHandler<UpdateQuoteStatusCommand, bool>
    {
        private readonly IQuoteRepository _quoteRepository;
        private readonly IRabbitMQService _rabbitMQService;

        public UpdateQuoteStatusCommandHandler(IQuoteRepository quoteRepository, IRabbitMQService rabbitMQService)
        {
            _quoteRepository = quoteRepository;
            _rabbitMQService = rabbitMQService;
        }

        public async Task<bool> Handle(UpdateQuoteStatusCommand request, CancellationToken cancellationToken)
        {
            var quote = await _quoteRepository.GetByIdAsync(request.Id);
            if (quote == null)
            {
                throw new ArgumentException("Quote not found");
            }

            if (Enum.TryParse(request.Status, out QuoteStatus status))
            {
                quote.Status = status;
                await _quoteRepository.UpdateAsync(quote);

                if (status == QuoteStatus.Completed)
                {
                    var createRecipeDto = new CreateRecipeDto
                    {
                        Code = request.Code,
                        PatientId = quote.PatientId,
                        Description = request.Description,
                        ExpiryDate = request.ExpiryDate
                    };

                    var message = Newtonsoft.Json.JsonConvert.SerializeObject(createRecipeDto);
                    _rabbitMQService.SendMessage(message);
                }

                return true;
            }
            else
            {
                throw new ArgumentException("Invalid status value");
            }
        }
    }
}