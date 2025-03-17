using System;

namespace MSQuotes.Application.DTOs
{
	public class CreateQuoteDto
	{
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }

    }
}