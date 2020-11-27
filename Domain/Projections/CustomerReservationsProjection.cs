using Domain.Events;
using Domain.ValueObjects;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Projections
{
    public class CustomerReservationsProjection
    {
        Dictionary<string, List<Reservation>> _reservations { get; } = new Dictionary<string, List<Reservation>>();

        public CustomerReservationsProjection(IEnumerable<IEvent> events)
        {
            foreach (var @event in events)
            {
                switch (@event)
                {
                    case SeatsReserved sr:
                        var reservation = new Reservation(sr.Customer, sr.Seats, sr.Screening);

                        if(sr.Customer.CustomerHasReservations(_reservations))
                        {
                            var reservations = _reservations.FromCustomer(sr.Customer);

                            var r = reservations.ForScreening(sr.Screening);
                            if(!r.Equals(Reservation.Default()))
                            {
                                reservation = r.ConcatenateSeats(sr.Seats);
                                reservations.Remove(r);
                            }

                            reservations.Add(reservation);
                            break;
                        }
                        else
                        {
                            _reservations.Add(sr.Customer, new List<Reservation> { reservation });
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        public IEnumerable<Reservation> MyReservations(string customer)
        {
            return _reservations.ContainsKey(customer)
                ? _reservations[customer]
                : new List<Reservation>();
        }
    }

    public static class ReservationsHelpers
    {
        public static bool CustomerHasReservations(this string customer, Dictionary<string, List<Reservation>> customerReservations)
        {
            return customerReservations.ContainsKey(customer);
        }

        public static List<Reservation> FromCustomer(this Dictionary<string, List<Reservation>> reservations, string customer)
        {
            return reservations[customer];
        }

        public static Reservation ForScreening(this List<Reservation> reservations, string screening)
        {
            return reservations.SingleOrDefault(r => r.Screening == screening);
        }

        public static Reservation ConcatenateSeats(this Reservation reservation, IEnumerable<Seat> seats)
        {
            return new Reservation(reservation.Customer, reservation.Seats.Concat(seats), reservation.Screening);
        }
    }
}
