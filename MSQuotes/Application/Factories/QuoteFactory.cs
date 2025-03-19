using System;
using MSQuotes.Domain;

namespace MSPerson.Application.Factories
{
    public static class QuoteFactory
    {
        public static Quote Create(DateTime date, string location, int patientId, int doctorId)
        {
            if (string.IsNullOrWhiteSpace(location))
                throw new ArgumentException("Location cannot be empty.");

            if (patientId <= 0 || doctorId <= 0)
                throw new ArgumentException("Invalid PatientId or DoctorId.");

            return new Quote
            {
                Date = date,
                Location = location,
                PatientId = patientId,
                DoctorId = doctorId,
                Status = QuoteStatus.Pending
            };
        }

        public static void UpdateStatus(Quote quote, QuoteStatus status)
        {
            if (quote == null)
                throw new ArgumentNullException(nameof(quote), "Quote cannot be null.");

            quote.Status = status;
        }
    }
}