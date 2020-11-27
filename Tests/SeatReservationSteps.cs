using Domain;
using Domain.Commands;
using Domain.Entities;
using Domain.Events;
using Domain.Queries;
using Domain.ValueObjects;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using TechTalk.SpecFlow;

namespace Tests
{
    public static class Helpers
    {
        public static IEnumerable<Seat> ToSeats(this string seats)
        {
            return from seat in seats.Split(",")
                   select new Seat(seat.Trim());
        }
    }

    [Binding]
    public class SeatReservationSteps
    {
        readonly ICollection<IEvent> _events = new List<IEvent>();
        readonly ICollection<IEvent> _publishedEvents = new List<IEvent>();
        IEnumerable<Reservation> _reservations;
        private Screening _screening;

        [Given(@"the Screening for Movie '(.*)' scheduled on '(.*)' in (.*) in Cinema '(.*)'")]
        public void GivenTheScreening(string movie, DateTime timeOfDay, string room, string cinema)
        {
            _events.Add(new ScreeningCreated(movie, timeOfDay, room, cinema));
        }
        
        [Given(@"there are no Reservations")]
        public void GivenThereAreNoReservations()
        {
        }

        [Given(@"the Reservations")]
        public void GivenTheReservations(Table table)
        {
            foreach(var row in table.Rows)
            {
                _events.Add(new SeatsReserved(row["Customer"], row["Seats"].ToSeats(), row["Screening"]));
            }
        }

        [When(@"Customer '(.*)' reserves Seats '(.*)'")]
        public void WhenCustomerReservesSeats(string customer, string seats)
        {
            var sut = new ReserveSeatsCommandHandler(_events, (@event) =>
            {
                _publishedEvents.Add(@event);

                _events.Add(@event);
            });

            sut.Handle(new ReserveSeatsCommand(seats.ToSeats(), customer));
            _screening = sut.Screening;
        }

        [When(@"Customer '(.*)' queries his Reservations")]
        public void WhenCustomerQueriesHisReservations(string customer)
        {
            _reservations = new CustomerReservationsQueryHandler(_events).Handle(new CustomerReservationsQuery(customer));
        }

        [Then(@"Seats '(.*)' should be reserved for Customer '(.*)' for the given Screening")]
        public void ThenSeatsShouldBeReservedForCustomerForTheGivenScreening(string seats, string customer)
        {
            _publishedEvents.Should().Contain(new SeatsReserved(customer, seats.ToSeats(), _screening.ToString()));
        }

        [Then(@"Customer '(.*)' should be informed that the Seats are not available")]
        public void ThenCustomerShouldBeInformedThatTheSeatsAreNotAvailable(string customer)
        {
            _publishedEvents.Should().Contain(evt => evt is SeatsNotReserved);
        }

        [Then(@"Customer '(.*)' should get the following Reservations")]
        public void ThenCustomerShouldGetTheFollowingReservations(string customer, Table table)
        {
            var expected = from row in table.Rows
                           select new Reservation(row["Customer"], row["Seats"].ToSeats(), row["Screening"]);

            _reservations.Should().Equal(expected);
        }
    }









}
