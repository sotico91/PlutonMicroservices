using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MSQuotes.Application.DTOs;
using MSQuotes.Application.Interfaces;
using MSQuotes.Domain;
using MSQuotes.Infrastructure.Messaging;
using MSRecipes.Application.DTOs;

namespace MSQuotes.Application.Services
{
	public class QuoteService : IQuoteService
	{

        private readonly IQuoteRepository _quoteRepository;
        private readonly RabbitMQService _rabbitMQService;

        public QuoteService(IQuoteRepository quoteRepository, RabbitMQService rabbitMQService)
        {
            _quoteRepository = quoteRepository;
            _rabbitMQService = rabbitMQService;
        }

        public async Task CreateQuoteAsync(CreateQuoteDto createQuoteDto)
        {
            var quote = new Quote
            {
                Date = createQuoteDto.Date,
                Location = createQuoteDto.Location,
                PatientId = createQuoteDto.PatientId,
                DoctorId = createQuoteDto.DoctorId,
                Status = QuoteStatus.Pending
            };

            await _quoteRepository.AddAsync(quote);
        }

        public async Task UpdateQuoteStatusAsync(int Id, UpdateQuoteStatusDto updateQuoteStatusDto)
        {
            var quote = await _quoteRepository.GetByIdAsync(Id);
            if (quote == null)
            {
                throw new ArgumentException("Quote not found");
            }

            if (Enum.TryParse(updateQuoteStatusDto.Status, out QuoteStatus status))
            {
                quote.Status = status;
                await _quoteRepository.UpdateAsync(quote);

                if (status == QuoteStatus.Completed)
                {
                    var createRecipeDto = new CreateRecipeDto
                    {
                        Code = updateQuoteStatusDto.Code,
                        PatientId = quote.PatientId,
                        Description = updateQuoteStatusDto.Description,
                        ExpiryDate = updateQuoteStatusDto.ExpiryDate
                    };
                    var message = Newtonsoft.Json.JsonConvert.SerializeObject(createRecipeDto);
                    _rabbitMQService.SendMessage(message);
                }
            }
            else
            {
                throw new ArgumentException("Invalid status value");
            }
        }

        public async Task<List<QuoteDto>> GetAllQuotesAsync()
        {
            var quotes = await _quoteRepository.GetAllAsync();
            return quotes.Select(a => new QuoteDto
            {
                Id = a.Id,
                Date = a.Date,
                Location = a.Location,
                PatientId = a.PatientId,
                DoctorId = a.DoctorId,
                Status = a.Status.ToString()
            }).ToList();
        }
    }

}