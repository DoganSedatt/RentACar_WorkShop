﻿namespace Business
{
    public class UpdateCustomerRequest
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }

    }
}