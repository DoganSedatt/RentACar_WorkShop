namespace Business
{
    public class UpdateCustomerResponse
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}