using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotBits.Old
{
    public static class OldGameExtensions
    {
        public static OldLoginClient Connect(this IPlayerIOGame<OldLoginClient> game)
        {
            return game.ConnectAsync().GetResultEx();
        }

        public static Task<OldLoginClient> ConnectAsync(this IPlayerIOGame<OldLoginClient> game)
        {
            return PlayerIOAsync.ConnectAsync(game.GameId, "public", "BotBits", "", null, null)
                .Then(c => game.ConnectionManager.WithClient(c.Result))
                .ToSafeTask();
        }
    }
}
