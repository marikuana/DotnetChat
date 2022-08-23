namespace DotnetChat
{
    public class OnlineUserService : IUserConnertions, IOnlineUserService
    {
        private IDictionary<int, IList<string>> onlineUsers;

        public OnlineUserService()
        {
            onlineUsers = new Dictionary<int, IList<string>>();
        }

        public void UserConnection(int id, string connectionId)
        {
            if (!onlineUsers.ContainsKey(id))
                onlineUsers[id] = new List<string>();
            onlineUsers[id].Add(connectionId);
        }

        public void UserConnectionClose(int id, string connectionId)
        {
            if (onlineUsers.ContainsKey(id))
                onlineUsers[id].Remove(connectionId);
        }

        public bool UserIsOnline(int id)
        {
            return onlineUsers.ContainsKey(id) && onlineUsers[id].Count > 0;
        }

        public IEnumerable<string> GetUserConnections(int id)
        {
            if (onlineUsers.ContainsKey(id))
                return onlineUsers[id];
            return new List<string>();
        }

        public IEnumerable<string> GetUserConnections(IEnumerable<int> ids)
        {
            List<string> connectionsId = new List<string>();
            foreach (var id in ids)
                connectionsId.AddRange(GetUserConnections(id));
            return connectionsId;
        }
    }
}
