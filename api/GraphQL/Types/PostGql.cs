using infrastructure.DataModels;
using service.Services;

namespace api.GraphQL.Types;

[GraphQLName("Post")]
public class PostGql
{
    public int Id { get; set; }
    public int? AuthorId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }

    [GraphQLIgnore]
    public static PostGql FromModel(Post model)
    {
        return new PostGql()
        {
            Id = model.Id,
            Content = model.Content,
            Title = model.Title,
            AuthorId = model.AuthorId
        };
    }
    public UserGql? GetAuthor([Service] UserService service)
    {
        User? user;
        if(AuthorId.HasValue)
        {
            user = service.GetById(AuthorId.Value);
            
            return new UserGql()
            {
                Id = user.Id,
                FullName = user.FullName,
                IsAdmin = user.IsAdmin,
                Email = user.Email,
                AvatarUrl = user.AvatarUrl
            };
        }
        return null;
    }
}