using System;
using System.Collections.Generic;
using System.Linq;
using PlayerIOClient;

namespace BotBits.Old
{
    public class OldPlayerIOConnectionAdapter : IConnection, IDisposable
    {
        private readonly PlayerIOConnectionAdapter _innerAdapter;
        private readonly HashSet<int> _knownPlayers = new HashSet<int>();

        public OldPlayerIOConnectionAdapter(Connection connection)
        {
            this._innerAdapter = new PlayerIOConnectionAdapter(connection);
            this._innerAdapter.OnMessage += this._innerAdapter_OnMessage;
        }

        public void Send(Message message)
        {
            try
            {
                Message m;
                switch (message.Type)
                {
                    case "init":
                        m = message;
                        break;
                    case "11f":
                        m = Message.Create("face", message[0]);
                        break;
                    case "m":
                        m = Message.Create("update", message[0], message[1], message[2], message[3], message[4], message[5], message[6], message[7]);
                        break;
                    case "11": // Block message
                        m = Message.Create("change", message[1], message[2], message[3]);
                        break;

                    default:
                        return;
                }
                this._innerAdapter.Send(m);
            }
            catch (Exception)
            {
            }
        }

        public void Disconnect()
        {
            this._innerAdapter.Disconnect();
        }

        public bool Connected
        {
            get { return this._innerAdapter.Connected; }
        }

        public event MessageReceivedEventHandler OnMessage;

        public event DisconnectEventHandler OnDisconnect
        {
            add { this._innerAdapter.OnDisconnect += value; }
            remove { this._innerAdapter.OnDisconnect -= value; }
        }

        public void Dispose()
        {
            this._innerAdapter.Dispose();
        }

        private void AddIfNotExists(int userId, int smiley = 0, double x = 16, double y = 16)
        {
            if (!this._knownPlayers.Contains(userId))
            {
                this.InvokeOnMessage(this.GetAdd(Message.Create("add", userId, smiley, x, y)));
            }
        }

        private void _innerAdapter_OnMessage(object sender, Message e)
        {
            Message m;
            switch (e.Type)
            {
                case "init":
                    m = this.GetInit(e);
                    break;
                case "add":
                    m = this.GetAdd(e);
                    break;
                case "change":
                    m = this.GetBlock(e);
                    break;
                case "update":
                    m = this.GetMove(e);
                    break;
                case "face":
                    m = this.GetSmiley(e);
                    break;
                case "left":
                    m = this.GetLeft(e);
                    break;

                default:
                    return;
            }
            this.InvokeOnMessage(m);
        }

        private Message GetInit(Message e)
        {
            var worldData = e.GetString(0);
            var myId = e.GetInt(1);

            this._knownPlayers.Add(myId);
            var m = Message.Create("init", "nobody", "Untitled World", 0, 0, 0, "11",
                myId,
                20, 20,
                "Guest-" + myId,
                true, false, 100, 100, false, 1, false, 0, true);

            var ids = worldData.Split('\n')
                .Select(l => l.Split(',')
                    .Select(int.Parse).ToArray()).ToArray();

            var groups = new Dictionary<int, List<Point>>();
            for (var x = 0; x < 100; x++)
            {
                for (var y = 0; y < 100; y++)
                {
                    var id = ids[y][x];
                    List<Point> group;
                    if (!groups.TryGetValue(id, out group))
                    {
                        group = new List<Point>();
                        groups.Add(id, group);
                    }
                    group.Add(new Point(x, y));
                }
            }

            m.Add("ws");
            foreach (var group in groups)
            {
                m.Add(0);
                m.Add(group.Key);
                m.Add(group.Value.SelectMany(p => new[] { (byte)(p.X >> 8), (byte)((p.X & 0xFF)) }).ToArray());
                m.Add(group.Value.SelectMany(p => new[] { (byte)(p.Y >> 8), (byte)((p.Y & 0xFF)) }).ToArray());
            }
            m.Add("we");

            m.Add("ps");
            m.Add("pe");

            return m;
        }

        private Message GetAdd(Message e)
        {
            var userId = e.GetInt(0);
            var smiley = e.GetInt(1);
            var x = e.GetInt(1);
            var y = e.GetInt(1);

            this._knownPlayers.Add(userId);
            return Message.Create("add", userId, "Guest-" + userId, smiley, x, y,
                false, false, false, 0, 0, false, false, false);
        }

        private Message GetMove(Message e)
        {
            var userId = e.GetInt(0);
            var x = e.GetInteger(1);
            var y = e.GetInteger(2);
            var speedX = e.GetDouble(3);
            var speedY = e.GetDouble(4);
            var modifierX = e.GetDouble(5);
            var modifierY = e.GetDouble(6);
            var horizontal = e.GetDouble(7);
            var vertical = e.GetDouble(8);


            this.AddIfNotExists(userId, x: x, y: y);
            return Message.Create("m", userId, x, y, speedX, speedY, modifierX, modifierY, horizontal, vertical,
                0, false);
        }

        private Message GetSmiley(Message e)
        {
            var userId = e.GetInt(0);
            var smiley = e.GetInt(1);

            this.AddIfNotExists(userId, smiley);
            return Message.Create("face", userId, smiley);
        }

        private Message GetLeft(Message e)
        {
            var userId = e.GetInt(0);

            this.AddIfNotExists(userId);
            return Message.Create("left", userId);
        }

        private Message GetBlock(Message e)
        {
            var x = e.GetInteger(0);
            var y = e.GetInteger(1);
            var block = e.GetInt(2);
            return Message.Create("b", 0, x, y, block);
        }

        protected virtual void InvokeOnMessage(Message e)
        {
            var handler = this.OnMessage;
            if (handler != null) handler(this, e);
        }
    }
}