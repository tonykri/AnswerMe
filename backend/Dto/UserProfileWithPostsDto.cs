using backend.Models;

namespace backend.Dto;

public class UserProfileWithPostsDto: UserProfileDto{
    public UserProfileWithPostsDto(string firstname, string lastname, string email, DateOnly birthdate, ICollection<Post> posts): base(firstname, lastname, email, birthdate)
     {
        Posts = posts;
     }

    public ICollection<Post>? Posts { get; set; }
}