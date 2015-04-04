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
    }
}