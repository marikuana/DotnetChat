namespace DotnetChat
{
    public interface IOnlineUserService
    {
        IEnumerable<string> GetUserConnections(IEnumerable<int> ids);
        IEnumerable<string> GetUserConnections(int id);
        bool UserIsOnline(int id);
    }
}
