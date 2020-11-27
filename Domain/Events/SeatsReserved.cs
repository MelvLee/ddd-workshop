using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Events
{
    public struct SeatsReserved : IEvent
    {
        public SeatsReserved(string customer, IEnumerable<Seat> seats, string screening)
        {
            Customer = customer;
            Seats = seats.ToList();
            Screening = screening;
        }

        public string Customer { get; }
        public IReadOnlyCollection<Seat> Seats { get; }
        public string Screening { get; }

        public bool Equals(SeatsReserved other)
        {
            return Customer == other.Customer &&
                   Screening == other.Screening &&
                   Seats.Count == other.Seats.Count &&
                   Seats.All((seat) => other.Seats.Contains(seat));
        }

        public override bool Equals(object obj)
        {
            return obj is SeatsReserved other &&
                   Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Seats);
        }
    }
}
