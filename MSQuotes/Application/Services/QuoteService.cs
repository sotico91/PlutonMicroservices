using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MSQuotes.Application.Commands;
using MSQuotes.Application.DTOs;
using MSQuotes.Application.Interfaces;
using MSQuotes.Domain;
using MSRecipes.Application.DTOs;
using Newtonsoft.Json;

namespace MSQuotes.Application.Services
{
	public class QuoteService : IQuoteService
	{

        private readonly IQuoteRepository _quoteRepository;
        private readonly IRabbitMQService _rabbitMQService;

        public QuoteService(IQuoteRepository quoteRepository, IRabbitMQService rabbitMQService)
        {
            _quoteRepository = quoteRepository;
            _rabbitMQService = rabbitMQService;
        }

        public async Task<int> CreateQuoteAsync(CreateQuoteCommand createQuoteCommand)
        {
            var quote = new Quote
            {
                Date = createQuoteCommand.Date,
                Location = createQuoteCommand.Location,
                PatientId = createQuoteCommand.PatientId,
                DoctorId = createQuoteCommand.DoctorId,
                Status = QuoteStatus.Pending
            };

            await _quoteRepository.AddAsync(quote);
            return quote.Id;
        }

        public async Task UpdateQuoteStatusAsync(UpdateQuoteStatusCommand updateQuoteStatusCommand)
        {
            var quote = await _quoteRepository.GetByIdAsync(updateQuoteStatusCommand.Id);
            if (quote == null)
                throw new KeyNotFoundException("Quote not found");

            if (!Enum.TryParse(updateQuoteStatusCommand.Status, out QuoteStatus status))
                throw new ArgumentException("Invalid status value");

            quote.Status = status;
            await _quoteRepository.UpdateAsync(quote);

            // Si la cita ha sido completada, enviamos un mensaje a RabbitMQ
            if (status == QuoteStatus.Completed)
            {
                var createRecipeDto = new CreateRecipeDto
                {
                    Code = updateQuoteStatusCommand.Code,
                    PatientId = quote.PatientId,
                    Description = updateQuoteStatusCommand.Description,
                    ExpiryDate = updateQuoteStatusCommand.ExpiryDate
                };

                var message = JsonConvert.SerializeObject(createRecipeDto);
                _rabbitMQService.SendMessage(message);
            }
        }

        public async Task<IEnumerable<QuoteDto>> GetAllQuotesAsync()
        {
            var quotes = await _quoteRepository.GetAllAsync();
            return quotes.Select(ConvertToDto).ToList();
        }

        private static QuoteDto ConvertToDto(Quote quote)
        {
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