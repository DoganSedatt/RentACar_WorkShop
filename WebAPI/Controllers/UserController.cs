using Business;
using Business.Abstract;
using Business.Responses.Brand;
using Business.Responses.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }



        [HttpGet("{Id}")]//Route kullanımı böyle de olabilir GetList metodundaki GetAll gibi de.
        public GetUserByIdResponse GetById([FromRoute] GetUserByIdRequest request)
        {//FromRoute demek id isteğini Route'dan al demek.Yani Web sayfası üzerindeki ilgili alandan alacak.
            GetUserByIdResponse response = _userService.GetById(request);
            return response;
        }


        [HttpGet]
        [Route("GetAll")]
        public GetUserListResponse GetList([FromQuery] GetUserListRequest request)
        {
            GetUserListResponse response = _userService.GetList(request);
            return response;
        }
        [HttpPost]
        [Route("UserAdd")]
        public ActionResult<AddUserResponse> Add(AddUserRequest request)
        {
            AddUserResponse response = _userService.Add(request);

            //return response; // 200 OK
            return CreatedAtAction(actionName: nameof(GetList),
            routeValues: new { Id = response.Id }, // Anonymous object
            // Response Header: Location=http://localhost:5245/api/models/1

            value: response);
        }


        [HttpDelete("{Id}")]
        public DeleteUserResponse Delete([FromRoute]DeleteUserRequest request)
        {
            
            DeleteUserResponse deleteUserResponse=_userService.Delete(request);
            return deleteUserResponse;
        }

        [HttpPut("{Id}")]
        public ActionResult<UpdateUserResponse> Update([FromRoute]int Id,[FromBody]UpdateUserRequest request)
        {
            if (Id != request.Id)
                return BadRequest();

            UpdateUserResponse response= _userService.Update(request);
            return response;
        }

    }
}
