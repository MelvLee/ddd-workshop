namespace Domain.Queries
{
    public struct CustomerReservationsQuery
    {
        public CustomerReservationsQuery(string customer)
        {
            Customer = customer;
        }

        public string Customer { get; }
    }
}
