namespace BlazorWebRtc_Application.Interface.Services.Manager
{
    public interface IConnectionManager
    {
        void AddConnection(string userId, string connectionId);
        string GetConnection(string userId);
        IEnumerable<string> GetConnections(string userId);
        void RemoveConnection(string connectionId);
        List<string> GetAllConnections();
        List<string> GetSpecificConnections();
        List<string> GetAllUserIds();
        List<string> GetConnectionByUserId(List<string> userIds);
        string GetConnectionByUserIdSingleObj(string userId);    
    }
}
