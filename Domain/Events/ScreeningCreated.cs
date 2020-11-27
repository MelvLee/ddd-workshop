using System;

namespace Domain.Events
{
    public struct ScreeningCreated : IEvent
    {
        public ScreeningCreated(string movie, DateTime timeOfDay, string room, string cinema)
        {
            Movie = movie;
            TimeOfDay = timeOfDay;
            Room = room;
            Cinema = cinema;
        }

        public string Movie { get; }
        public DateTime TimeOfDay { get; }
        public string Room { get; }
        public string Cinema { get; }
    }
}
