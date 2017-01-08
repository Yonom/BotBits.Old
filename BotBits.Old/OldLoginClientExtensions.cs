using System.Threading.Tasks;

namespace BotBits.Old
{
    public static class OldLoginClientExtensions
    {
        public static Task CreateJoinRoomAsync(this OldLoginClient client, int row, int column)
        {
            return client.CreateJoinRoomAsync(column + "x" + row);
        }

        public static Task JoinRoomAsync(this OldLoginClient client, int row, int column)
        {
            return client.JoinRoomAsync(column + "x" + row);
        }

        public static void JoinRoom(this OldLoginClient client, int row, int column)
        {
            client.CreateJoinRoom(column + "x" + row);
        }

        public static void CreateJoinRoom(this OldLoginClient client, int row, int column)
        {
            client.CreateJoinRoom(column + "x" + row);
        }

        public static Task CreateJoinRoomAsync(this Task<OldLoginClient> client, string worldId)
        {
            return client.Then(task => task.Result.CreateJoinRoomAsync(worldId)).ToSafeTask();
        }

        public static Task JoinRoomAsync(this Task<OldLoginClient> client, string worldId)
        {
            return client.Then(task => task.Result.JoinRoomAsync(worldId)).ToSafeTask();
        }

        public static Task CreateJoinRoomAsync(this Task<OldLoginClient> client, int row, int column)
        {
            return client.Then(task => task.Result.CreateJoinRoomAsync(column, row)).ToSafeTask();
        }

        public static Task JoinRoomAsync(this Task<OldLoginClient> client, int row, int column)
        {
            return client.Then(task => task.Result.JoinRoomAsync(column, row)).ToSafeTask();
        }
    }
}