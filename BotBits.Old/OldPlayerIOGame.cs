using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlayerIOClient;

namespace BotBits.Old
{
    public sealed class OldPlayerIOGame : IPlayerIOGame<OldLoginClient>
    {
        public string GameId { get; }
        public ILogin<OldLoginClient> Login { get; }

        public OldPlayerIOGame(ILogin<OldLoginClient> login, string gameId)
        {
            this.Login = login;
            this.GameId = gameId;
        }
    }
}
