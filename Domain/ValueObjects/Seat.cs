using System;
using System.Text.RegularExpressions;

namespace Domain.ValueObjects
{
    public struct Seat
    {
        static readonly Regex _seatRegex = new Regex("(?<row>[A-Z]{1})-(?<number>[0-9]{1,2})");

        public Seat(string seat)
        {
            Match match = _seatRegex.Match(seat);
            if (!match.Success)
            {
                throw new ArgumentException($"{seat} is not valid");
            }

            Row = match.Groups["row"].Value;
            Number = match.Groups["number"].Value;
        }

        public readonly string Row;
        public readonly string Number;

        public override bool Equals(object obj)
        {
            return obj is Seat seat &&
                   Row == seat.Row &&
                   Number == seat.Number;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Row, Number);
        }

        public override string ToString()
        {
            return $"Seat {Row}-{Number}";
        }
    }
}
