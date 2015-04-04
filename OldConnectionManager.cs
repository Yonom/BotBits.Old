﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotBits.SendMessages;
using PlayerIOClient;

namespace BotBits.Old
{
    public sealed class OldConnectionManager : Package<OldConnectionManager>, IDisposable,
        IPlayerIOGame<OldLoginClient>, IConnectionManager<OldLoginClient>
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
            global::BotBits.ConnectionManager
                .Of(BotBits)
                .SetConnection(connection, args);
        }

        public OldLoginClient WithClient(Client client)
        {
            return new OldLoginClient(this, client);
        }

        public OldLoginGame WithGame(string gameId)
        {
            return new OldLoginGame(this, gameId);
        }

        public string GameId { get { return "everybody-edits-old-gue3mggr0mppaimep8jw"; } }
        public IConnectionManager<OldLoginClient> ConnectionManager { get { return this; } }

        public void Dispose()
        {
            if (this._adapter != null)
                this._adapter.Disconnect();
        }
    }
}