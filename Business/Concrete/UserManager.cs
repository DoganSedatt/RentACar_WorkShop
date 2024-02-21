using AutoMapper;
using Business.Abstract;
using Business.BusinessRules;
using Business.Profiles.Validation.FluentValidation.Model;
using Business.Profiles.Validation.FluentValidation.User;
using Business.Responses.Model;
using Core.CrossCuttingConcerns.Validation.FluentValidation;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUserDal _userDal;
        private readonly UserBusinnesRules _userBusinnesRules;
        private readonly IMapper _mapper;

        public UserManager(IUserDal userDal,UserBusinnesRules userBusinnesRules,IMapper mapper)
        {
            _userDal = userDal;
            _mapper = mapper;
            _userBusinnesRules = userBusinnesRules;
        }

        public AddUserResponse Add(AddUserRequest request)
        {
            //Bir ekleme yaparken validation,rules,map sırasını uygula
            ValidationTool.Validate(new AddUserRequestValidator(), request);
            _userBusinnesRules.CheckIfUserNameExists(request.FirstName);

            var userToAdd = _mapper.Map<User>(request);
            User addedUser=_userDal.Add(userToAdd);
            var response = _mapper.Map<AddUserResponse>(addedUser);
            return response;

        }

        public DeleteUserResponse Delete(DeleteUserRequest request)
        {
            User? userToDelete = _userDal.Get(predicate: user => user.Id == request.Id);
            //? işareti, User nesnesinin null değer de alabilceğini gösteriyor.
            //Null ise zaten doğrulama kısmını geçemeyecek ve silme işlemi yapılamayacak.
            _userBusinnesRules.CheckIfUserExists(userToDelete);

            //Dal
            User deletedUser =_userDal.Delete(userToDelete!);
            
            //Mapping
            var response=_mapper.Map<DeleteUserResponse>(deletedUser);
            return response;
        }

       

        public GetUserByIdResponse GetById(GetUserByIdRequest request)
        {
            User? user = _userDal.Get(predicate: user => user.Id == request.Id);
            _userBusinnesRules.CheckIfUserExists(user);

            var response = _mapper.Map<GetUserByIdResponse>(user);
            return response;
        }


        public GetUserListResponse GetList(GetUserListRequest request)
        {
            //DAL
            IList<User> userList = _userDal.GetList();

            //Mapping
            GetUserListResponse response = _mapper.Map<GetUserListResponse>(userList); // Mapping
            return response;
        }

        public UpdateUserResponse Update(UpdateUserRequest request)
        {
            User? userToUpdate = _userDal.Get(predicate: user => user.Id == request.Id);
            _userBusinnesRules.CheckIfUserExists(userToUpdate);
            userToUpdate = _mapper.Map(request, userToUpdate);
            User updatedUser=_userDal.Update(userToUpdate!);
            var response = _mapper.Map<UpdateUserResponse>(updatedUser);
            return response;
        }

    }
}
