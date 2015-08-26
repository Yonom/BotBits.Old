using System;
using System.Threading.Tasks;
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

        Task ILoginClient.CreateOpenWorldAsync(string roomId, string name)
        {
            throw new NotSupportedException("Open worlds are not supported.");
        }

        public Task CreateJoinRoomAsync(string roomId)
        {
            return this.Client.Multiplayer
                .CreateJoinRoomAsync(roomId, FlixelWalker + Version, true, null, null)
                .Then(task => this.InitConnection(roomId, task.Result))
                .ToSafeTask();
        }

        public Task JoinRoomAsync(string roomId)
        {
            return this.Client.Multiplayer
                .JoinRoomAsync(roomId, null)
                .Then(task => this.InitConnection(roomId, task.Result))
                .ToSafeTask();
        }

        Task<DatabaseWorld> ILoginClient.LoadWorldAsync(string roomId)
        {
            throw new NotSupportedException("There are no saved world in old EverybodyEdits!");
        }

        private void InitConnection(string roomId, Connection conn)
        {
            this._connectionManager.AttachConnection(conn,
                new ConnectionArgs(
                    this.ConnectUserId,
                    roomId, 
                    new PlayerData(
                        new PlayerObject(
                            new DatabaseObject()
                                .Set("isModerator", true)),
                        new ShopData(
                            new VaultItem[0]))));
        }

        public string ConnectUserId { get { return this.Client.ConnectUserId; } }
    }
}
