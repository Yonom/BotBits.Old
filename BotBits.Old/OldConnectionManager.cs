using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotBits.SendMessages;
using PlayerIOClient;

namespace BotBits.Old
{
    public sealed class OldConnectionManager : Package<OldConnectionManager>, IDisposable, IConnectionManager
    {
        private OldPlayerIOConnectionAdapter _adapter;

        void IConnectionManager.AttachConnection(Connection connection, ConnectionArgs args)
        {
            var adapter = new OldPlayerIOConnectionAdapter(connection);
            try
            {
                this.SetConnection(adapter, args);
                this._adapter = adapter;
            }
            catch
            {
                adapter.Dispose();
                throw;
            }

            QuickSend.Enable(this.BotBits);
        }

        public void SetConnection(IConnection connection, ConnectionArgs args)
        {
            ConnectionManager
                .Of(this.BotBits)
                .SetConnection(connection, args);
        }

        public void Dispose()
        {
            if (this._adapter != null)
                this._adapter.Disconnect();
        }
    }

    public sealed class OldLogin : Package<OldLogin>, IPlayerIOGame<OldLoginClient>, ILogin<OldLoginClient>
    {
        public OldLoginClient WithClient(Client client)
        {
            return new OldLoginClient(OldConnectionManager.Of(this.BotBits), client);
        }

        public OldPlayerIOGame WithGame(string gameId)
        {
            return new OldPlayerIOGame(this, gameId);
        }
        
        public string GameId => "everybody-edits-old-gue3mggr0mppaimep8jw";
        ILogin<OldLoginClient> IPlayerIOGame<OldLoginClient>.Login => this;
    }
}
