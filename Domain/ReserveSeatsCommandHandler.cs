using Domain.Commands;
using Domain.Entities;
using Domain.Events;
using System;
using System.Collections.Generic;

namespace Domain
{
    public class ReserveSeatsCommandHandler
    {
        public ReserveSeatsCommandHandler(IEnumerable<IEvent> history, Action<IEvent> publish)
        {
            Screening = new Screening(history, publish);
        }

        public Screening Screening { get; set; }

        public void Handle(ReserveSeatsCommand command)
        {
            Screening.Reserve(command.Customer, command.SeatsToReserve);
        }
    }
}
