using Business;
using Business.Abstract;
using Business.Requests.Model;
using Business.Responses.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        //Service sınıfını kullanacak.Constructor içinde
        private readonly ICustomerService _customerservice;
        public CustomerController(ICustomerService customerService) { 
                _customerservice = customerService;
        }


 
        [HttpPost("CustomerAdd")]
        public ActionResult<AddCustomerResponse> Add(AddCustomerRequest request)
        {
            AddCustomerResponse response = _customerservice.Add(request);
            return CreatedAtAction( // 201 Created
                actionName: nameof(GetById),
                routeValues: new { Id = response.Id }, // Anonymous object
                                                       // Response Header: Location=http://localhost:5245/api/models/1

                value: response // Response Body: JSON
            );
        }

        [HttpGet]

        public GetCustomerListResponse GetList([FromRoute] GetCustomerListRequest request)
        {
            GetCustomerListResponse response = _customerservice.GetList(request);
            return response;
        }

        [HttpGet("{Id}")] // GET http://localhost:5245/api/models/1
        public GetCustomerByIdResponse GetById([FromRoute] GetCustomerByIdRequest request)
        {
            GetCustomerByIdResponse response = _customerservice.GetById(request);
            return response;
        }

    }
}
