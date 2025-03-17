using System;

namespace MSQuotes.Domain
{
	public class Quote
	{
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public QuoteStatus Status { get; set; }
    }
}