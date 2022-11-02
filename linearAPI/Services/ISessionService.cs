using linearAPI.Entities;

namespace linearAPI.Services
{
    public interface ISessionService
    {
        LinearUser? getUser(string username);
    }
}