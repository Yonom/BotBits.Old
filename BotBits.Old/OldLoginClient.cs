using System;
using System.Linq;
using System.Threading.Tasks;
using PlayerIOClient;

namespace BotBits.Old
{
    public sealed class OldLoginClient : ILoginClient
    {
        private const string FlixelWalker = "FlixelWalker";
        private const int Version = 1;
        private readonly OldConnectionManager _oldConnectionManager;

        public OldLoginClient(OldConnectionManager oldConnectionManager, Client client)
        {
            this._oldConnectionManager = oldConnectionManager;
            this.Client = client;
        }

        public Client Client { get; }

        public string ConnectUserId => this.Client.ConnectUserId;

        public Task<LobbyItem[]> GetLobbyAsync()
        {
            return this.GetLobbyRoomsAsync(this.Client, FlixelWalker + Version).ToSafeTask();
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
            this._oldConnectionManager.AttachConnection(conn,
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

        public Task<LobbyItem[]> GetLobbyRoomsAsync(Client client, string roomType)
        {
            return client.Multiplayer
                .ListRoomsAsync(roomType, null, 0, 0)
                .Then(r => r.Result
                    .Select(room => new LobbyItem(this, room))
                    .ToArray())
                .ToSafeTask();
        }
    }
}