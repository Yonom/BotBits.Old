using System;
using PlayerIOClient;

namespace BotBits.Old
{
    public sealed class OldConnectionManager : Package<OldConnectionManager>, IDisposable
    {
        private OldPlayerIOConnectionAdapter _adapter;

        public void Dispose()
        {
            this._adapter?.Disconnect();
        }

        public void AttachConnection(Connection connection, ConnectionArgs args)
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
    }

    public sealed class OldLogin : Package<OldLogin>, IPlayerIOGame<OldLoginClient>, ILogin<OldLoginClient>
    {
        public OldLoginClient WithClient(Client client)
        {
            return new OldLoginClient(OldConnectionManager.Of(this.BotBits), client);
        }

        public string GameId => "everybody-edits-old-gue3mggr0mppaimep8jw";
        ILogin<OldLoginClient> IPlayerIOGame<OldLoginClient>.Login => this;

        public OldPlayerIOGame WithGame(string gameId)
        {
            return new OldPlayerIOGame(this, gameId);
        }
    }
}