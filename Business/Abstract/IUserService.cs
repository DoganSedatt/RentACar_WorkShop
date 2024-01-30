using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IUserService
    {
        public GetUserListResponse GetList(GetUserListRequest request);
        public GetUserByIdResponse GetById(GetUserByIdRequest request);
        public AddUserResponse Add(AddUserRequest request);
        public UpdateUserResponse Update(UpdateUserRequest request);
        public DeleteUserResponse Delete(DeleteUserRequest request);
    }
}
