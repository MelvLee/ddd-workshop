using Domain.Events;
using Domain.Projections;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Entities
{
    public class Screening
    {
        private readonly ScreeningState _state;
        private readonly Action<IEvent> _publish;

        public Screening(IEnumerable<IEvent> history, Action<IEvent> publish)
        {
            _state = new ScreeningState(history);
            _publish = publish;
        }

        public void Reserve(string customer, IEnumerable<Seat> seats)
        {
            if (SeatsAreReserved(seats))
            {
                _publish(new SeatsNotReserved(customer, seats, "The seats are not available"));

                return;
            }

            _publish(new SeatsReserved(customer, seats, _state.ToString()));
        }

        private bool SeatsAreReserved(IEnumerable<Seat> seats)
        {
            return seats.Any((seat) => _state.ReservedSeats.Contains(seat));
        }

        public override string ToString()
        {
            return _state.ToString();
        }
    }
}
