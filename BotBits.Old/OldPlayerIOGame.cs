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
        public string GameId { get; private set; }
        public IConnectionManager<OldLoginClient> ConnectionManager { get; private set; }

        public OldPlayerIOGame(IConnectionManager<OldLoginClient> connectionManager, string gameId)
        {
            this.ConnectionManager = connectionManager;
            this.GameId = gameId;
        }
    }
}
