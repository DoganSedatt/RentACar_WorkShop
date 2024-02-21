namespace Business
{
    public class DeleteCustomerResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime DeletedAt { get; set; }
    }
}