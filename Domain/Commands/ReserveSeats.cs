using Domain.ValueObjects;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Commands
{
    public struct ReserveSeats : ICommand
    {
        public ReserveSeats(IEnumerable<Seat> seats, string customer, string screening)
        {
            Customer = customer;
            SeatsToReserve = seats.ToList();
            Screening = screening;
        }

        public string Customer { get; }
        public string Screening { get; }
        public IReadOnlyCollection<Seat> SeatsToReserve { get; }
    }
}
