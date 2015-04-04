using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlayerIOClient;

namespace BotBits.Old
{
    public sealed class OldLoginGame
    {
        public IConnectionManager<OldLoginClient> Client { get; private set; }
        public string GameId { get; private set; }

        public OldLoginGame(IConnectionManager<OldLoginClient> client, string gameId)
        {
            this.Client = client;
            this.GameId = gameId;
        }
    }
}
