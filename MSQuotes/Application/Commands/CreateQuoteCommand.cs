using System;
using MediatR;

namespace MSQuotes.Application.Commands
{
    public class CreateQuoteCommand : IRequest<int>
    {
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
    }
}