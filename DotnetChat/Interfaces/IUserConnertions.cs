namespace DotnetChat
{
    public interface IUserConnertions
    {
        public void UserConnection(int id, string connectionId);
        public void UserConnectionClose(int id, string connectionId);
    }
}
