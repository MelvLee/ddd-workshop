using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Events
{
    public struct SeatsNotReserved : IEvent
    {
        public SeatsNotReserved(string customer, IEnumerable<Seat> seats, string reason)
        {
            Customer = customer;
            Seats = seats.ToList();
            Reason = reason;
        }

        public string Customer { get; }
        public IReadOnlyCollection<Seat> Seats { get; }
        public string Reason { get; }

        public bool Equals(SeatsNotReserved other)
        {
            return Customer == other.Customer &&
                   Seats.Count == other.Seats.Count &&
                   Seats.All((seat) => other.Seats.Contains(seat)) &&
                   Reason == other.Reason;
        }

        public override bool Equals(object obj)
        {
            return obj is SeatsNotReserved other &&
                   Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Seats, Reason);
        }
    }
}
