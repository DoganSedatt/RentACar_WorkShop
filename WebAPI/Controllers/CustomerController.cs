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
            return response; // 201 Created
        }



        [HttpGet("GetAll")]
        public GetCustomerListResponse GetList([FromQuery] GetCustomerListRequest request)
        {
            GetCustomerListResponse response = _customerservice.GetList(request);
            return response;
        }

        [HttpGet("GetById")] // GET http://localhost:5245/api/models/1
        public GetCustomerByIdResponse GetById([FromQuery] GetCustomerByIdRequest request)
        {
            GetCustomerByIdResponse response = _customerservice.GetById(request);
            return response;
        }
        [HttpPut("{Id}")]
        public ActionResult<UpdateCustomerResponse> Update([FromRoute] int Id,[FromBody] UpdateCustomerRequest request)
        {
            if (request.Id != Id)
                return BadRequest();

            UpdateCustomerResponse response= _customerservice.Update(request);
            return Ok(response);
        }

        [HttpDelete]
        public ActionResult<DeleteCustomerResponse> Delete(DeleteCustomerRequest request)
        {
            DeleteCustomerResponse response=_customerservice.Delete(request);
            return response;
        }

    }
}
