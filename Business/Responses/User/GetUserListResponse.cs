using Business.Dtos.User;

namespace Business
{
    public class GetUserListResponse
    {
        public ICollection<UserListItemDto> Items { get; set; }
    }
}