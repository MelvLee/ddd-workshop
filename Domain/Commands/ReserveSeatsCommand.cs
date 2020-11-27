using Domain.ValueObjects;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Commands
{
    public struct ReserveSeatsCommand
    {
        public ReserveSeatsCommand(IEnumerable<Seat> seats, string customer)
        {
            Customer = customer;
            SeatsToReserve = seats.ToList();
        }

        public string Customer { get; }
        public IReadOnlyCollection<Seat> SeatsToReserve { get; }
    }
}
