using Domain.Events;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;

namespace Domain.Projections
{
    public class ScreeningState
    {
        public ScreeningState(IEnumerable<IEvent> history)
        {
            foreach (var evt in history)
            {
                switch (evt)
                {
                    case ScreeningPlanned sc:
                        Movie = sc.Movie;
                        TimeOfDay = sc.TimeOfDay;
                        Room = sc.Room;
                        Cinema = sc.Cinema;
                        break;
                    case SeatsReserved sr:
                        ReservedSeats.AddRange(sr.Seats);
                        break;
                    default:
                        break;
                }
            }
        }

        public List<Seat> ReservedSeats { get; } = new List<Seat>();
        public string Movie { get; }
        public DateTime TimeOfDay { get; }
        public string Room { get; }
        public string Cinema { get; }

        public override string ToString()
        {
            return $"{Movie} in {Cinema}, {Room} @ {TimeOfDay:dd-MM-yyyy HH:mm}";
        }
    }
}
