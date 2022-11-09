using LinearEntities.Entities;

namespace LinearAPI.Services
{
    public interface ISessionService
    {
        LinearUser? getUserFromUserName(string username);
        LinearUser? AssertSignedIn(string? sessionId);
        void SignOut(string sessionId);
        string SignIn(LinearUser user);
    }
}