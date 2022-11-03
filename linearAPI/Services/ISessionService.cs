using linearAPI.Entities;

namespace linearAPI.Services
{
    public interface ISessionService
    {
        LinearUser? getUserFromUserName(string username);
        LinearUser? AssertSignedIn(string? sessionId);
        void SignOut(string sessionId);
        string SignIn(LinearUser user);
    }
}