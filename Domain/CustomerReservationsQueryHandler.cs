using Domain.Events;
using Domain.Projections;
using Domain.Queries;
using Domain.ValueObjects;
using System.Collections.Generic;

namespace Domain
{
    public class CustomerReservationsQueryHandler
    {
        readonly CustomerReservationsProjection _projection;
        
        public CustomerReservationsQueryHandler(IEnumerable<IEvent> events)
        {
            _projection = new CustomerReservationsProjection(events);
        }

        public IEnumerable<Reservation> Handle(CustomerReservationsQuery query)
        {
            return _projection.MyReservations(query.Customer);
        }
    }
}
