﻿namespace Business
{
    public class GetCustomerByIdResponse
    {
        //Verdiğim customer Id'sine göre dönecek cevaplar
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        
    }
}