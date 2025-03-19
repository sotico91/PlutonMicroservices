using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using MSQuotes.Application.Commands;
using MSQuotes.Application.DTOs;
using MSQuotes.Application.Interfaces;
using MSQuotes.Application.Queries;
using MSQuotes.Domain;

namespace MSQuotes.Application.Services
{
	public class QuoteService : IQuoteService
	{

        private readonly IMediator _mediator;

        public QuoteService(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<int> CreateQuoteAsync(CreateQuoteDto createQuoteDto)
        {
            var command = new CreateQuoteCommand
            {
                Date = createQuoteDto.Date,
                Location = createQuoteDto.Location,
                PatientId = createQuoteDto.PatientId,
                DoctorId = createQuoteDto.DoctorId
            };

            return await _mediator.Send(command);
        }
        public async Task UpdateQuoteStatusAsync(int id, UpdateQuoteStatusDto updateQuoteStatusDto)
        {
            if (!Enum.TryParse(updateQuoteStatusDto.Status, out QuoteStatus status))
                throw new ArgumentException("Invalid status value");

            var command = new UpdateQuoteStatusCommand
            {
                Status = status.ToString(),
                Code = updateQuoteStatusDto.Code,
                Description = updateQuoteStatusDto.Description,
                ExpiryDate= updateQuoteStatusDto.ExpiryDate
            };

            await _mediator.Send(command);
        }

        public async Task<IEnumerable<QuoteDto>> GetAllQuotesAsync()
        {
            var quotes = await _mediator.Send(new GetAllQuotesQuery());
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
                Status = ((QuoteStatus)quote.Status).ToString()
            };
        }

    }
}