using System.Linq;
using System.Threading.Tasks;
using BotBits.Events;
using BotBits.SendMessages;
using PlayerIOClient;

namespace BotBits.Old
{
    public sealed class OldLoginClient : ILoginClient 
    {
        private readonly IConnectionManager _connectionManager;
        private const string FlixelWalker = "FlixelWalker";
        private const int Version = 1;

        public Client Client { get; private set; }

        public OldLoginClient(IConnectionManager connectionManager, Client client)
        {
            this._connectionManager = connectionManager;
            this.Client = client;
        }

        public Task<LobbyItem[]> GetLobbyAsync()
        {
            return this.Client.GetLobbyRoomsAsync(this, FlixelWalker + Version).ToSafeTask();
        }

        public Task CreateOpenWorldAsync(string roomId, string name)
        {
            throw new System.NotSupportedException("Open worlds are not supported.");
        }

        public Task CreateJoinRoomAsync(string roomId)
        {
            return this.Client.Multiplayer
                .CreateJoinRoomAsync(roomId, FlixelWalker + Version, true, null, null)
                .Then(task => this.InitConnection(task.Result))
                .ToSafeTask();
        }

        public Task JoinRoomAsync(string roomId)
        {
            return this.Client.Multiplayer
                .JoinRoomAsync(roomId, null)
                .Then(task => this.InitConnection(task.Result))
                .ToSafeTask();
        }

        public Task<DatabaseWorld> LoadWorldAsync(string roomId)
        {
            throw new System.NotSupportedException("There are no saved world in old EverybodyEdits!");
        }

        private void InitConnection(Connection conn)
        {
            this._connectionManager.AttachConnection(conn,
                new ConnectionArgs(
                    new ShopData(
                        new VaultItem[0]),
                    new PlayerObject(
                        new DatabaseObject()
                            .Set("isModerator", true))));
        }
    }
}
