using service;
using service.Services;

namespace api.GraphQL.Types;

[GraphQLName("Query")]
public class QueryGql
{
    public UserGql GetMe([Service] UserService service, [GlobalState(GlobalStateKeys.Session)] SessionData? session)
    {
        if (session == null) return null;
        var user = service.GetById(session.UserId);
        if (user == null) return null;
        return UserGql.FromModel(user);
        /*return new UserGql()
        {
            Id = 2,
            FullName = "Alize Smith",
            Email = "alice@example.com",
            AvatarUrl = "https://robohash.org/alize@example.com",
            IsAdmin = false
        };*/
    }

    public IEnumerable<PostGql> GetPosts([Service] PostService service)
    {
        return service.GetAll().Select(PostGql.FromModel);
    }
    
    public PostGql? GetPost([Service] PostService service, int id)
    {
        var model = service.GetById(id);
        if (model == null) return null;
        return PostGql.FromModel(model);
    }
}