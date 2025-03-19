using System;
using MediatR;

namespace MSQuotes.Application.Commands
{
    public class UpdateQuoteStatusCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}