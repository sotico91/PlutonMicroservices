using System;

namespace MSQuotes.Application.DTOs
{
    public class QuoteDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public string Status { get; set; }
    }
}