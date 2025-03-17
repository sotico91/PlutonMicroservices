using System;

namespace MSQuotes.Application.DTOs
{
	public class UpdateQuoteStatusDto
	{
        public string Code { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public DateTime ExpiryDate { get; set; }

    }
}